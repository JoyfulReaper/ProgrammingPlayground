open System
open System.IO

type Person = {
    FirstName: string
    LastName: string
}

type DataReader = string -> Result<string seq, exn>

let createPerson () =
    printfn "Please enter first name: "
    let firstName = Console.ReadLine()
    printfn "Please enter last name: "
    let lastName = Console.ReadLine()
    { FirstName = firstName; LastName = lastName }
    
   
let writeFile path (data:string seq) =
    try
        File.WriteAllLines(path, data)
        Ok ()
    with
        | exn -> Error exn
    
let readFile : DataReader =
    fun path ->
        try
            File.ReadAllLines(path)
            |>Seq.ofArray
            |> Ok
        with
            | exn -> Error exn
            
let parseLine (line:string) : Person option =
    match line.Split(',') with
    | [| firstName; lastName |] ->
        Some { FirstName = firstName; LastName = lastName }
    | _ -> None
    
let parse (data:string seq) =
    data
    |> Seq.map parseLine
    |> Seq.choose id
    
let output data =
    data
    |> Seq.iter (fun p -> printfn "First name: %s Last name: %s" p.FirstName p.LastName)
    
let import (dataReader:DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error exn -> printfn "Error: %s" exn.Message
    
let addPerson path person =
    let lines = readFile path
    match lines with
    | Ok lines ->
        Seq.append lines [sprintf "%s,%s" person.FirstName person.LastName]
        |> writeFile path
    | Error ex-> Error ex

    
".\data.csv"
|> import readFile

let person = createPerson()
match person |> addPerson ".\data.csv" with
| Ok () -> printfn "Person added"
| Error ex -> printfn "Error: %s" ex.Message

".\data.csv"
|> import readFile