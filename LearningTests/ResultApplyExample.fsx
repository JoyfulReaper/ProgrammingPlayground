[<RequireQualifiedAccess>]
module Result =
    /// Apply a Result<fn> to a Result<x> monadically
    let apply fR xR =
        match fR, xR with
        | Ok f, Ok x -> Ok (f x)
        | Error err1, Ok _ -> Error err1
        | Ok _, Error err2 -> Error err2
        | Error err1, Error _ -> Error err1

let testFn x  =
    sprintf "%s" x

let testFnR  =
    Ok testFn

let testR = Ok "test"

let result : Result<string, string> = Result.apply testFnR testR