let t1 = (2,3)
let t2 = (-2,7)
let t3 = (2,true)
let t4 = (7,false)
let t5 = ("hello",42)
let t6 = ("goodbye",99)
let t7 = (42,true,"hello")

let genericTupleFn tuple =
    let (x,y) = tuple
    printfn "x is %A and y is %A" x y

// define some types
type Person = {First:string; Last:string}
type Complex = float * float
type ComplexComparisonFunction = Complex -> Complex -> int

// define some tuples using them
type PersonAndBirthday = Person * System.DateTime
type ComplexPair = Complex * Complex
type ComplexListAndSortFunction = Complex list * ComplexComparisonFunction
type PairOfIntFunctions = (int->int) * (int->int)

// a function that takes a single tuple parameter
// but looks like it takes two ints
let addConfusingTuple (x, y) = x + y

let z = 1, true, "hello", 3.14 // construct
let z1, z2, z3, z4 = z // deconstruct
let _, z5, _, z6 = z // ignore 1st and 3rd elements

let x = 1, 2
fst x
snd x

let tryParse intStr =
    try
        let i = System.Int32.Parse intStr
        (true, i)
    with _ -> (false,0) // any exception

// test it
tryParse "99"
tryParse "abc"

// return word count and letter count in a tuple
let wordAndLetterCount (s:string) =
    let words = s.Split [|' '|]
    let letterCount = words |> Array.sumBy (fun word -> word.Length)
    (words.Length, letterCount)

// test
wordAndLetterCount "to be or not to be"

let addOneToTuple (x, y, z) = (x+1, y+1, z+1)

// try it
addOneToTuple (1, 2, 3)

(1, 2) = (1, 2) // true
(1, 2, 3, "hello") = (1, 2, 3, "bye") // false
(1, (2, 3), 4)= (1, (2, 3), 4) // true

(1, 2, 3).GetHashCode()
(1, 2, 3).ToString()