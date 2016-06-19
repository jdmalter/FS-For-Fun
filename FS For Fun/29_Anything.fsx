// interface
type IEnumerator<'a> =
    abstract member Current : 'a
    abstract MoveNext : unit -> bool

// abstract base class with virtual methods
[<AbstractClass>]
type Shape() =
    // readonly properties
    abstract member Width: int with get
    abstract member Height: int with get
    // non-virtual method
    member this.BoundingArea = this.Height * this.Width
    // virtual method with base implementation
    abstract member Print : unit -> unit
    default this.Print () = printfn "I'm a shape"

// concrete class that inherits from base class and overrides
type Rectangle(x:int, y:int) =
    inherit Shape()
    override this.Width = x
    override this.Height = y
    override this.Print () = printfn "I'm a rectangle"

// test
let r = Rectangle(2,3)
printfn "The width is %i" r.Width
printfn "The area is %i" r.BoundingArea
r.Print()

type Circle(rad:int) =
    inherit Shape()

    // mutable field
    let mutable radius = rad

    // property overrides
    override this.Width = radius * 2
    override this.Height = radius * 2

    // alternate constructor with default radius
    new() = Circle(10)

    // property with get and set
    member this.Radius
        with get() = radius
        and set(value) = radius <- value

// test constructors
let c1 = Circle() // parameterless ctor
printfn "The width is %i" c1.Width
let c2 = Circle(2)
printfn "The width is %i" c2.Width

// test mutable property
c2.Radius <- 3
printfn "The width is %i" c2.Width

// standard generics
type KeyValuePair<'a,'b>(key:'a, value:'b) =
    member this.Key = key
    member this.Value = value

// generics with constraints
type Container<'a,'b
    when 'a : equality
    and 'b :> System.Collections.ICollection>
    (name:'a,values:'b) =
    member this.Name = name
    member this.Values = values

type Point2D =
    struct
        val X: float
        val Y: float
        new(x: float, y: float) = { X = x; Y = y }
    end

// test
let p = Point2D() // zero initialized
let p2 = Point2D(2.0,3.0) // explicitly initialized

// create a new Exception class
exception MyError of string

try
    let e = MyError("Oops!")
    raise e
with
    | MyError msg ->
        printfn "The exception error was %s" msg
    | _ ->
        printfn "Some other exception"

type System.String with
    member this.StartsWithA = this.StartsWith "A"

// test
let s = "Alice"
printfn "'%s' starts with an 'A' = %A" s s.StartsWithA

type System.Int32 with
    member this.IsEven = this % 2 = 0

// test
let i = 20
if i.IsEven then printfn "%i is even" i

open System
type MyConsole() =
    member this.WriteLine([<ParamArray>] args: Object[]) =
        for arg in args do
            printfn "%A" arg

// test
let cons = new MyConsole()
cons.WriteLine("abc", 42, 3.14, true)

type MyButton() =
    let clickEvent = new Event<_>()

    [<CLIEvent>]
    member this.OnClick = clickEvent.Publish

    member this.TestEvent(arg) =
        clickEvent.Trigger(this, arg)

// test
let myButton = new MyButton()
myButton.OnClick.Add(fun (sender,arg) ->
    printfn "Click event with arg=%O" arg)
myButton.TestEvent("Hello World!")

// delegates
type MyDelegate = delegate of int -> int
let f = MyDelegate (fun x -> x * x)
let result = f.Invoke(5)

// enums
type Color = | Red=1 | Green=2 | Blue=3

let color1 = Color.Red // simple assignment
let color2:Color = enum 2 // cast from int
// create from parsing a string
let color3 = System.Enum.Parse(typeof<Color>,"Green") :?> Color // :?> i s downcast

[<System.FlagsAttribute>]
type FileAccess = | Read=1 | Write=2 | Execute=4
let fileaccess = FileAccess.Read ||| FileAccess.Write

open System.Windows.Forms

let form = new Form(Width=400,Height=300,Visible=true,Text="Hello World")
form.TopMost <- true
form.Click.Add (fun args -> printfn "the form was clicked")
form.Show()