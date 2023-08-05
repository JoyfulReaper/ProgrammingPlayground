module List =
    let mapForLoop f (list : 'a list) =
        [for x in list -> f x]

    // Simple but not optimized implementation
    let rec map f list =
        match list with
        | [] -> []
        | x :: xs -> f x :: map f xs

module Option =
    let map f x =
        match x with
        | Some s -> Some <| f s
        | None -> None

[1 .. 10]
|> List.mapForLoop string

[1 .. 10]
|> List.map string

Some 2
|> Option.map string