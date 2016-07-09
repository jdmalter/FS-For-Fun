let addThreeNumbers x y z =
    
    // create a nested helper function
    let add n =
        fun x -> x + n

    x |> add y |> add z

// test
addThreeNumbers 2 3 4

let validateSize max n =
    
    // create a nested helper function with no params
    let printError() =
        printfn "Oops: '%i' is bigger than max: '%i'" n max

    // use the helper function
    if n > max then printError()

// test
validateSize 10 9
validateSize 10 11

let sumNumbersUpTo max =
    
    // recursive helpder function with accumulator
    let rec recursiveSum n sumSoFar =
        match n with
        | 0 -> sumSoFar
        | _ -> recursiveSum (n-1) (n+sumSoFar)

    // call helper function with initial values
    recursiveSum max 0

// test
sumNumbersUpTo 10

// wtf does this function do?
let f x = 
    let f2 y = 
        let f3 z = 
            x * z
        let f4 z = 
            let f5 z = 
                y * z
            let f6 () = 
                y * x
            f6()
        f4 y
    x * f2 x

// test
[-4;-3;-2;-1;0;1;2;3;4] |> List.map f // f computes x^3

[<RequireQualifiedAccess>]
module MathStuff =

    let add x y = x + y
    let subtract x y = x - y

    // nested module
    [<RequireQualifiedAccess>]
    module FloatLib =

        let add x y :float = x + y
        let subtract x y :float = x - y

    // type definitions
    type Complex = {r:float; i:float}
    type IntegerFunction = int -> int -> int
    type DegreesOrRadians = Deg | Rad

    // "constant"
    let PI = 3.141

    // "variable"
    let mutable TrigType = Deg

    // initialization / static constructor
    do printfn "module initialized"

module OtherStuff =
    // open MathStuff // make all function accessible
    
    // use a function from the MathStuff module
    let add1 x = MathStuff.add x 1

    // fully qualified
    let add1Float x = MathStuff.FloatLib.add x 1.0

    // with a relative path
    let sub1Float x = MathStuff.FloatLib.subtract x 1.0

// module
module Example =
    
    // declare the type inside a module
    type PersonType = {First:string; Last:string}