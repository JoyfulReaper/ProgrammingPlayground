open System

// Map, Filter, Fold, Reduce, Sort, Bind
// GroupBy
// List.Choose 

///////////// Seq.map mapping source ////////////////

// Seq.map 'T -> 'U
let getLength (s : string) =
    s.Length

let printNum n =
    printf "%d " n

let sequence = ["a"; "bbb"; "cc"]

// Higher order Function
// Seq.Map: 'T - 'U (Map string to int)
sequence 
|> Seq.map getLength
|> Seq.iter printNum

///////////// Group By ////////////////

let gbSeq = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
let grouped = 
    gbSeq |> Seq.groupBy (fun n -> n % 2 = 0)

for (key, group) in grouped do
    printfn "Key: %b" key
    for item in group do
        printf "%d " item
    printfn ""

type Gender =
    | Male
    | Female

type UserStatus =
    | Free
    | Registered

type Person =
    {
        Name : string
        Gender: Gender
    }

type User =
    {
        UserId: Guid
        Person: Person
        UserStatus: UserStatus
    }



let people = [ { Name = "Sam"; Gender = Male }; { Name = "Fred"; Gender = Male }; { Name = "Sally"; Gender = Female }; { Name = "Kyle"; Gender = Male }; { Name = "Sarah"; Gender = Female } ]

let pGrouped =
    people
    |> Seq.groupBy (fun p -> p.Gender)

let getGenderString = function
    | Male -> "Male"
    | Female -> "Female"

for (gender, group) in pGrouped do
    getGenderString gender |> printfn "Gender: %s" 
    for person in group do
        printfn "%s" person.Name

// Create 3 sample users
let user1 =
    {
        UserId = Guid.NewGuid()
        Person = {
            Name = "Kyle"
            Gender = Male
        }
        UserStatus = Registered
    }

let user2 =
    {
        UserId = Guid.NewGuid()
        Person = {
            Name = "Sam"
            Gender = Male
        }
        UserStatus = Free
    }

let user3 =
    {
        UserId = Guid.NewGuid()
        Person = {
            Name = "Sarah"
            Gender = Female
        }
        UserStatus = Registered
    }

let userList = [ user1; user2; user3 ]

let genderReveal users =
    let rec loop = function
        | [] -> ()
        | head::tail ->
            printfn "%s: %s" head.Person.Name (getGenderString head.Person.Gender)
            loop tail
    loop users

userList |> genderReveal

let getFreeloaders users =
    let rec loop = function
        | [] -> []
        | head::tail ->
            match head.UserStatus with
            | Free -> head::(loop tail)
            | _ -> loop tail
    loop users

let freeloaders = userList |> getFreeloaders

let freeloaders2 =
    query {
        for user in userList do
        where (user.UserStatus = Free)
        select user
    }

freeloaders2
|> Seq.iter (fun user -> printfn "%s" user.Person.Name)

//////////////// Seq.Fold folder state source  ////////////////
// Applies a function to each element of a collection, threading an accumulator argument through the computation.

type Charge =
    | In of int
    | Out of int

let inputs = [ In 1; Out 2; In 3]

let test =
    (0, inputs) ||> Seq.fold (fun acc charge ->
        match charge with
        | In i -> acc + i
        | Out o -> acc - o)

printfn "%i" test


//////////// Seq.Filter predicate source //////////////
let men = 
    userList 
    |> Seq.filter (fun u -> u.Person.Gender = Male)

men
|> Seq.iter (fun u -> printfn "%s" u.Person.Name)


//////////// Seq.Reduce reduction source //////////////
// Applies a function to each element of a collection, threading an accumulator argument through the computation.
// Begin by applying the function to the first two elements. Then feed this result into the function along with the third element and so on. Return the final result.

let inputs2 = [1; 3; 4; 2]
inputs2 |> Seq.reduce (fun a b -> a * 10 + b)

///////// List.Choose chooser list //////////////
// Applies a function to each element in a list and then returns a list of values v where the applied function returned Some(v). 
// Returns an empty list when the input list is empty or when the applied chooser function returns None for all elements.

type Happiness =
    | AlwaysHappy
    | MostOfTheTimeGrumpy

type People = {
        Name: string
        Happiness: Happiness
    }

let takeJustHappyPersons person =
    match person.Happiness with
    | AlwaysHappy -> Some person
    | MostOfTheTimeGrumpy -> None

let candidates =
    [
        { Name = "Sam"; Happiness = AlwaysHappy }
        { Name = "Fred"; Happiness = MostOfTheTimeGrumpy }
        { Name = "Sally"; Happiness = AlwaysHappy }
        { Name = "Kyle"; Happiness = MostOfTheTimeGrumpy }
        { Name = "Sarah"; Happiness = AlwaysHappy }
    ]

let happyPeople = candidates |> List.choose takeJustHappyPersons

happyPeople |> List.iter (fun p -> printfn "%s" p.Name)