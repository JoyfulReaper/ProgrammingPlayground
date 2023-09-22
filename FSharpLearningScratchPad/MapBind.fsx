open System

type Thing =
    {
        Name: string
        Color: string
        Sanded: bool
        Painted: bool
        Tested: bool
    }

let paint color thing =
    match Random.Shared.NextDouble() with
    | x when x < 0.75 ->
        Ok { thing with Color = color; Painted = true;}
    | _ ->
        Error "Failed to paint"

let sand thing =
    { thing with Sanded = true }

let test thing =
    match Random.Shared.NextDouble() with
    | x when x < 0.75 ->
            Ok { thing with Tested = true }
    | _ ->
            Error "Failed Testing"

let paintSandAndTest color thing =
    paint color thing
    |> Result.map sand
    |> Result.bind test

let thing = { Name = "Thing"; Color = "Red"; Painted = false; Sanded = false; Tested = false }
let thingResult = thing |> paintSandAndTest "Blue"

match thingResult with
| Ok t ->
    printfn "Name: %s, Color: %s, Painted: %b, Sanded: %b, Tested: %b" t.Name t.Color t.Painted t.Sanded t.Tested
| Error e ->
    printfn "Error: %s" e

