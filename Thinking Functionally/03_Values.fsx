// val add1 : x:int -> int
let add1 x = x + 1

// this function always gives the same output value for a given input value
// this function has no side effects

add1 5
// replace "x" with "5"
// add1 5 = 5 + 1 = 6
// result is 6

// x is not a varialbe that changes; x is a value, 5
let plus1 = add1
// replaces every "plus1" with "add1"
plus1 5

// constant operation; val c : int = 5
let c = 5

// constant function; val e : unit -> int
let d = fun()->5
// or
let e() = 5

// define variants of existing keywords
let if' b t f = if b then t else f