namespace LearningTest

[<RequireQualifiedAccess>]
module Result =
    /// Pass in a function to handle each case of `Result`
    let bimap onSuccess onError xR =
       match xR with
        | Ok x -> onSuccess x
        | Error e -> onError e

    let map = Result.map
    let mapError = Result.mapError
    let bind = Result.bind

    // Like `map` but with a unit-returning function
    let iter (f : _ -> uint) result =
        map f result |> ignore

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

    /// Lift a two parameter function to use Result parameters
    let lift2 f x1 x2 =
        let (<!>) = map
        let (<*>) = apply
        f <!> x1 <*> x2