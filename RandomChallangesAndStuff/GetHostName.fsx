open System.Net

let getHostName (address : string) =
    try
        let entry : IPHostEntry = Dns.GetHostEntry(address)
        match entry with
        | null ->
            None
        | _ ->
            Some entry.HostName
    with
    | ex -> None


let displayHostName () =
    printfn "Enter ip address: "
    let ipaddress = System.Console.ReadLine()

    match getHostName ipaddress with
    | Some name ->
        printfn "Hostname: %s" name
    | None ->
        printfn "Unable to determine hostname"

displayHostName()