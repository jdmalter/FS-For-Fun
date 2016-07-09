[1;2;3] // a normal expression
type intlist = int list // a type expression

Some 1 // a normal expression
type intoption = int option // a type expression

(1,"a") // a normal expression
type intstring = int * string // a type expression

// expression syntax // type syntax
let add1 x = x + 1 // int -> int
let add x y = x + y // int -> int -> int
let print x = printf "%A" x // 'a -> unit
System.Console.ReadLine // unit -> string
[0] |> List.sum // 'a list -> a
List.filter // ('a -> bool) -> 'a list -> 'a list
List.map // ('a -> 'b) -> 'a list -> 'b list

type Adder = int -> int
type AdderGenerator = int -> Adder

let a:AdderGenerator = fun x -> (fun y -> x + y)
let c = fun (x:float) -> (fun y -> x + y)

let testA x = x + x
let testB x y = x + y
let testC x = testB x
let testD (x:int->int) = x 0
let testE x y z = x + y + z
let testF (x:int->int) = x >> (*) -1
let testG x y = y(x + x) * y(x + x)
let testH x = x 0 1 + x 2 3