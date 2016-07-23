// character columns
//3456789
let f =
    let x=1 // offside line is at column 3
    let y=1 // this line must start at column 3
    x+y // this line must start at column 3

// character columns
//34567890123456789
let g =   let x=1 // offside line at col 11
          x+1 // must start at column 11 from now on

let h =
    let g = (
     1+2) // first char after "(" defines a new line at col 6
    g

let i =
    if true then
     1+2 // first char after "then" defines a new line at col 6
    else
      0 // first char after "else" defines a new line at col 7

let j =
    match 1 with
    | 1 ->
        1+2 // first char after match "->" defines a new line at col 9
    | _ -> 
        0

// character columns
//34567890123456789
let x = 1 // defines a new line at col 9
      + 2 // "+" allowed to be outside the line
      + 3

let y = 1 // defines a new line at col 9
       + 2 // infix operators that start a line don't count
                * 3 // starts with "*" so doesn't need to align
          - 4 // starts with "-" so doesn't need to align

let z = fun x -> // "fun" should define a new line at col 9
    let y = 1 // but doesn't. The real line starts here.
    x + y