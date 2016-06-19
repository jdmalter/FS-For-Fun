﻿let add1 input = input + 1
let times2 input = input * 2

let genericLogger anyFunc input =
    printfn "input is %A" input // Log the input
    let result = anyFunc input // Evaluate the function
    printfn "result is %A" result // Log the result
    result // Return the result

let add1WithLogging = genericLogger add1
let times2WithLogging = genericLogger times2

// test
add1WithLogging 3
times2WithLogging 3

[1..5] |> List.map add1WithLogging

let genericTimer anyFunc input =
    let stopwatch = System.Diagnostics.Stopwatch()
    stopwatch.Start()
    let result = anyFunc input // evaluate the function
    printfn "elapsed ms in %A" stopwatch.ElapsedMilliseconds
    result

let add1WithTimer = genericTimer add1WithLogging

// test
add1WithTimer 3

// Strategy design pattern
type Animal(noiseMakingStrategy) =
    member this.MakeNoise =
        noiseMakingStrategy() |> printfn "Making noise %s"

// now create a cat
let meowing() = "Meow"
let cat = Animal(meowing)
cat.MakeNoise

// .. and a dog
let woofOrBark() = if (System.DateTime.Now.Second % 2 = 0)
                   then "Woof" else "Bark"
let dog = Animal(woofOrBark)
dog.MakeNoise // try again a second later