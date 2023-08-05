open System

let generateAllCombinations numDie =
    let rec generateCombinations numDie currentCombination allCombinations =
        match numDie with
        | 0 ->
            currentCombination::allCombinations
        | _ ->
            let mutable combinations = allCombinations
            for roll in 1 .. 6 do
                let updatedCombination = roll::currentCombination
                combinations <- generateCombinations (numDie - 1) updatedCombination combinations
            combinations
    generateCombinations numDie [] []
    
let findCountOfTargetCombinations targetRoll (possibleOutcomes:int list list) =
    List.map List.sum possibleOutcomes
    |> List.filter (fun x -> x = targetRoll)
    |> List.length
        

let diceRoll numberOfDie targetRoll =
    numberOfDie
    |> generateAllCombinations
    |> findCountOfTargetCombinations targetRoll


let executeApplication () =
    printfn "How many die should we knock around? "
    let numberOfDie = Console.ReadLine() |> Int32.Parse
    printfn "What's your target roll? "
    let targetRoll = Console.ReadLine() |> Int32.Parse

    let numberOfCombinations = diceRoll numberOfDie targetRoll
    printfn "There are a total of %i distinct combintations that totals %i" numberOfCombinations targetRoll

executeApplication ()