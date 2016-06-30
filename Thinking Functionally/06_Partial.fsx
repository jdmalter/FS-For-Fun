// create an "adder" by partial application of add
let add42 = (+) 42 // partial application
add42 1
add42 3

// create a new list by applying the add42 function to each element
[1;2;3] |> List.map add42

// create a "tester" by partial application of "less than"
let twoIsLessThan = (<) 2 // partial application
twoIsLessThan 1
twoIsLessThan 3

// filter each element with the twoIsLessThan function
[1;2;3] |> List.filter twoIsLessThan

// create a "printer" by partial application of printfn
let printer = printfn "printing param=%i"

// Loop over each element and call the printer function
[1;2;3] |> List.iter printer

// an example using List.map
let add1 = (+) 1
let add1ToEach = List.map add1 // fix the "add1" function

// test
add1ToEach [1;2;3;4]

// an example using List.filter
let filterEvens = List.filter (fun i -> i%2 = 0) // fix the filter function

// test
filterEvens [1;2;3;4]

// create an adder that supports a pluggable logging function
let adderWithPluggableLogger logger x y =
    logger "x" x
    logger "y" y
    let result = x + y
    logger "x+y" result
    result

// create a logging function that writes to the console
let consoleLogger argName argValue =
    printfn "%s=%A" argName argValue

// create an adder with the console logger partially applied
let addWithConsoleLogger = adderWithPluggableLogger consoleLogger
addWithConsoleLogger 1 2
addWithConsoleLogger 42 99

// create a logging function that creates popup windows
let popupLogger argName argValue =
    let message = sprintf "%s=%A" argName argValue
    System.Windows.Forms.MessageBox.Show(
        text=message,caption="Logger")
    |> ignore

// create an adder with the popup logger partially applied
let addWithPopupLogger = adderWithPluggableLogger popupLogger
addWithPopupLogger 1 2
addWithPopupLogger 42 99

// create another adder with 42 baked in
let add42WithConsoleLogger = addWithConsoleLogger 42
[1;2;3] |> List.map add42WithConsoleLogger
[1;2;3] |> List.map add42

List.map (fun i -> i+1) [0;1;2;3]
List.filter (fun i -> i>1) [0;1;2;3]
List.sortBy (fun i -> -i) [0;1;2;3]

let eachAdd1 = List.map (fun i -> i+1)
eachAdd1 [0;1;2;3]

let excludeOneOrLess = List.filter (fun i -> i>1)
excludeOneOrLess [0;1;2;3]

let sortDesc = List.sortBy (fun i -> -i)
sortDesc [0;1;2;3]

// piping using list functions
let result =
   [1..10]
   |> List.map (fun i -> i+1)
   |> List.filter (fun i -> i>5)

let compositeOp = List.map (fun i -> i+1)
                  >> List.filter (fun i -> i>5)
compositeOp [1..10]

// create wrapper for .NET string functions
let replace oldStr newStr (s:string) =
    s.Replace(oldValue=oldStr, newValue=newStr)

let startWith lookFor (s:string) =
    s.StartsWith(lookFor)

"hello" |> replace "h" "j" |> startWith "j"

["the"; "quick"; "brown"; "fox"] |> List.filter (startWith "f")

// allows function arguments in front of the function
let (|>) x f = f x

let doSomething x y z = x+y+z
doSomething 1 2 3 // all parameters after function

let doSomethingCurried x y =
    let intermediateFn z = x+y+z
    intermediateFn // return intermediateFn

let doSomethingPartial = doSomething 1 2
doSomethingPartial 3 // only one parameter after function now
3 |> doSomethingPartial // same as above - last parameter piped in

"12" |> int // parses string "12" to an int
1 |> (+) 2 |> (*) 3 // chain of arithmetic

// inverse of pipe
let (<|) f x = f x

printf "%i" (1+2)
printf "%i" <| 1 + 2

let add x y = x + y
1+2 |> add <| 3+4