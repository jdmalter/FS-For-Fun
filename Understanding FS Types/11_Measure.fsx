[<Measure>] type cm
[<Measure>] type inch

let x = 1<cm> // int
let y = 1.0<cm> // float
let z = 1.0m<cm> // decimal

[<Measure>] type m
[<Measure>] type sec
[<Measure>] type kg

let distance = 1.0<m>
let time = 2.0<sec>
let speed = 2.0<m/sec>
let acceleration = 2.0<m/sec^2>
let force = 5.0<kg m/sec^2>

[<Measure>] type N = kg m/sec^2
let force1 = 5.0<kg m/sec^2>
let force2 = 5.0<N>
force1 = force2 // true

[<Measure>] type foot
let distance1 = 3.0<foot>
let distance2 = distance1 * 2.0 // type inference for result

let addThreeFeet ft = // type inference for input and output
    ft + 3.0<foot>

let untypedTimesThree (ft:float) =
    ft * 3.0

let footTimesThree (ft:float<foot>) =
    ft * 3.0

// dimensionless
let a = 42

// also dimensionless
let b = 42<1>

// test addition
3.0<foot> + 2.0<foot>

// test multiplication
3.0<foot> * 2.0

// conversion factor
let inchesPerFoot = 12.0<inch/foot>

// test
let distanceInFeet =  3.0<foot>
let distanceInInches = distanceInFeet * inchesPerFoot

[<Measure>] type degC
[<Measure>] type degF

let convertDegCToF c =
    c * 1.8<degF/degC> + 32.0<degF>

// test
let f = convertDegCToF 0.0<degC>

let ten = 10.0 // normal

// converting from non-measure  to measure
let tenFeet = ten * 1.0<foot> // with measure
let tenAgain = tenFeet / 1.0<foot> // without measure
let tenAnotherWay = tenFeet * 1.0<1/foot> // without measure

let square (x:int<'u>) = x * x

// test
square 10<foot>

// convert using map -- OK
[1.0..10.0] |> List.map (fun i -> i * 1.0<foot>)

// using a generator -- OK
let feet = [ for i in [1.0..10.0] -> i * 1.0<foot> ]

// OK
feet |> List.sum

// Fixed with generic 0
feet |> List.fold (+) 0.0<_>

// define the function
open LanguagePrimitives
let add1 n =
    n + (FloatWithMeasure 1.0)

// test
add1 10.0<foot>

let add2Int n =
    n + (Int32WithMeasure 2)

// test
add2Int 10<foot>

type Coord<[<Measure>] 'u> =
    { X: float<'u>; Y: float<'u>; }

// test
let coord = { X = 10.0<foot>; Y = 2.0<foot> }

type CurrencyRate<[<Measure>] 'u, [<Measure>] 'v> =
    { Rate: float<'u/'v>; Date: System.DateTime }

// test
[<Measure>] type EUR
[<Measure>] type USD
[<Measure>] type GBP

let mar1 = System.DateTime(2012,3,1)
let eurToUsdOnMar1 = { Rate = 1.2<USD/EUR>; Date = mar1 }
let eurToGbpOnMar1 = { Rate = 0.8<GBP/EUR>; Date = mar1 }

let tenEur = 10.0<EUR>
let tenEurInUsd = eurToUsdOnMar1.Rate * tenEur

type ProductPrice<'product, [<Measure>] 'currency> =
    { Proudct: 'product; Price: float<'currency>; }