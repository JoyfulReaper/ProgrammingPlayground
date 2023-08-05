module Option =
    let bind f xOpt =
        match xOpt with
        | Some x -> f x
        | None -> None

let (>>=) x f = Option.bind f x

let isPositive num =
    match num with
    | 0 -> None
    | x -> Some (x > 0)

Some 2
>>= isPositive
