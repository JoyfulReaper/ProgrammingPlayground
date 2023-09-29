open System

type PersonError =
    | FailedToHaveBirthday
    | FailedToTakeOffPants of string

type SystemError =
    | SystemErrorOccured of string

type Person =
    {
        Name: string
        Age: int
        IsWearingMask: bool
        IsWearingPants: bool
    }
    
let john = { Name = "John"; Age = 30 ; IsWearingMask = false; IsWearingPants = true }

let happyBirthday (person: Person) =
    if Random.Shared.NextDouble() < 0.75 then
        { person with Age = person.Age + 1 } |> Ok
    else
        Error <| FailedToHaveBirthday
        
let putOnMask person =
    {person with IsWearingMask = true}
     
let takeOffPants person =
    if Random.Shared.NextDouble() < 0.75 then
        { person with IsWearingPants = false } |> Ok
    elif Random.Shared.NextDouble() < 0.25 then
        Error <| FailedToTakeOffPants "I can't!"
    else
        Error <| FailedToTakeOffPants "I don't want to!"
     
let displayPerson person =
    match person with
       | Ok person ->
           sprintf "%A" person
       | Error e ->
           sprintf "Error: %A" e
        
let haveABirthday (person:Person) =
    person |> Ok |> displayPerson |> printfn "%s"
    
    person
    |> happyBirthday
    |> Result.map putOnMask
    |> Result.bind takeOffPants
    |> Result.mapError (fun e -> match e with
                                 | FailedToHaveBirthday -> SystemErrorOccured "Failed to have birthday"
                                 | FailedToTakeOffPants s -> sprintf "Failed to take off pants: %s"s |> SystemErrorOccured )
    |> displayPerson
    |> printfn "%s"
    
let newPerson =
    haveABirthday john
    

