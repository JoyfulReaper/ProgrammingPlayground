module Result =
    let map = Result.map

    /// Apply a Result<fn> to a Result<x> monadically
    let apply fR xR =
        match fR, xR with
        | Ok f, Ok x -> Ok (f x)
        | Error err1, Ok _ -> Error err1
        | Ok _, Error err2 -> Error err2
        | Error err1, Error _ -> Error err1

    // combine list of result, monadically
    let sequence aListOfResults =
        let (<*>) = apply
        let (<!>) = map
        let cons head tail = head::tail
        let consR headR tailR = cons <!> headR <*> tailR
        let initialValue = Ok [] // empty list inside Result

        // loop through the list, prepending each element
        // to the initial value
        List.foldBack consR aListOfResults initialValue

let aListOfResults =
    [Ok 1; Ok 2; Ok 3; Error "Error"];

let aListOfResults' : Result<int, string> list =
    [Ok 1; Ok 2; Ok 3; Ok 4]

let result = Result.sequence aListOfResults
let result' = Result.sequence aListOfResults'

let printResults result =
    match result with
    | Ok x ->
        printfn "All Results OK!"
        List.iter (fun y -> printf "%O " y) x
        printfn ""
    | _ ->
        printfn "You've got errors!"

result |> printResults
result' |> printResults