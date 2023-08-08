[<RequireQualifiedAccess>]
module Result =
    /// Pass in a function to handle each case of `Result`
    let map = Result.map

    /// Apply a Result<fn> to a Result<x> monadically
    let apply fR xR =
        match fR, xR with
        | Ok f, Ok x -> Ok (f x)
        | Error err1, Ok _ -> Error err1
        | Ok _, Error err2 -> Error err2
        | Error err1, Error _ -> Error err1

    /// Lift a two parameter function to use Result parameters
    let lift2 f x1 x2 =
        let (<!>) = map
        let (<*>) = apply
        f <!> x1 <*> x2


// Define the function 'add' that takes two integers and returns their sum
let add x y = x + y

// Sample Result values (you can use your actual Result values in your scenario)
let x1 : Result<int, string> = Ok 5
let x2 : Result<int, string> = Ok 10

// Use 'lift2' to apply the 'add' function to the Result values 'x1' and 'x2'
let resultSum : Result<int, string> = Result.lift2 add (Ok 5) (Ok 10)

// Match the 'resultSum' to handle the possible outcomes
match resultSum with
| Ok sum -> printfn "The sum is: %d" sum
| Error errorMessage -> printfn "An error occurred: %s" errorMessage


