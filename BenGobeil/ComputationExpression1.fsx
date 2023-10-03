module Option =
    let apply fOpt xOpt =
        match fOpt, xOpt with
        | Some f, Some x -> Some <| f x
        | _ -> None
        
    let bind f xOpt =
        match xOpt with
        | Some x -> f x
        | None -> None
        
    let (<*>) = apply
        
    let zip x y =
        let toTuple x1 x2 = (x1, x2)
        Some toTuple <*> x <*> y

type OptionBuilder() =
    member _.MergeSources(x, y) =
        printfn $"Merge: {x}, {y}"
        Option.zip x y
        
    member _.BindReturn(x, f) =
        printfn $"BindReturn: x: {x}"
        Option.map f x
        
    member _.Bind(x,f) =
        printfn $"Bind x: {x}"
        Option.bind f x
        
    member _.Return(x) =
        printfn $"Return x: {x}"
        Some x
        
    member _.ReturnFrom(x) =
        x
    
let option = OptionBuilder()

let addThreeOptions (intOption1: int option) (intOption2: int option) (intOption3: int option)  =
    option {
        // let! x = intOption1
        // let! y = intOption2
        // let! z = intOption3
        //
        // return x + y + z
        return! intOption1
    }
    
// Monadic (bind)
let monadicAdd (intOption1: int option) (intOption2: int option) (intOption3: int option)  =
    intOption1
    |> Option.bind ( fun x ->
        intOption2
        |> Option.bind ( fun y ->
            intOption3
            |> Option.bind ( fun z ->
                x + y + z |> Some
            )
        )
    )
    
// Applicative (apply)
// module Option =
//     // Take optional function and optional value and return optional result
//     // Apply the function to the value
//     let apply fOpt xOpt =
//         match fOpt, xOpt with
//         | Some f, Some x -> Some <| f x
//         | _ -> None
        
let (<*>) = Option.apply

let applicativeAdd (intOption1: int option) (intOption2: int option) (intOption3: int option) =
    let add3 x y z = x + y + z
    Some (add3)
        <*> intOption1
        <*> intOption2
        <*> intOption3


monadicAdd (Some 1) (Some 2) (Some 3)
applicativeAdd (Some 1) (Some 2) (Some 3)

////////////////////////////////////////////////////

seq { 1
      2
      3
      4
      5
      6
      7
      8
       }

type ListBuilder() =
    member _.Zero() =
        printfn "Zero"
        []
        
    member _.Yield(x) =
        printfn $"Yield: X {x}"
        [x]
    
    member _.Combine(currentValueFromYield: 'a list, accumulatorFromDelay) =
        printfn $"Combine: currentValueFromYield {currentValueFromYield}"
        printfn $"Combine: accumulatorFromDelay {accumulatorFromDelay}"
        currentValueFromYield @ accumulatorFromDelay
    
    member _.Delay(f) =
        printfn $"Starting delay"
        let result = f()
        printfn $"Ending delay: {result}"
        result
let list = ListBuilder()

let newList: int list = list { 1
                               2
                               3 }

///////////////////////////////////////////

type Wheels =
    | Inch16
    | Inch18
    
type Engine =
    | Electric
    | Gas of numberOfCylinders: int
    
type Doors =
    | Base
    | Electric
    
type Performance =
    | Standard
    | Sport
    | Track

type Car =
    {
        Wheels: Wheels
        Engine: Engine
        Doors: Doors
        AC: bool
        Performance: Performance
    }
    
module Car =
    let baseCar =
        {
            Wheels = Inch16
            Engine = Gas 4
            Doors = Base
            AC = false
            Performance = Standard 
        }
        
   type CarBuilder() =
       member _.Zero _ =
           Car.baseCar
           
       member _.Yield _ =
           Car.baseCar
           
       [<CustomOperation("engine")>]
       member _.Wheels(car, engine) =
           { car with Engine = engine }
           
       [<CustomOperation("doors")>]
       member _.Wheels(car, doors) =
           { car with Doors = doors }
           
       [<CustomOperation("wheels")>]
       member _.Wheels(car, wheels) =
           { car with Wheels = wheels }
           
       [<CustomOperation("ac")>]
       member _.AC(car) =
           { car with AC = true }
           
       [<CustomOperation("performance")>]
       member _.Performance(car, performance) =
           { car with Performance = performance }
    
let car = CarBuilder()

car {
    wheels Inch18
    performance Track
    ac
}