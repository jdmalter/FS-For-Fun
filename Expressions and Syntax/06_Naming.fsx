﻿let primesUpTo n =
    let rec sieve l =
        match l with
        | [] -> []
        | p::xs ->
            p :: sieve [for x in xs do if (x % p) > 0 then yield x]
    [2..n] |> sieve

// test
primesUpTo 100