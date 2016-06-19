// define the square function
let square x = x * x

// test
let s2 = square 2
let s3 = square 3
let s4 = square 4

// define the sumOfSquares function
let sumOfSquares n =
    [1..n] |> List.map square |> List.sum

// try it
sumOfSquares 100