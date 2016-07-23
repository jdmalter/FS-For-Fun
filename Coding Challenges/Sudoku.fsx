// basic types and functions

type row = char
type rows = row list

type col = int
type cols = col list

type square = row * col
type squares = square list

let cross (rows:rows) (cols:cols) :squares =
    rows |> List.collect (fun row -> cols |> List.map (fun col -> (row, col)))

let join lists =
    lists |> List.reduce (@)

// values of the basic types

let splitRows:rows list = [['A';'B';'C'];['D';'E';'F'];['G';'H';'I']]
let rows:rows = join splitRows

let splitCols:cols list = [[1;2;3];[4;5;6];[7;8;9]]
let cols:cols = join splitCols

let squares = cross rows cols
let horizontalUnits = rows |> List.map (fun row -> cross [row] cols)
let verticalUnits = cols |> List.map (fun col -> cross rows [col])
let squareUnits = splitRows |> List.collect (fun row -> splitCols |> List.map (fun col -> cross row col))
let unitList = horizontalUnits @ verticalUnits @ squareUnits

// create dictionary of square to units and square to peers

let getUnits (square:square) = 
    unitList
    |> List.filter (fun unit -> unit |> List.contains square)

let getPeers (square:square) =
    getUnits square
    |> join
    |> List.distinct
    |> List.filter (fun other -> other <> square)

let units = dict(squares |> List.map (fun square -> (square, getUnits square)))
let peers = dict(squares |> List.map (fun square -> (square, getPeers square)))

// test

squares |> List.length = 81
unitList |> List.length = 27
squares |> List.forall (fun square -> units.Item square |> List.length = 3)
squares |> List.forall (fun square -> peers.Item square |> List.length = 20)
units.Item ('C', 2) = [
    [('C', 1); ('C', 2); ('C', 3); ('C', 4); ('C', 5); ('C', 6); ('C', 7); ('C', 8); ('C', 9)];
    [('A', 2); ('B', 2); ('C', 2); ('D', 2); ('E', 2); ('F', 2); ('G', 2); ('H', 2); ('I', 2)];
    [('A', 1); ('A', 2); ('A', 3); ('B', 1); ('B', 2); ('B', 3); ('C', 1); ('C', 2); ('C', 3)]]
peers.Item ('C', 2) = [
    ('C', 1); ('C', 3); ('C', 4); ('C', 5); ('C', 6); ('C', 7); ('C', 8); ('C', 9);
    ('A', 2); ('B', 2); ('D', 2); ('E', 2); ('F', 2); ('G', 2); ('H', 2); ('I', 2);
    ('A', 1); ('A', 3); ('B', 1); ('B', 3)]

// grid types

type value = int
type values = value list

type grid = System.Collections.Generic.Dictionary<square,values>

// values of the grid types

let digits:values = [1;2;3;4;5;6;7;8;9]

// eliminate all the values from s except d

let rec assign (grid:grid) (square:square) (value:value) :grid option =

    // eliminates one of the possible values for a square

    let rec eliminate (square:square) (value:value) :grid option =
        match grid.[square] |> List.contains value with
        | true ->
            // remove the digit from the square

            grid.[square] <- grid.[square] |> List.filter (fun otherValue -> otherValue <> value)

            // If a square has only one possible value, then eliminate that value from the square's peers. 

            let ruleOne =
                match grid.[square] |> List.length with
                | 0 ->
                    None
                | 1 ->
                    let successfulElimination = 
                        peers.[square]
                        |> List.forall (fun otherSquare -> (eliminate otherSquare grid.[square].Head).IsSome)
                    match successfulElimination with
                    | true ->
                        Some grid
                    | false ->
                        None
                | _ ->
                    Some grid

            // If a unit has only one possible place for a value, then put the value there.

            let ruleTwo =

                let successfulAssignment =
                    units.[square] 
                    |> List.forall (fun unit ->
                        let digitPlaces = unit |> List.filter (fun square -> grid.[square] |> List.contains value)
                        match digitPlaces |> List.length with
                        | 0 ->
                           false
                        | 1 ->
                           (assign grid digitPlaces.Head value).IsSome
                        | _ ->
                           true)
                match successfulAssignment with
                | true ->
                    Some grid
                | false ->
                    None

            if ruleOne.IsSome then ruleTwo else None
        | false ->
            Some grid

    // was every possible value eliminated from the square?

    let successfulElimination = 
        grid.[square]
        |> List.filter (fun otherValue -> otherValue <> value)
        |> List.forall (fun otherValue -> (eliminate square otherValue).IsSome)

    match successfulElimination with
    | true ->
        Some grid
    | false ->    
        None

// parse a values into a grid

let parseGrid (values:values) :grid option =

    let grid = new System.Collections.Generic.Dictionary<square,values>()
    squares |> List.iter (fun square -> grid.[square] <- digits)

    let successfulAssignment =
        values 
        |> List.zip squares 
        |> List.forall (fun (square,value) -> if digits |> List.contains value then (assign grid square value).IsSome else true)

    match successfulAssignment with
    | true ->
        Some grid
    | false ->
        None

// display a puzzle

let display (grid:grid) =
    let width = 1 + (squares |> List.map (fun square -> grid.[square] |> List.length) |> List.max)
    let lineSegment = new string [|for i in [1..3*width] -> '-'|]
    let line = lineSegment + "+" + lineSegment + "+" + lineSegment

    rows |> List.iter (fun row -> 
        cols |> List.iter (fun col ->

            // print out every col in row
            
            printf "%-*s" width (new string [|for c in grid.[(row, col)] -> c.ToString().[0]|])
            if [3;6] |> List.contains col then printf "|" else ())

        // print out every row in rows

        printfn ""
        if ['C';'F'] |> List.contains row then printfn "%s" line else ())

// test

let grid1 = parseGrid [0;0;3;0;2;0;6;0;0;9;0;0;3;0;5;0;0;1;0;0;1;8;0;6;4;0;0;0;0;8;1;0;2;9;0;0;7;0;0;0;0;0;0;0;8;0;0;6;7;0;8;2;0;0;0;0;2;6;0;9;5;0;0;8;0;0;2;0;3;0;0;9;0;0;5;0;1;0;3;0;0]
if grid1.IsSome then display grid1.Value else printfn "No result"

let grid2 = parseGrid [4;0;0;0;0;0;8;0;5;0;3;0;0;0;0;0;0;0;0;0;0;7;0;0;0;0;0;0;2;0;0;0;0;0;6;0;0;0;0;0;8;0;4;0;0;0;0;0;0;1;0;0;0;0;0;0;0;6;0;3;0;7;0;5;0;0;2;0;0;0;0;0;1;0;4;0;0;0;0;0;0;]
if grid2.IsSome then display grid2.Value else printfn "No result"

// systematically try all possibilities until we hit one that works

let rec search (grid:grid) =

    // is the grid already solved?

    let validateSearch =
        let successfulSearch = squares |> List.forall (fun square -> grid.[square] |> List.length = 1)
        match successfulSearch with
        | true -> Some grid
        | false -> None

    match validateSearch.IsSome with
    | true ->
        Some grid
    | false ->
        let square =
            squares
            |> List.filter (fun square -> grid.[square] |> List.length > 1)
            |> List.minBy (fun square -> grid.[square] |> List.length)
        grid.[square] // take some possible values
        |> List.map (fun digit -> assign (new System.Collections.Generic.Dictionary<square, values>(grid)) square digit) // create versions of the grid with those possible values
        |> List.filter (fun assignedGrid -> assignedGrid.IsSome) // remove assigned grids with contradictions
        |> List.map (fun assignedGrid -> assignedGrid.Value |> search) // search any valid grid
        |> List.tryFind (fun searchedGrid -> searchedGrid.IsSome) // find searched grids without contradictions
        |> fun result -> if result.IsSome then result.Value else None // if there is a valid result, return it

// searches a parsed grid

let solve (values:values) =
    let possibleGrid = values |> parseGrid
    match possibleGrid.IsSome with
    | true -> possibleGrid.Value |> search
    | false -> possibleGrid

// test

let grid3 = solve [8;5;0;0;0;2;4;0;0;7;2;0;0;0;0;0;0;9;0;0;4;0;0;0;0;0;0;0;0;0;1;0;7;0;0;2;3;0;5;0;0;0;9;0;0;0;4;0;0;0;0;0;0;0;0;0;0;0;8;0;0;7;0;0;1;7;0;0;0;0;0;0;0;0;0;0;3;6;0;4;0;]
if grid3.IsSome then display grid3.Value else printfn "No result"

let grid4 = solve [0;0;5;3;0;0;0;0;0;8;0;0;0;0;0;0;2;0;0;7;0;0;1;0;5;0;0;4;0;0;0;0;5;3;0;0;0;1;0;0;7;0;0;0;6;0;0;3;2;0;0;0;8;0;0;6;0;5;0;0;0;0;9;0;0;4;0;0;0;0;3;0;0;0;0;0;0;9;7;0;0;]
if grid4.IsSome then display grid4.Value else printfn "No result"