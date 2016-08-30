let inferInt = (+) 1
let inferFloat = (+) 1.0
let inferDecimal = (+) 1m // m suffix means decimal
let inferSByte = (+) 1y // y suffix means signed byte
let inferChar = (+) 'a' // a char
let inferString = (+) "my string"

let x = 1
let y = x // deduce that y is also an int

// if..else implies a bool
let inferBool x = if x then false else true
// for..do implies a sequence
let inferStringList x = for y in x do printfn "%s" y
// :: implies a list
let inferIntList x = 99::x
// .NET library method is strongly typed
let inferStringAndBool x = System.String.IsNullOrEmpty(x)

let inferInt2 (x:int) = x
let inferIndirectInt2 = inferInt2

let inferGeneric x = x

let outerFn action : string =
    let innerFn x = x + 1 // define a sub fn that returns an int
    action (innerFn 2) // result of applying action to innerFn

let doItTwice f = (f >> f)

let add3 = (+) 3
let add6 = doItTwice add3

// test
add6 5 // 11

let square x = x * x
let fourthPower = doItTwice square
// test
fourthPower 3 // 81

let chittyBang x = "Chitty " + x + " Bang"
let chittyChittyBangBang = doItTwice chittyBang
// test
chittyChittyBangBang "&"

let rec fib n = // REC
    if n <= 2 then 1
    else fib (n - 1) + fib (n - 2)

let rec showPositiveNumber x =
    match x with
    | x when x >= 0 -> printfn "%i is positive" x
    | _ -> showNegativeNumber x

and showNegativeNumber x =
    match x with
    | x when x < 0 -> printfn "%i is negative" x
    | _ -> showPositiveNumber x

type A = None | AUsesB of B
and B = None | BUsesA of A // ...AND?

let stringLength (s:string) = s.Length

["hello";"world"] |> List.map (fun s -> s.Length)

let myBottomLevelFn x = x

let myMidLevelFn x =
    let y = myBottomLevelFn x
    // some stuff
    let z = y
    // some stuff
    printf "%s" z // kills generic types
    // some more stuff
    x

let myTopLevelFn x =
    // some stuff
    myMidLevelFn x
    // some more stuff
    x