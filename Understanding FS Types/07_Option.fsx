let validInt = Some 1
let invalidInt = None

match validInt with
| Some i -> printfn "the valid value is %i" i
| None -> printfn "the value is none"

type SearchResult1 = Option<string> // Explicit C#-style generics
type SearchResult2 = string option // built-in postfix keyword

[1;2;3;4] |> List.tryFind (fun x -> x = 3) // Some 3
[1;2;3;4] |> List.tryFind (fun x -> x = 10) // None

let o1 = Some 42
let o2 = Some 42

o1 = o2
printfn "%A" o1

type OptionalString = string option
type UnionOptionalString = | UnionOptionalString of string option

let x = Some 99
x |> Option.map (fun v -> v * 2)
x |> Option.fold (fun _ v -> v * 2) 0

let len (s:Option<string>) =
    match s with
    | Some s -> s.Length
    | None -> 0