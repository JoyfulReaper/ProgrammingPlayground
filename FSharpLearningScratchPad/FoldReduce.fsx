let sumOfSquares numbers =
    numbers |> List.fold (fun acc v -> acc + v * v) 0

let r1 = [1..3] |> sumOfSquares

// Reduce is a specific case of fold where the list is assumed to be non-empty
// No initial seed value is needed

let productOfAll (numbers: int list) : int =
    numbers
    |> List.reduce (fun a b -> a * b)

let r2 = [1..5] |> productOfAll

//////////////
let shoppingCart = [("Item1", 10.0); ("Item2", 5.0); ("Item3", 15.0)]
let c2 = []

let calculateTotalPrice (cart : (string * float) list) =
    cart
    |> List.fold (fun acc (n, p) -> acc + p) 0.0

shoppingCart |> calculateTotalPrice
c2 |> calculateTotalPrice

////////////
