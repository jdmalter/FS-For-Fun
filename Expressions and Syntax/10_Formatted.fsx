open System
open System.IO

// partial application - explicit paramters
let printStringAndInt s i = printfn "A string: %s. An int: %i" s i
let printHelloAndInt i = printStringAndInt "Hello" i
do printHelloAndInt 42

// partial application - point free style
let printInt = printfn "An int: %i"
do printInt 42

let doSomething printerFn x y =
    let result = x + y
    printerFn "result is" result

let callback = printfn "%s %i"
do doSomething callback 3 4

[1..5] |> List.map (sprintf "i=%i")

// tuple printing
let t = (1,2)
Console.WriteLine("A tuple: {0}", t)
printfn "A tuple: %A" t

// record printing
type Person = {First:string; Last:string}
let johnDoe = {First="John"; Last="Doe"}
Console.WriteLine("A record: {0}", johnDoe)
printfn "A record: %A" johnDoe

// union types printing
type Temperature = F of int | C of int
let freezing = F 32
Console.WriteLine("A union: {0}", freezing)
printfn "A union: %A" freezing

let format:Printf.TextWriterFormat<_> = "A string: %s"
printfn format "Hello"

let formatAString = "A string: %s"
let formatAStringAndInt = "A string: %s. An int: %i"

//convert to TextWriterFormat
let twFormat1 = Printf.TextWriterFormat<string->unit>(formatAString)
printfn twFormat1 "Hello"
let twFormat2 = Printf.TextWriterFormat<string->int->unit>(formatAStringAndInt)
printfn twFormat2 "Hello" 42

printfn "escape %%"

let rows = [(1,"a"); (-22,"bb"); (333,"ccc"); (-4444,"dddd")]

// no alignment
for (i,s) in rows do
    printfn "|%i|%s|" i s

// with alignment
for (i,s) in rows do
    printfn "|%5i|%5s|" i s

// with left alignment for column 2
for (i,s) in rows do
    printfn "|%5i|%-5s|" i s

// with dynamic column width=20 for column 1
for (i,s) in rows do
    printfn "|%*i|%-5s|" 20 i s

// with dynamic column width for column 1 and column 2
for (i,s) in rows do
    printfn "|%*i|%-*s|" 20 i 10 s

printfn "signed8: %i unsigned8: %u" -1y -1y
printfn "signed16: %i unsigned16: %u" -1s -1s
printfn "signed32: %i unsigned32: %u" -1 -1
printfn "signed64: %i unsigned64: %u" -1L -1L
printfn "uppercase hex: %X lowercase hex: %x octal: %o" 255 255 255
printfn "byte: %i " 'A'B

let pi = 3.14
printfn "float: %f exponent: %e compact: %g" pi pi pi

let petabyte = pown 2.0 50
printfn "float: %f exponent: %e compact: %g" petabyte petabyte petabyte

let largeM = 123456789.123456789M // a decimal
printfn "float: %f decimal: %M" largeM largeM

printfn "2 digits precision: %.2f. 4 digits precision: %.4f." 123.456789 123.456789
// output => 2 digits precision: 123.46. 4 digits precision: 123.4568.
printfn "2 digits precision: %.2e. 4 digits precision: %.4e." 123.456789 123.456789
// output => 2 digits precision: 1.23e+002. 4 digits precision: 1.2346e+002.
printfn "2 digits precision: %.2g. 4 digits precision: %.4g." 123.456789 123.456789
// output => 2 digits precision: 1.2e+02. 4 digits precision: 123.5.

printfn "|%f|" pi // normal   
printfn "|%10f|" pi // width
printfn "|%010f|" pi // zero-pad
printfn "|%-10f|" pi // left aligned
printfn "|%0-10f|" pi // left zero-pad

//define the function using a closure
let printRand =
    let rand = new Random()
    // return the actual printing function
    fun (tw:TextWriter) -> tw.Write(rand.Next(1,100))

//test it
for i in [1..5] do
    printfn "rand = %t" printRand

//define the callback function
//note that the data parameter comes after the TextWriter
let printLatLong (tw:TextWriter) (lat,long) =
    tw.Write("lat:{0} long:{1}", lat, long)

// test it
let latLongs = [(1,2); (3,4); (5,6)]
for latLong in latLongs do
    // function and value both passed in to printfn
    printfn "latLong = %a" printLatLong latLong

// function to format a date
let yymmdd1 (date:DateTime) = date.ToString("yy.MM.dd")

// function to format a date onto a TextWriter
let yymmdd2 (tw:TextWriter) (date:DateTime) = tw.Write("{0:yy.MM.dd}", date)

// test it
for i in [1..5] do
    let date = DateTime.Now.AddDays(float i)

    // using %s
    printfn "using ToString = %s" (yymmdd1 date)
    
    // using %a
    printfn "using a callback = %a" yymmdd2 date

let doAfter s =
    printfn "Done"
    // return the result
    s

let result = Printf.ksprintf doAfter "%s" "Hello"

// a logging library such as log4net or System.Diagnostics.Trace
type Logger(name) =
    
    let currentTime (tw:TextWriter) =
        tw.Write("{0:s}",DateTime.Now)

    let logEvent level msg =
        printfn "%t %s [%s] %s" currentTime level name msg

    member this.LogInfo msg =
        logEvent "INFO" msg

    member this.LogError msg =
        logEvent "ERROR" msg

    static member CreateLogger name =
        new Logger(name)

// my application code
module MyApplication =

    let logger = Logger.CreateLogger("MyApp")

    // create a logInfo using the Logger class
    let logInfo format =
        let doAfter s =
            logger.LogInfo(s)
        Printf.ksprintf doAfter format

    // create a logError using the Logger class
    let logError format =
        let doAfter s =
            logger.LogError(s)
            System.Windows.Forms.MessageBox.Show(s) |> ignore
        Printf.ksprintf doAfter format
    
    // function to exercise the logging
    let test() =
        do logInfo "Message #%i" 1
        do logInfo "Message #%i" 2
        do logError "Oops! an error occurred in my app"

MyApplication.test()