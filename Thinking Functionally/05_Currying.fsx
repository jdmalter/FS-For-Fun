// normal version
let printTwoParameters x y =
    printfn "x=%i y=%i" x y

// explicitly curried version
let printTwoParametersCurried x = // only one parameter!
    let subFunction y =
        printfn "x=%i y=%i" x y // new function with one parameter
    subFunction // return the subfunction

// eval with one argument
printTwoParameters 1

// get back a function!
// val it : (int -> unit) = <fun:it@12-1>

// step by step version
let x = 6
let y = 99
let intermediateFnPrint = printTwoParameters x // return fn with x "baked in"
let resultPrint = intermediateFnPrint y

// inline version of above
resultPrint = (printTwoParameters x) y

// normal version
resultPrint =  printTwoParameters x y

// normal version
let addTwoParameters x y =
    x + y

// explicitly curried version
let addTwoParametersCurried x = // only one parameter!
    let subFunction y =
        x + y // new function with one parameter
    subFunction // return the subfunction

// now use it step by step
let intermediateFnAdd = addTwoParameters x // return fn with x "baked in"
let resultAdd = intermediateFnAdd y

// normal version
resultAdd = addTwoParameters x y

// using plus as a single value function
let intermediateFnPlus = (+) x // return add with x "baked in"
let resultPlus = intermediateFnPlus y

// using plus as a function with two parameter
resultPlus = (+) x y

// normal version of plus as infix operator
resultPlus = x + y

// normal version of multiply
let resultMultiply = 3 * 5

// multiply as a one parameter function
let intermediateFnMultiply = (*) 3 // return multiply with 3 "baked in"
resultMultiply = intermediateFnMultiply 5

// create a function
let printHello() = printfn "hello"

// create a function
printHello()