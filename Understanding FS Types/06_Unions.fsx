type Person = {first: string; last: string} // define a record type
type IntOrBool = // define a union type
    | I of int
    | B of bool

type MixedType =
    | Tup of int * int // a tuple
    | P of Person // use the record type
    | L of int list // a list of ints
    | U of IntOrBool // use the union type

let i = I 99 // use the "I" constructor
let b = B true // use the "B" constructor

let myTup = Tup (2,99) // use the "Tup" constructor
let myP = P {first = "Al"; last = "Jones"} // use the "P" constructor

type C = Circle of int | Rectangle of int * int

[1..10]
|> List.map Circle

[1..10]
|> List.zip [21..30]
|> List.map Rectangle

type IntOrBool1 = I of int | B of bool
type IntOrBool2 = I of int | B of bool

let x1 = IntOrBool1.I 99
let x2 = IntOrBool2.B true

// "deconstruction" of union type
let matcher x =
    match x with
    | Tup (x, y) ->
        printfn "Tuple matched with %i %i" x y
    | P {first = f; last = l} ->
        printfn "Person matched with %s %s" f l
    | L l ->
        printfn "int list matched with %A" l
    | U u ->
        printfn "IntOrBool matched with %A" u

matcher myTup
matcher myP

type Directory =
    | Root // no need to name the root
    | Subdirectory of string // other directories  need to be named

type Size = Small | Medium | Large // "enum style" union

let myDir1 = Root
let myDir2 = Subdirectory "bin"

let mySize1 = Small
let mySize2 = Medium

type CustomerId = CustomerId of int // define a union type
type OrderId = OrderId of int // define another union type

let printOrderId (OrderId orderId) = // deconstruct in the param
    printfn "The orderId is %i" orderId

// try it
let orderId = OrderId 1 // create an order id
printOrderId orderId

let printCustomerId (CustomerId customerId) =
    printfn "The customerId is %i" customerId

// try it
let custId = CustomerId 1
printCustomerId custId

type TypeAlias = A // type alias!
type SingleCase = | A // single case union type

type Contact = Email of string | Phone of int

let email1 = Email "bob@example.com"
let email2 = Email "bob@example.com"

email1 = email2
printfn "%A" email1