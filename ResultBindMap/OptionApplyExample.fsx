module Option =
    let apply fOpt xOpt =
        match fOpt,xOpt with
        | Some f, Some x -> Some <| f x
        | _ -> None

let someAdd = Some (+)

let (<*>) = Option.apply

someAdd <*> Some 2 <*> Some 1