open System

let getRandomDigit() =
    System.Random.Shared.Next(0, 10)

let getFourRandomDigits() =
    [ for i in 1 .. 4 do
        getRandomDigit()
        |> string 
        |> char ]

let splitNumberToDigitList (number : int) =
    let numberAsString =
        number
        |> string
    List.ofSeq numberAsString

// In the correct place
let countCows input target =
    let pairs = List.zip input target
    let mutable cows = 0
    List.iter2 (fun input target ->
        if input = target then
            cows <- cows + 1
    ) input target
    cows
 
let countBulls input target =
    let commonNumbers =
        input
        |> List.filter(fun i -> List.contains i target)
        |> List.length
    let cows = countCows input target
    commonNumbers - cows

let displayResults cows bulls =
    printfn "You guessed correctly! Cows: %i Bulls %i" cows bulls

let cowsAndBulls() =
    let rec loop cows bulls =
        let target = getFourRandomDigits()
        printfn "Enter a 4 digit number: "
        let guess = Int32.Parse (Console.ReadLine())
        let guessList = splitNumberToDigitList guess

        let compareResult = List.compareWith compare guessList target
        if compareResult = 0 then
            displayResults cows bulls

        let newCows = 
            guessList
            |> countCows target
        let newBulls =
            guessList
            |> countBulls target

        printfn "Cows: %i Bulls: %i" newCows newBulls
        loop (cows + newCows) (bulls + newBulls)
    loop 0 0