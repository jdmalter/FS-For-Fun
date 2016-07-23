let a,b = 1,2
type Person = {First:string; Last:string}
let alice = {First="Alice"; Last="Doe"}
let {First=first} = alice

// pattern amtch the parameters
let add (x,y) = x + y

// test
let aTuple = (1,2)
add aTuple

// standard syntax
let f () =
    let x = 1
    let y = 2
    x + y // the result

// syntax using "in" keyword
let g () =
    let x = 1 in // the "in" keyword is available in F#
        let y = 2 in
            x + y // the result

let h () =
    2 + 2 |> ignore
    let x = 1
    x + 1 // this is the final result


// create a new object that implements IDisposable
let makeResource name =
    { new System.IDisposable 
      with member this.Dispose() = printfn "%s disposed" name }

let exampleUseBinding name =
    use myResource = makeResource name
    printfn "done"

// test
exampleUseBinding "hello"

let usingResource name callback =
    use myResource = makeResource name
    callback myResource
    printfn "done"

let callback aResource = printfn "Resource is %A" aResource
do usingResource "hello" callback

let returnValidResource name =
    // "let" binding here instead of "use"
    let myResource = makeResource name
    myResource // still valid

let testValidResource =
    // "use" binding here instead of "let"
    use resource = returnValidResource "hello"
    printfn "done"

using (makeResource "hello") callback

module TimerExtensions =

    type System.Timers.Timer with
        static member StartWithDisposable interval handler =
            // create the timer
            let timer = new System.Timers.Timer(interval)

            // add the handler and start it
            do timer.Elapsed.Add handler
            timer.Start()

            // return an IDisposable that calls "Stop"
            { new System.IDisposable with
              member disp.Dispose() =
                do timer.Stop()
                do printfn "Timer stopped"}

open TimerExtensions
let testTimerWithDisposable =
    let handler = (fun _ -> printfn "elapsed")
    use timer = System.Timers.Timer.StartWithDisposable 100.0 handler
    System.Threading.Thread.Sleep 500

do printf "logging"
do ( 1+1 |> ignore )

module A =

    module B =
        do printfn "Module B initialized"

    module C =
        do printfn "Module C initialized"

    do printfn "Module A initialized"