type A = int * int
type B = {FirstName:string; LastName:string}
type C = Circle of int | Rectangle of int * int
type D = Day | Month | Year
type E<'a> = Choice1 of 'a | Choice2 of 'a * 'a

type MyClass(initX:int) =
    let x = initX
    member this.Method() = printf "x=%i" x

module sub =
    // type declared in a module
    type A = int * int

    module private helper =
        // type declared in a submodule
        type B = B of string list

        // internal access is allowed
        let b = B ["a";"b"]

let a = (1,1) // "construct"
let (a1,a2) = a // "deconstruct"

let b = {FirstName="Bob"; LastName="Smith"} // "construct"
let {FirstName = b1} = b // "deconstruct"

let c = Circle 99 // "construct"
match c with
| Circle c1 -> printf "circle of radius %i" c1 // "deconstruct"
| Rectangle (c2,c3) -> printf "%i %i" c2 c3 // "deconstruct"

let c' = Rectangle (2,1) // "construct"
match c' with
| Circle c1 -> printf "circle of radius %i" c1 // "deconstruct"
| Rectangle (c2,c3) -> printf "%i %i" c2 c3 // "deconstruct"

let d = Month
let e = Choice1 "a"
let myVal = MyClass 99
myVal.Method()