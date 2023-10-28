open System.Net.Http
open System.Text.Json

open FSharp.Control.TaskBuilder

open Oryx
open Oryx.SystemTextJson.ResponseReader

[<Literal>]
let Url = "https://www.wikipedia.org/w/api.php"

let options = JsonSerializerOptions()

let query term = [
    struct ("action", "opensearch")
    struct ("search", term)
]

let asyncMain argv = task {
    use client = HttpClient()
    let request term =
        httpRequest
        |> GET
        |> withHttpClient client
        |> withUrl Url
        |> withQuery (query term)
        |> fetch
        |> json options
        
    let! result = request "F#" |> runAsync
    printfn "Result: %A" result
}

[<EntryPoint>]
let main argv =
    asyncMain().GetAwaiter().GetResult()
    0