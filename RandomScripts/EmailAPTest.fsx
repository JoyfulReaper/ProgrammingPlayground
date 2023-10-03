open System
open System.IO
open System.Text.RegularExpressions

type ValidationError =
    | MissingData of name: string
    | InvalidData of name: string * value: string

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success then Some (List.tail [for x in m.Groups -> x.Value])
    else None

let (|IsValidEmail|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [x] -> Some x
    | _ -> None
    
let validateEmail email =
    if email <> "" then
        match email with
        | IsValidEmail _ -> Ok (Some email)
        | _ -> Error (InvalidData ("Email", email))
    else
        Ok None
        
let test email =
    match email with
    | IsValidEmail x -> printfn "%A" x
    | _ -> printfn "Invalid email"