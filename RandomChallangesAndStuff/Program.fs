printfn "Hello from F#"
printfn "Coding in F# for the first time wish me luckS"

//open System

//let getRandomDigit() =
//    System.Random.Shared.Next(0, 10)

//let getFourRandomDigits() =
//    [ for i in 1 .. 4 do
//        getRandomDigit()
//        |> string 
//        |> char ]

//let splitNumberToDigitList (number : int) =
//    let numberAsString =
//        number
//        |> string
//    List.ofSeq numberAsString

//// In the correct place
//let countCows input target =
//    let pairs = List.zip input target
//    let mutable cows = 0
//    List.iter2 (fun input target ->
//        if input = target then
//            cows <- cows + 1
//    ) input target
//    cows
 
//let countBulls input target =
//    let commonNumbers =
//        input
//        |> List.filter(fun i -> List.contains i target)
//        |> List.length
//    let cows = countCows input target
//    commonNumbers - cows

//let displayResults cows bulls =
//    printfn "You guessed correctly! Cows: %i Bulls %i" cows bulls

//let cowsAndBulls() =
//    let target = getFourRandomDigits()
//    let rec loop cows bulls target =
//        printfn "Enter a 4 digit number: "
//        let guess = Int32.Parse (Console.ReadLine())
//        let guessList = splitNumberToDigitList guess

//        let compareResult = List.compareWith compare guessList target
//        if compareResult = 0 then
//            displayResults cows bulls

//        let newCows = 
//            guessList
//            |> countCows target
//        let newBulls =
//            guessList
//            |> countBulls target

//        printfn "Cows: %i Bulls: %i\n" newCows newBulls
//        loop (cows + newCows) (bulls + newBulls) target
//    loop 0 0 target

//cowsAndBulls()


//open System.Text.RegularExpressions
//open System

//let extractDomain url =
//    let pattern = "^https?://(?:www\.)?([^/]+)"
//    let regex = Regex(pattern)
//    match regex.Match(url) with
//    | m when m.Success -> m.Groups.[1].Value
//    | _ -> "Failed to extract domain name!"

//Console.WriteLine("Enter any url: ")
//let url = Console.ReadLine()
//let result = extractDomain url
//printfn "Result: %s" result