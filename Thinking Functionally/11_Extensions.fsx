module Person =
    // type with no member initially
    type T = {First:string; Last:string}

    // constructor
    let create first last =
        {First=first; Last=last}

    // standalone function
    let fullName {First=first; Last=last} =
        first + " " + last
    
    // standalone function
    let hasSameFirstAndLastName (person:T) otherFirst otherLast =
        person.First = otherFirst && person.Last = otherLast

    // another member added later
    type T with
        member this.FullName = fullName this
        member this.SortableName =
            this.Last + ", " + this.First
        member this.HasSameFirstAndLastName = hasSameFirstAndLastName this

// test
let person = Person.create "John" "Doe"
let fullname = Person.fullName person // functional style
let fullname2 = person.FullName // OO style
let sortableName = person.SortableName
let result1 = Person.hasSameFirstAndLastName person "bob" "smith" // functional style
let result2 = person.HasSameFirstAndLastName "bob" "smith" // OO style

// in a different module
module PersonExtensions =
    type Person.T with
        member this.UppercaseName =
            this.FullName.ToUpper()

// bring the extension into scope first!
open PersonExtensions

// test
let uppercaseName = person.UppercaseName

type System.Int32 with
    member this.IsEven = this % 2 = 0
    static member IsOdd x = x % 2 = 1

type System.Double with
    static member Pi = 3.141

// test
let i = 20
if i.IsEven then printfn "'%i' is even" i
let result = System.Int32.IsOdd 20
let pi = System.Double.Pi

type Product = {SKU:string; Price:float} with

    // curried style
    member this.CurriedTotal qty discount =
        (this.Price * float qty) - discount

    // no discount
    member this.TupleTotal (qty) =
        this.Price * float qty

    // tuple style
    member this.TupleTotal (qty,discount) =
        // return
        (this.Price * float qty) - discount

// test
let product = {SKU="ABC"; Price=2.0}
let total1 = product.CurriedTotal 10 1.0
let total2 = product.TupleTotal (10,1.0)
let total3 = product.TupleTotal (qty=10,discount=1.0)
let total4 = product.TupleTotal (discount=1.0,qty=10)
let total5 = product.TupleTotal (10)
let total6 = product.TupleTotal (10,1.0)

let totalFor10 = product.CurriedTotal 10
let discounts = [1.0..5.0]
let totalForDifferentDiscounts =
    discounts |> List.map totalFor10