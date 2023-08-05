[<RequireQualifiedAccess>]
module Result =
    /// Pass in a function to handle each case of `Result`
    let bimap onSuccess onError xR =
       match xR with
        | Ok x -> onSuccess x
        | Error e -> onError e



let onSuccess x =
    printfn "It worked: %s" x

let onError x =
    printfn "It didn't work: %s" x

let yes = Ok "Ok"
let no = Error "Error"

yes |> Result.bimap onSuccess onError
no |> Result.bimap onSuccess onError