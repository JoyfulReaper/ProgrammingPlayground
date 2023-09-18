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

let getPeople numPeople () = 
    let rnd = new Random()
    [ for i in 1..numPeople -> 
        {
            FirstName = sprintf "Person%d" i
            LastName = sprintf "Person%d" i
            HasBrain = rnd.Next(0, 2) = 1
            IsInfected = rnd.Next(0, 2) = 1
        }
    ]

let getFivePeople = getPeople 5

let printConversionResult (conversionResult: Result<Zombie, String>) =
    match conversionResult with
    | Ok z ->
        printfn "Successfully converted %s to a zombie!" z.InfectedPerson.FirstName
    | Error errorMessage ->
        printfn "Error converting creature to zombie: %s" errorMessage

let convertPersonToZombie person =
    if not <| person.IsInfected then
        Error <| sprintf "%s %s is not infected!" person.FirstName person.LastName
    
    else if not <| person.HasBrain then
        Error <| sprintf "%s %s does not have a brain!" person.FirstName person.LastName
    else
        {
            Name = sprintf "Zombie %s" person.FirstName
            Number = Random.Shared.Next(0, 100)
            InfectedPerson = person
        } |> Ok

let feedZombie zombie =
    if Random.Shared.NextDouble() < 0.25 then
        Error <| sprintf "%s couldn't find any brains to eat!" zombie.Name
    else
        Ok zombie

let pokeZombie zombie =
    printfn "%s was poked!" zombie.Name
    if Random.Shared.NextDouble() < 0.25 then
        {zombie with Name = "Poked " + zombie.Name}
    else
        zombie

//////////////////////////////

let people = getFivePeople ()

let zombieConversionResults =
    people 
    |> List.map convertPersonToZombie

zombieConversionResults
    |> List.map (Result.map pokeZombie)
    |> List.map (Result.bind feedZombie)
    |> List.iter printConversionResult