let StandaloneSubexpression aBool =
    if aBool then 42 else 0

let IfThenElseExpression aBool =
    let result = StandaloneSubexpression aBool
    printfn "result=%i" result

let LoopExpression =
    [1..3] |> List.sum