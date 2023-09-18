open System


type Person =
    {
        FirstName: string
        LastName: string
        HasBrain: bool
        IsInfected: bool
    }

type Zombie =
    {
        Name: string
        Number: int
        InfectedPerson: Person
    }

type Dog =
    {
        Name: string
    }

type Creature =
    | Person of Person
    | Zombie of Zombie
    | Dog of Dog


let dog =
    { Name = "Dog" } |> Dog

let getCreatures () = 
    let rnd = new Random()
    dog::[ for i in 1..5 -> 
            {
                FirstName = sprintf "Person%d" i
                LastName = sprintf "Person%d" i
                HasBrain = rnd.Next(0, 2) = 1
                IsInfected = rnd.Next(0, 2) = 1
            } |> Person
        ]

let convertCreatureToZombie creature =
    match creature with
    | Person p ->
        if not <| p.IsInfected then
            Error <| sprintf "%s %s is not infected!" p.FirstName p.LastName
        
        else if not <| p.HasBrain then
            Error <| sprintf "%s %s does not have a brain!" p.FirstName p.LastName
        else
            let zombie =
                {
                    Name = sprintf "Zombie %s" p.FirstName
                    Number = Random.Shared.Next(0, 100)
                    InfectedPerson = p
                }
            Ok <| Zombie zombie
    | _ ->
        Error <| sprintf "Only people can become zombies!"

let printConversionResult (conversionResult: Result<Creature, String>) =
    match conversionResult with
    | Ok (Zombie z) ->
        printfn "Successfully converted %s to a zombie!" z.InfectedPerson.FirstName
    | Error errorMessage ->
        printfn "Error converting creature to zombie: %s" errorMessage
    | _  ->
        printfn "This shouldn't happen, but a creature other than a human became a zombie!"

let creatures = getCreatures()
let convertedCreatures =
    creatures
    |> List.map convertCreatureToZombie

List.iter printConversionResult convertedCreatures

let zombies = 
    convertedCreatures
    |> List.choose (function
        | Ok (Zombie z) -> Some z
        | _ -> None
    )