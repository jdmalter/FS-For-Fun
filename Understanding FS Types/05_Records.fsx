type ComplexNumber = { real: float; imaginary: float }
type GeoCoord = { lat: float; long: float }

let myComplexNumber = { real = 1.1; imaginary = 2.2 } // use equals!
let myGeoCoord = { lat = 1.1; long = 2.2 } // use equals in let

let { lat = myLat; long = myLong } = myGeoCoord // deconstruct
let { lat = _; long = myLong2 } = myGeoCoord // deconstruct
let { long = myLong3 } = myGeoCoord // deconstruct

let myGeoCoordA = { lat = 1.1; long = 2.2 }
let myGeoCoordB = { long = 2.2; lat = 1.1 } // same as above

type Person1 = { first: string; last: string }
type Person2 = { first: string; last: string }
let p = { Person1.first = "Alice"; last = "Jones" }
let { Person1.first = f; last = l } = p

module Module1 =
    type Person = { first: string; last: string }

module Module2 =
    type Person = { first: string; last: string }

module Module3 =
    let p = { Module1.Person.first = "Alice";
              Module1.Person.last = "Jones" }

// for the record version, create a type to hold the return result
type TryParseResult = { success: bool; value: int }

// the record version of TryParse
let tryParseRecord intStr =
    try
        let i = System.Int32.Parse intStr
        { success = true; value = i }
    with _ -> { success = false; value = 0 }

// test it
tryParseRecord "99"
tryParseRecord "abc"

// define return type
type WordAndLetterCountResult = { wordCount: int; letterCount: int }

let wordAndLetterCount (s:string) =
    let words = s.Split [|' '|]
    let letterCount = words |> Array.sumBy (fun word -> word.Length)
    { wordCount = words.Length; letterCount = letterCount }

// test
wordAndLetterCount "to be or not to be"

let addOneToGeoCoord { lat = x; long = y } = { lat = x + 1.0; long = y + 1.0 }

// try it
addOneToGeoCoord { lat = 1.0; long = 2.0 }

let g1 = { lat = 1.1; long = 2.2; }
let g2 = { g1 with lat = 99.9 }

let p1 = { first = "Alice"; last = "Jones" }
let p2 = { p1 with last = "Smith" }

{ first = "Alice"; last = "Jones" }.GetHashCode()

printfn "%A" { first = "Alice"; last = "Jones" } // nice
{ first = "Alice"; last = "Jones" }.ToString() // ugly
printfn "%O" { first = "Alice"; last = "Jones" } // ugly