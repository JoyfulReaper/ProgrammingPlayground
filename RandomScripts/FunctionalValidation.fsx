#r "nuget: FSToolkit.ErrorHandling"

open FsToolkit.ErrorHandling
open System

[<StructuredFormatDisplay("{Message}")>]
type ValidationError =
    | NameExceedsMaxLength of string * int
    | NameMustNotBeEmpty
    | InvalidAge of string
    member this.Name =
        match this with
        | NameExceedsMaxLength (name, _) -> "Name Exceeds Max Length"
        | NameMustNotBeEmpty -> "Name Must Not Be Empty"
        | InvalidAge _ -> "Invalid Age"
    member this.Message =
        match this with
        | NameExceedsMaxLength (name, limit) -> sprintf "Name '%s' exceeds max length of %i" name limit
        | NameMustNotBeEmpty -> "Name must not be empty"
        | InvalidAge message -> sprintf "Invalid Age: %s" message


//////////////////////////////////

[<CLIMutable>]
type PersonDto =
    {
        FirstName: string
        LastName: string
        Age: int
    }

/////////////////////////////////

type Name = private Name of string

module Name =
    
    let value (Name name) = name
    
    let create (name:string) =
        if name.Length > 50 then
            Error (NameExceedsMaxLength (name, 50))
        elif
            String.IsNullOrWhiteSpace(name) then
                Error NameMustNotBeEmpty
        else
            Ok (Name name)
            
type Age = private Age of int


module Age =
    
    let value (Age age) = age
    
    let create (age:int) =
        if age <= 0 then
            Error <| InvalidAge "Age must be greater than 0"
        elif age > 120 then
            Error <| InvalidAge "Age must be less than 120"
        else
            Ok (Age age)
            
type Person =
    {
        FirstName: Name
        LastName: Name
        Age: Age
    }
    
module Person =
    let create (dto:PersonDto) : Result<Person,ValidationError list> =
        validation {
            let! firstName = Name.create dto.FirstName
            and! lastName = Name.create dto.LastName
            and! age = Age.create dto.Age
            return { FirstName = firstName; LastName = lastName; Age = age }
        }
        
        
let createPerson () =
    printfn "First Name: "
    let firstName = Console.ReadLine ()
    printfn "Last Name: "
    let lastName = Console.ReadLine ()
    printfn "Age: "
    let age = Console.ReadLine ()
    
    let dto:PersonDto = { FirstName = firstName; LastName = lastName; Age = int age }
    
    match Person.create dto with
    | Ok person -> printfn "Person created successfully: %A" person
    | Error errors -> printfn "Errors: %A" errors
    
createPerson()