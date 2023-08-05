let sumMultiples n =
    [1..(n-1)]
    |> List.filter (fun x -> x % 3 = 0 || x % 5 = 0)
    |> List.sum

let rec countOccurrences needle haystack =
    match haystack with
    | [] -> 0
    | head::tail ->
        let countTail = countOccurrences needle tail
        if head = needle then
            1 + countTail
        else
            countTail

let test = function
    | a -> sprintf "%s" a

let x = test "d"


type Pets =
    | Dog of name: string
    | Cat of name: string
    | Hampster of name: string


let shelly = Dog "Shelly"

let whatPet = function // Variable is not in scope
    | Dog x -> sprintf "The dog's name is %s" x
    | Cat x -> sprintf "The cat's name is %s" x
    | Hampster x -> sprintf "The boring hampster is named %s" x

let whatPet' pet = // variable is in scope
    match pet with
    | Dog x -> sprintf "The dog's name is %s" x
    | Cat x -> sprintf "The cat's name is %s" x
    | Hampster x -> sprintf "The boring hampster is named %s" x

shelly |> whatPet |> printfn "%s"
shelly |> whatPet' |> printfn "%s"