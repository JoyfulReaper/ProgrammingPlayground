type Gender =
    | Male
    | Female

type Person = {
    Name: string
    Gender: Gender
    BirthYear: int
}

let person1 =
    {
        Name = "Kyle"
        Gender = Male
        BirthYear = 1988
    }

let person2 =
    {
        Name = "Sally"
        Gender = Female
        BirthYear = 2000
    }

let person3 =
    {
        Name = "Chris"
        Gender = Male
        BirthYear = 1965
    }

let person4 =
    {
        Name = "Sarah"
        Gender = Female
        BirthYear = 1975
    }

let person5 =
    {
        Name = "Wane"
        Gender = Male
        BirthYear = 2015
    }

let people = [person1;person2;person3;person4;person5]

let gbGender = 
    people |>
    List.groupBy (fun p -> p.Gender)

for (gender, people) in gbGender do
    printfn "%A" gender
    for person in people do
        printfn "%s" person.Name

gbGender
|> List.iter (fun (gender, people) ->
    printfn "%A" gender
    people
    |> List.iter (fun p -> printfn "%s" p.Name))