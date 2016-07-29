let f x =
    match x with
    | 1 -> "a"
    | _ -> "b"

let g l =
    match l with
    | [] -> printfn "is empty"
    | x::_ when x > 0 -> printfn "first element is > 0"
    | x::_ -> printfn "first element is <= 0"

let posNeg x = if x > 0 then "+" elif x < 0 then "-" else "0"
[-5..5] |> List.map posNeg

let greetings =
    if (System.DateTime.Now.Hour < 12)
    then (fun name -> "good morning, " + name)
    else (fun name -> "good day, " + name)

// test
greetings "Jacob"

[1..10] |> List.iter (printf "%i ")

let sum l = List.reduce (+) l

// test
sum [1..10]

let printRandomNumbersUntilMatched matchValue maxValue =
    let randomNumberGenerator = new System.Random()
    let sequenceGenerator _ = randomNumberGenerator.Next(maxValue)
    let isNotMatch = (<>) matchValue

    // create and process the sequence of rands
    Seq.initInfinite sequenceGenerator
        |> Seq.takeWhile isNotMatch
        |> Seq.iter (printf "%d ")

    // done
    printfn "\nFound a %d!" matchValue

// test
printRandomNumbersUntilMatched 10 20

let myList = [for x in 0..10 do if x*x < 100 then yield x]