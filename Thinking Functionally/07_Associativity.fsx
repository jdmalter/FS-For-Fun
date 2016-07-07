let F x y z = x y z
// is equal to F x y z = (x y) z

let f (x:int) = float x * 3.0 // f is int->float
let g (x:float) = x > 4.0 // g is float->bool
let h (x:int) = g ( f(x) )

// test
h 1
h 2

// val compose : ('a -> 'b) -> ('b -> 'c) -> 'a -> 'c
let compose f g x = g ( f(x) )

// new symbol >>
let (>>) f g x = g ( f(x) )

let add1 x = x + 1
let times2 x = x * 2
let add1Times2 = add1 >> times2 // (>>) add1 times2 x

// test
add1Times2 3

let add n x = x + n
let times n x = x * n
let add5Times3 = add 5 >> times 3

// test
add5Times3 1

// val twice : f:('a -> 'a) -> ('a -> 'a)
let twice f = f >> f

// val add1Twice : (int -> int)
let add1Twice = twice add1

// test
add1Twice 9

let add1ThenMultiply = (+) 1 >> (*)

// test
add1ThenMultiply 2 7

let times2Add1 = add 1 << times 2
times2Add1 3

let myList = []
myList |> List.isEmpty |> not // straight pipeline
myList |> (not << List.isEmpty) // using reverse composition

let doSomething x y z = x+y+z
doSomething 1 2 3 // all parameters after function
3 |> doSomething 1 2 // last parameter piped in