
[<RequireQualifiedAccess>]
module Result =
    let map = Result.map

    // Like `map` but with a unit-returning function
    let iter (f : _ -> unit) result = 
        map f result |> ignore   

let printIt x =
    printfn "%s" x

let ok1 =  Ok "Some OK"

ok1 |> Result.iter printIt 
