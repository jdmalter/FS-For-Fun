let intToString x = sprintf "x is %i" x // format int to string
let stringToInt x = System.Int32.Parse(x)

let intToFloat x = float x // "float" fn. converts ints to floats
let intToBool x = (x = 2) // true if x equals 2
let stringToString x = x + " world"

let stringLength (x:string) = x.Length

// higher order function fn:(int -> int) -> int
let evalWith5ThenAdd2 fn = fn 5 + 2 // same as fn(5) + 2

let add1 x = x + 1
evalWith5ThenAdd2 add1

let times3 x = x * 3 // a function of type (int -> int)
evalWith5ThenAdd2 times3 // test it

// val adderGenerator : int -> (int -> int)
let adderGenerator numberToAdd = (+) numberToAdd
let add2 = adderGenerator 2

add2 5 // val it : int = 7

let evalWith5AsInt (fn:int->int) = fn 5
let evalWith5AsFlaot (fn:int->float) = fn 5

// val printInt : int -> unit
let printInt x = printf "x is %i" x // print to console

// val whatIsThis : unit = ()
let whatIsThis = ()

// val printHello : unit = ()
let printHello = printf "hello world" // print to console

// val printHelloFn : unit -> unit
let printHelloFn () = printf "hello world" // print to console

// ignore takes anything and returns the unit type
do (1+1 |> ignore) // ok

// val onAStick : x:'a -> string
let onAStick x = x.ToString() + " on a stick"

onAStick 22
onAStick 3.14159
onAStick "hello"

// val concatString : x':a -> y:'b -> string
let concatString x y = x.ToString() + y.ToString()

// val isEqual : x:'a -> y:'a -> bool
let isEqual x y = (x=y)

let testA = float 2
let testB x = float 2
let testC x = float 2 + x
let testD x = x.ToString().Length
let testE (x:float) = x.ToString().Length
let testF x = printfn "%s" x
let testG x = printfn "%f" x
let testH = 2 * 2 |> ignore
let testI x = 2 * 2 |> ignore
let testJ (x:int) = 2 * 2 |> ignore
let testK = "hello"
let testL() = "hello"
let testM x = x=x
let testN x = x 1 // hint: what kind of thing is x?
let testO x:string = x 1 // hint: what does :string modify? 