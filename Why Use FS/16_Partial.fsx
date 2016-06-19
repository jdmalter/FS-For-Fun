// define a adding function
let add x y = x + y

// normal use
let z = add 1 2

let add42 = add 42

// use the new function
add42 2
add42 3

let genericLogger before after anyFunc input =
    before input // callback for custom behavior
    let result = anyFunc input // evalute the function
    after result // callback for custom behavior
    result // return the result

let add1 input = input + 1

// reuse case 1
genericLogger
    (fun x -> printf "before=%i. " x) // function to call before
    (fun x -> printfn " after=%i." x) // function to call after
    add1 // main function
    2 // parameter

// reuse casae 2
genericLogger
    (fun x -> printf "started with=%i " x) // different callback
    (fun x -> printfn " ended with=%i" x) // function to call after
    add1 // main function
    2 // parameter

let add1WithConsoleLogging =
    genericLogger
        (fun x -> printf "input=%i. " x)
        (fun x -> printfn " result=%i" x)
        add1
        // Last parameter NOT defined here yet

// test
add1WithConsoleLogging 2
[1..5] |> List.map add1WithConsoleLogging