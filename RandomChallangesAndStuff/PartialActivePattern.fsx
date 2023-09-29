open System.Text.RegularExpressions

let (|Email|_|) (input:string) =
    match Regex.Match(input, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success with
    | true -> Some input
    | false -> None
    
match "example@example.com" with
| Email e -> printfn "Email: %s" e
| _ -> printfn "Not Valid"


let (|AllEven|_|) (input:int list) =
    match input |> List.forall (fun x -> x % 2 = 0) with
    | true -> Some input
    | false -> None
    
match [2;4;6;8] with
| AllEven e -> printfn "All Even: %A" e
| _ -> printfn "Not All Even"