open System

let [<Literal>] MaxGuesses = 10
let [<Literal>] MaxNumber = 100
let [<Literal>] MinNumber = 1

type GuessResult =
    | Correct
    | TooHigh
    | TooLow

let getTargetNumber min max =
    Random.Shared.Next(min, max + 1)
    
let checkGuess target guess =
    match guess with
    | _ when guess = target -> Correct
    | _ when guess > target -> TooHigh
    | _ -> TooLow
    
let showResult guessResult =
    match guessResult with
    | Correct -> "Correct!"
    | TooHigh -> "Too high!"
    | TooLow -> "Too low!"
    
let checkForGameOver target numGuesses guessResult =
    if numGuesses >= MaxGuesses then
        printfn "You lose after %i guesses! The number I was thinking of was: %i" numGuesses target
        true
    elif guessResult = Correct then
        printfn "You win after %i guesses!" numGuesses
        true
    else false
let playGame () =
    let target = getTargetNumber MinNumber MaxNumber
    
    let rec gameLoop target numGuesses =
        printfn "Guess a number between %d and %d" MinNumber MaxNumber
        let guess = Console.ReadLine () |> int
        let result = checkGuess target guess
        showResult result |> printfn "%s"
        let gameOver =checkForGameOver target numGuesses result
        
        if not gameOver then
            gameLoop target (numGuesses + 1)
    
    gameLoop target 1
    
playGame () |> ignore