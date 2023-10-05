type total = Total of int

let test = Total 1

let deconstructTotal total =
    let (Total value) = total
    value
    
test
    |> deconstructTotal
    |> printfn "The total is: %d"




let value (Total v) = v

test |> value