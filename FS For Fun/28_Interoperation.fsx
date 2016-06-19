// using an Int32
let (i1success,i1) = System.Int32.TryParse("123")
if i1success then printfn "Parsed as %i" i1 else printfn "parse failed"

let (i2success,i2) = System.Int32.TryParse("hello")
if i2success then printfn "Parsed as %i" i1 else printfn "parse failed"

// using a DateTime
let (d1success,d1) = System.DateTime.TryParse("1/1/1980")
if d1success then printfn "Parsed as %A" d1 else printfn "parse failed"

let (d2success,d2) = System.DateTime.TryParse("hello")
if d2success then printfn "Parsed as %A" d2 else printfn "parse failed"

// using a dictionary
let dict = new System.Collections.Generic.Dictionary<string,string>()
dict.Add("a","hello")
let (e1success,e1) = dict.TryGetValue("a")
if e1success then printfn "Parsed as %A" e1 else printfn "parse failed"

let (e2success,e2) = dict.TryGetValue("b")
if e2success then printfn "Parsed as %A" e2 else printfn "parse failed"

let createReader fileName = new System.IO.StreamReader(path=fileName)

let (|Digit|Letter|Whitespace|Other|) ch =
    if System.Char.IsDigit(ch) then Digit
    else if System.Char.IsLetter(ch) then Letter
    else if System.Char.IsWhiteSpace(ch) then Whitespace
    else Other

let printChar ch =
    match ch with
    | Digit -> printfn "%c is a Digit" ch
    | Letter -> printfn "%c is a Letter" ch
    | Whitespace -> printfn "%c is a Whitespace" ch
    | _ -> printfn "%c is something else" ch

// print a list
['a';'b';'1';' ';'-';'c'] |> List.iter printChar

open System.Data.SqlClient

let (|ConstraintException|ForeignKeyException|Other|) (ex:SqlException) =
    if ex.Number = 2601 then ConstraintException
    else if ex.Number = 2627 then ConstraintException
    else if ex.Number = 547 then ForeignKeyException
    else Other

let executeNonQuery (sqlCommand:SqlCommand) =
    try
        let result = sqlCommand.ExecuteNonQuery()
        result
        // handle success
    with
    | :? SqlException as sqlException -> // if a SqlException
        match sqlException with // nice pattern matching
        | ConstraintException -> 2601 // handle constraint error
        | ForeignKeyException -> 547 // handle FK error
        | _ -> reraise() // don't handle any other cases
    // all non SqlExceptions are thrown normally

// create a new object that implements IDisposable
let makeResource name = 
    { new System.IDisposable with member this.Dispose() = printfn "%s disposed" name }

let useAndDisposeResources =
    use r1 = makeResource "first resource"
    printfn "using first resource"
    for i in [1..3] do
        let resourceName = sprintf "\tinner resoruce %d" i
        use temp = makeResource resourceName
        printfn "\tdo something with %s" resourceName
    use r2 = makeResource "second resource"
    printfn "using second resource"
    printfn "done."

type IAnimal = abstract member MakeNoise : unit -> string

let showTheNoiseAnAnimalMakes (animal:IAnimal) =
    animal.MakeNoise() |> printfn "Making noise %s"

type Cat = Felix | Socks
type Dog = Butch | Lassie

// now mixin the interface with the F# types
type Cat with member this.AsAnimal = { new IAnimal with member a.MakeNoise() = "Meow" }
type Dog with member this.AsAnimal = { new IAnimal with member a.MakeNoise() = "Woof" }

let dog = Lassie
showTheNoiseAnAnimalMakes (dog.AsAnimal)

let cat = Felix
showTheNoiseAnAnimalMakes (cat.AsAnimal)

open System.Reflection
open Microsoft.FSharp.Reflection

// create a record type...
type Account = {Id: int; Name: string}

// ... and show the fields
let fields =
    FSharpType.GetRecordFields(typeof<Account>)
    |> Array.map (fun propInfo -> propInfo.Name, propInfo.PropertyType.Name)

// create a union type...
type Choices = | A of int | B of string

// and show the choices
let choices =
    FSharpType.GetUnionCases(typeof<Choices>)
    |> Array.map (fun choiceInfo -> choiceInfo.Name)