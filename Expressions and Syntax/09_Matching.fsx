// order is important!!!
let x =
    match 1 with // on new line
    // aligned
    | 1 -> "a" // compact patterns can be on one line
    // aligned
    | 2 -> // longer patterns should be on a new line
        "a very long pattern that breaks up the flow"
    // aligned
    | i when i >= 0 && i <= 100 -> "ok"
    // aligned
    | _ -> "z"

let y =
    match (1,0) with
    // binding to a named value
    // OR -- same as multiple cases on one line
    | (1,x) | (2,x) | (3,x) | (4,x) -> printfn "x=%A" x

    // AND -- must match both patterns at once
    // Note only a signle & is used
    | (2,x) & (_,1) -> printfn "x=%A" x

    | (y,_) -> printfn "y=%A" y

let z =
    match [1;2;3] with
    // binding to explicit positions
    // square brackets used!
    | [1;x;y] -> printfn "x=%A y=%A" x y

    // binding to head::tail
    // no square brackets used!
    | 1::tail -> printfn "tail=%A" tail

    // empty list
    | [] -> printfn "empty"

    // any other list 
    | x -> printfn "list=%A" x

// loop through a alist and print the values
let rec loopAndPrint list =
    match list with
    // empty list means we're done.
    | [] ->
        printfn "empty"

    // binding to head::tail
    | x::xs ->
        printfn "element=%A," x
        // do all over again with the
        // rest of the list
        loopAndPrint xs

// test
loopAndPrint [1..5]

// ------------------------
// loop through a list and sum the values
let rec loopAndSum list sumSoFar = 
    match list with 
    // empty list means we're done.
    | [] -> 
        sumSoFar  

    // binding to head::tail. 
    | x::xs -> 
        let newSumSoFar = sumSoFar + x
        // do all over again with the 
        // rest of the list and the new sum
        loopAndSum xs newSumSoFar 

//test
loopAndSum [1..5] 0

// -----------------------
// Tuple pattern matching
let tuple = (1,2)
match tuple with 
| (1,_) -> printfn "first part is 1"
| (_,2) -> printfn "second part is 2"
| (_,_) -> printfn "first part is not 1 and second part is not 2"


// -----------------------
// Record pattern matching
type Person = {First:string; Last:string}
let person = {First="john"; Last="doe"}
match person with 
| {First="john"}  -> printfn "Matched John" 
| _  -> printfn "Not John" 

// -----------------------
// Union pattern matching
type IntOrBool= I of int | B of bool
let intOrBool = I 42
match intOrBool with 
| I i  -> printfn "Int=%i" i
| B b  -> printfn "Bool=%b" b

let a =
    match (1,0) with
    // binding to three values
    | (x,y) as t ->
        printfn "x=%A and y=%A" x y
        printfn "The whole tuple is %A" t

let matchOnTwoParamters x y =
    match (x,y) with
    | (1,y) ->
        printfn "x=1 and y=%A" y 
    | (x,1) ->
        printfn "x=%A and y=1" x
    | t ->
        printfn "%A" t

// test
matchOnTwoParamters 1 0

let matchOnTwoTuples x y = 
    match (x,y) with 
    | (1,_),(1,_) -> "both start with 1"
    | (_,2),(_,2) -> "both end with 2"
    | _ -> "something else"

// test
matchOnTwoTuples (1,3) (1,2)
matchOnTwoTuples (3,2) (1,2)

let elementsAreEqual tuple =
    match tuple with
    | (x,y) when x=y ->
        printfn "both parts are the same"
    | _ ->
        printfn "both parts are different"

// --------------------------------
// comparing values in a when clause
let makeOrdered tuple = 
    match tuple with 
    // swap if x is bigger than y
    | (x,y) when x > y -> (y,x)
        
    // otherwise leave alone
    | _ -> tuple

//test        
makeOrdered (1,2)        
makeOrdered (2,1)

// --------------------------------
// testing properties in a when clause        
let isAM date = 
    match date:System.DateTime with 
    | x when x.Hour <= 12-> 
        printfn "AM"
        
    // otherwise leave alone
    | _ -> 
        printfn "PM"

//test
isAM System.DateTime.Now

// --------------------------------
// pattern matching using regular expressions
open System.Text.RegularExpressions

// create an active pattern to match an email address
let (|EmailAddress|_|) input =
   let m = Regex.Match(input,@".+@.+") 
   if (m.Success) then Some input else None  

// use the active pattern in the match   
let classifyString string = 
    match string with 
    | EmailAddress x -> 
        printfn "%s is an email" x
        
    // otherwise leave alone
    | _ -> 
        printfn "%s is something else" string

//test
classifyString "alice@example.com"
classifyString "google.com"

// --------------------------------
// pattern matching using arbitrary conditionals
let fizzBuzz x = 
    match x with 
    | i when i % 15 = 0 -> 
        printfn "fizzbuzz" 
    | i when i % 3 = 0 -> 
        printfn "fizz" 
    | i when i % 5 = 0 -> 
        printfn "buzz" 
    | i  -> 
        printfn "%i" i

//test
[1..30] |> List.iter fizzBuzz

let f =
    function
    | _ -> "something"

// using function keyword
let g =
    function
    | x ->
        function
        | _ -> "something"

// using function keyword
[2..10] |> List.map (function
        | 2 | 3 | 5 | 7 -> sprintf "prime"
        | _ -> sprintf "not prime"
        )

let debugMode = false
try
    failwith "fail"
with
    | Failure msg when debugMode ->
        reraise()
    | Failure msg when not debugMode ->
        printfn "silently logged in production: %s" msg

// much easier to use the built in function
let addOneIfValid optionalInt =
    optionalInt |> Option.map (fun i -> i + 1)

Some 42 |> addOneIfValid

type TemperatureType = F of float | C of float

module Temperature =
    let fold fahrenheitFunction celsiusFunction temp =
        match temp with
        | F f -> fahrenheitFunction f
        | C c -> celsiusFunction c

let fFever tempF =
    if tempF > 100.0 then "Fever!" else "OK"

let cFever tempC =
    if tempC > 38.0 then "Fever!" else "OK"

// combine using the fold
let isFever = Temperature.fold fFever cFever // remove temp from both sides

let normalTemp = C 37.0
let result1 = isFever normalTemp

let highTemp = F 103.1
let result2 = isFever highTemp

let fConversion tempF =
    let convertedValue = (tempF - 32.0) / 1.8
    TemperatureType.C convertedValue // wrapped in type

let cConversion tempC =
    let convertedValue = (tempC * 1.8) + 32.0
    TemperatureType.F convertedValue // wrapped in type

// combine using the fold
let convert = Temperature.fold fConversion cConversion // remove temp from both sides

let c20 = C 20.0
let resultInF = convert c20

let f75 = F 75.0
let resultInC = convert f75

C 20.0 |> convert |> convert