// throws a generic System.Exception
let f x =
    if x then "ok"
    else failwith "message"

// throws an ArgumentException
let g x =
    if x then "ok"
    else invalidArg "paramName" "message"

// throws a NullArgumentException
let h x =
    if x then "ok"
    else nullArg "paramName"

// throws an InvalidOperationException
let i x =
    if x then "ok"
    else invalidOp "message"

// throws a standard .NET exception class
open System
let j x =
    if x then "ok"
    else raise (new InvalidOperationException("message"))

let k x =
    if x then failwith "error in true branch"
    else failwith "error in false branch"

try
    failwith "fail"
with
    | Failure msg -> "caught: " + msg

let divide x y =
    try
        (x+1) / y
    with
        | :? System.DivideByZeroException as ex ->
            printfn "%s" ex.Message
            reraise()

// test
divide 1 1
divide 1 0

let l x =
    try
        if x then "ok"
        else failwith "fail"
    finally
        printf "this will always be printed"

// library function that doesn't handle exceptions
let divideException x y = x / y

// library function that converts exceptions to None
let tryDivide x y =
    try
        Some (x / y)
    with
        | :? System.DivideByZeroException -> None // return missing