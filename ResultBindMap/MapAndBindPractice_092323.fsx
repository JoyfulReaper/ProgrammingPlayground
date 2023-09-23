open System

type Material =
    | Gold
    | Silver
    | Bronze
    | Copper
    | Iron
    
type Ore = Ore of Material
    
type Ignot = Ignot of Material
    
type Part =
    {
        Name: string
        Description: string option
        PartNumber: string
        Material: Material
    }

type Car = {
    Model: String
    Make: String
    Year: int
    Parts: Part array
}

let mineOre (material: Material) =
    if Random.Shared.NextDouble () > 0.25 then
        Some (Ore material)
    else
        None

let smeltOre (ore: Ore) =
    if Random.Shared.NextDouble () > 0.25 then
        let oreMaterial = 
            match ore with 
                Ore material -> 
                    material
        Some <| Ignot oreMaterial
    else
        None

    

let makePart (name: string) (description: string option) (partNumber: string) (ignot: Ignot) =
    let material = 
        match ignot with
            Ignot material ->
                material
    {
        Name = name
        Description = description
        PartNumber = partNumber
        Material = material
    }

let makeGear ignot = 
    makePart "Gear" (Some "A gear") "1234" ignot


let makeCar (model: string) (make: string) (year: int) part =
    {
        Model = model
        Make = make
        Year = year
        Parts = [|part|]
    }

let goMining material = 
    [for i in 1..10 -> mineOre material]

let smeltOres ores =
    [for ore in ores -> smeltOre ore]


let manufactureCar =
    let ore = goMining Iron
    ore
    |> List.map (fun x -> x |> Option.bind smeltOre)
    |> List.map (fun x -> x |> Option.map makeGear)
    |> List.map (fun x -> x |> Option.map (makeCar "Model" "Make" 2018))