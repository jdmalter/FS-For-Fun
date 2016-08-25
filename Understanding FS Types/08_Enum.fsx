type SizeUnion = Small | Medium | Large // union
type ColorEnum = Red = 0 | Yellow = 1 | Blue = 2 // enum

[<System.FlagsAttribute>]
type PermissionFlags = Read = 1 | Write = 2 | Execute = 4
let permission = PermissionFlags.Read ||| PermissionFlags.Write

let small = Small // Unions do not need to be qualified
let red = ColorEnum.Red // Enums must be qualified

let redInt = int ColorEnum.Red // cast from the underlying int type
let redAgain:ColorEnum = enum redInt // cast to a specified enum type
let yellowAgain = enum<ColorEnum>(1) // or create directly
let unknownColor = enum<ColorEnum>(99) // valid

let values = System.Enum.GetValues(typeof<ColorEnum>)
let redFromString =
    System.Enum.Parse(typeof<ColorEnum>,"Red")
    :?> ColorEnum // downcast needed

let qualifiedMatch x =
    match x with
    | ColorEnum.Red -> printfn "red" // qualified name used
    | _ -> printfn "something else" // includes unknown color!