let x = int 1.23
let y = float 1
let z = System.Convert.ToBoolean(0) //ok

// create a function with parameter of type Object
let objFunction (o:obj) = o

// test: call with an integer
let result = objFunction 1

let o = box 1
result = o

let i:int = unbox o

let detectTypeBoxed v =
    match box v with // used "box v"
    | :? int -> printfn "this is an int"
    | _ -> printfn "something else"

// test
detectTypeBoxed 1
detectTypeBoxed 3.14