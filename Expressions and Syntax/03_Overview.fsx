let e1 = 1 // literal
let e2 = [1;2;3] // list expression
let e3 = -2 // prefix operator
let e4 = 2 + 2 // infix operator
let e5 = "string".Length // dot lookup
let e7 = printf "hello" // function application

let x1 = fun () -> 1 // lambda expression

let x2 = match 1 with // match expression
            | 1 -> "a"
            | _ -> "b"

let x3 = if true then "a" else "b" // if-then-else

let x4 = for i in [1..10] // for loop
            do printf "%i" i

let x5 = try // exception handling
            let result = 1 / 0
            printfn "%i" result
          with
            | e ->
                printfn "%s" e.Message

let x6 = let n = 1 in n + 2 // let expression

let f x = printfn "x=%i" x; x + 1 // all on same line with ";"
let x = ignore 1;2 // ok since 1 is ignored

// create a clone of if-then-else
let test b t f = if b then t else f

// call it with two different choices
test true (printfn "true") (printfn "false") 
// true and false are printed since subexpressions are evaluated as seen

// create a clone of if-then-else that accepts functions rather than simple values
let testLazy b t f = if b then t() else f()

// call it with two different functions
testLazy true (fun () -> printfn "true") (fun () -> printfn "false")

// ...but call it with lazy values
let f = test true (lazy (printfn "true")) (lazy (printfn "false"))
f.Force() // use Force() to force the evaluation of a lazy value