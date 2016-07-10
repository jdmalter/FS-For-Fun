type Stack = StackContents of float list

// Push a value on the stack
let push x (StackContents contents) =
    StackContents (x::contents)

// Pop a value from the stack and return it
// and the new stack as a tuple
let pop (StackContents contents) =
    match contents with
    | top::rest ->
        let newStack = StackContents rest
        (top,newStack)
    | [] -> 
        failwith "Stack underflow"

let binary mathFn stack =
    let x,stack' = pop stack // pop the top of the stack
    let y,stack'' = pop stack' // pop the top of the stack again
    let result = mathFn x y // do the math
    push result stack'' // push the result value back on the doubly-popped stack

let unary f stack =
    let x,stack' = pop stack // pop the top of the stack
    push (f x) stack' // push the function value of the stack

let SHOW stack =
    let x,_ = pop stack
    printfn "The answer is %f" x
    stack // keep going with same stack

// Duplicate the top value on the stack
let DUP stack =
    let x,_ = pop stack // get the top of the stack
    push x stack // push it onto the stack again

// Swap the top two values
let SWAP stack = 
    let x,stack' = pop stack  
    let y,stack'' = pop stack'
    push y (push x stack'') 

// Make an obvious starting point
let EMPTY = StackContents []
let START = EMPTY

let ONE = push 1.0
let TWO = push 2.0
let THREE = push 3.0
let FOUR = push 4.0
let FIVE = push 5.0

let ADD = binary (+)
let SUB = binary (-)
let MUL = binary (*)
let DIV = binary (/)

let NEG = unary (fun x -> -x)

let SQUARE = DUP >> MUL
let CUBE = DUP >> DUP >> MUL >> MUL