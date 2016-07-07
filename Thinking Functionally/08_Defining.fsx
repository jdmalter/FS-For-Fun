// let add x y = x + y
let addLambda = fun x y -> x + y

// with separately defined function
let add1 i = i + 1
[1..10] |> List.map add1

// inlined without separately defined function
[1..10] |> List.map (fun i -> i + 1)

// definition using lamda
let adderGenerator = fun x -> fun y -> x + y

// test
let add2 = adderGenerator 2
add2 5

type Name = {first:string; last:string} // define a new type
let bob = {first="bob"; last="smith"} // define a value

// single parameter style
let f1 name = // pass in single parameter
    let {first=f; last=l} = name // extract in body of function
    printfn "first=%s; last=%s" f l

// match in the parameter itself
let f2 {first=f; last=l} = // direct pattern matching
    printfn "first=%s; last=%s" f l

// test
f1 bob
f2 bob

// a function that takes two distinct parameters
let addTwoParams x y = x + y

// a function that takes a single tuple parameter
let addTuple aTuple =
    let (x,y) = aTuple
    x + y

// another function that takes a single tuple parameter but looks like it takes two ints
let addConfusingTuple (x,y) = x + y

// test
addTwoParams 1 2 // ok - uses spaces to separate args
addTuple (1,2) // ok
addConfusingTuple (1,2) // ok

let x = (1,2)
addTuple x // ok

let y = 1,2
addTuple y
addConfusingTuple y

// val f : x:int * y:int * z:int -> int
let f (x,y,z) = x + y * z

// test
f (1,2,3)

// correct
System.String.Compare("a","b")

// create a wrapper function
let strcmp x y = System.String.Compare(x,y)

// partially apply it
let strcmpWithB = strcmp "B"

// use it with a higher order function
["A";"B";"C"] |> List.map strcmpWithB

// Pass in two numbers for addition. The numbers are independent, so use two parameters.
let add x y = x + y

// Pass in two numbers as a geographical co-ordinate. The numbers are dependent, so group them into a tuple or record.
let locateOnMap (xCoord,yCoord) = () // do something

// Set first and last name for a customer. The values are dependent, so group them into a record.
type CustomerName = {First:string; Last:string}
let setCustomerName aCustomerName = () // good

let sayHello() = printfn "Hello World!" // good
sayHello()

// define
let (.*%) x y = x + y + 1
let ( *+* ) x y = x + y + 1
2 .*% 3

let (~%%) (s:string) = s.ToCharArray()
%% "hello"

let sumExplicit list = List.reduce (fun sum e -> sum+e) list // explicit
let sum = List.reduce (+) // point free

let I x = x // identity function
let K x y = x // ignore second parameter
let M x = x >> x // apply twice
let T x y = y x // equivalent to |>
let Q x y z = y (x z) // equivalent to >>
let S x y z = x z (y z) // 
let rec Y f x = f (Y f) x // recursive

let rec fib i =
    match i with
    | 1 -> 1
    | 2 -> 1
    | n -> fib(n-1) + fib(n-2)