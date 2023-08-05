let shuffleList (list: 'a list) =
    let rnd = System.Random()
    list
    |> List.map (fun x -> (rnd.Next(), x))
    |> List.sortBy fst
    |> List.map snd

let getRandomDigit () =
    System.Random.Shared.Next(0, 11)

let getRandomSymbol () =
    let symbols = ["!"; "@"; "#"; "$"; "%"; "^"; "&"; "*"; "("; ")"; "-"; "="; "+"; "_"; "|"; "~"; "`"; "?"; "<"; ">"; "\\"; "/";]
    symbols[System.Random.Shared.Next(0, List.length symbols)]

let getRandomLetter () =
    let letters = ['a' .. 'z']
    letters[System.Random.Shared.Next(0, List.length letters)]

let getRandomPasswordComponent () =
    let compoent = System.Random.Shared.Next(1, 5)
    match compoent with
    | 1 ->
        getRandomDigit() |> string
    | 2 ->
        getRandomLetter() |> string
    | 3 ->
        getRandomSymbol()
    | 4 ->
        (getRandomLetter() |> string).ToUpper()

let generatePassword () =
    [getRandomDigit() |> string; getRandomSymbol(); (getRandomLetter() |> string).ToUpper(); (getRandomLetter() |> string).ToUpper(); for i in 1 .. 6 do getRandomPasswordComponent()]
    |> shuffleList
    |> String.concat ""