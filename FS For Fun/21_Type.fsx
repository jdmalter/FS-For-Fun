// define a "safe" email addres type
type EmailAddress = EmailAddress of string

// define a function that uses it
let sendEmail (EmailAddress email) =
    printfn "sent an email to %s" email

// try to send one
let aliceEmail = EmailAddress "alice@example.com"
sendEmail aliceEmail

// try to send a plain string
// sendEmail "bob@example.com" // error

// type-safe formatting
let printingExample =
    printf "an int %i" 2 // ok
    printf "a string %s" "hello" // ok
    printf "an int %i and a string %s" 2 "hello" // ok

let printAString x = printf "%s" x
let printAnInt x = printf "%i" x

// the result is:
// val printAString : string -> unit
// val printAnInt : int -> unit

// define some measures (associated with floats)
[<Measure>]
type cm

[<Measure>]
type inches

// why didn't we do let feet = 12.0<inches> ?
// i know its an exmaple, but it is still a little convoluted
[<Measure>]
type feet =
    // add a conversion function
    static member toInches(feet:float<feet>) : float<inches> =
        feet * 12.0<inches/feet>

// define some values
let meter = 100<cm>
let yard = 3.0<feet>

// convert to different measure
let yardInInches = feet.toInches(yard)

// now define some currencies
[<Measure>]
type GBP

[<Measure>]
type USD

let gbp10 = 10.0<GBP>
let usd10 = 10.0<USD>
gbp10 + gbp10 // allowed: same type
usd10 + 1.0<_> // allowed: wildcard

// deny comparsion
[<NoEquality; NoComparison>]
type CustomerAccount = {CustomerAccountId: int}

let x = {CustomerAccountId = 1}

// x = x // error!
x.CustomerAccountId = x.CustomerAccountId // no error