let square x = x * x

// functions as values
let squareclone = square
let result = [1..10] |> List.map squareclone

// functions taking other functions as parameters
let execFunction aFunc aParam = aFunc aParam
let result2 = execFunction square 12

// ======== Product Types ========
// declare it
type IntAndBool = {intPart: int; boolPart: bool}

// use it
let x = {intPart=1; boolPart=false}

// ======== Sum Types ========
// declare it
type IntOrBool =
    | IntChoice of int
    | BoolChoice of bool

// use it
let y = IntChoice 42
let z = BoolChoice true

// ======== Pattern Matching ========
// replacement of if-then-else
let ifThenElse booleanExpression =
    match booleanExpression with
        | true -> true // true branch
        | false -> false // false branch

// replacement of switch
let switch aDigit =
    match aDigit with
        | 1 -> 1 // Case when digit=1
        | 2 -> 2 // Case when digit=2
        | _ -> 0 // Case otherwise

// replacement of loops
let loop aList =
    match aList with
        | [] -> [] // Empty case
        | first::rest -> first::rest // Case with at least one element.  Process first element,
                     // and then call recursively with the rest of the list

type Shape = // define a union of alternative structures
    | Circle of int
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int)

let draw shape = // define a function "draw" with a shape parameter
    match shape with
    | Circle radius ->
        printfn "The circle has a radius of %d" radius
    | Rectangle (height,width) ->
        printfn "The rectangle is %d high and %d wide" height width
    | Polygon points ->
        printfn "The polygon is made of these points %A" points
    | _ -> printfn "I don't recognize this shape"

let circle = Circle(10)
let rect = Rectangle(4,5)
let polygon = Polygon( [(1,1); (2,2); (3,3)] )
let point = Point(2,3)

[circle; rect; polygon; point] |> List.iter draw