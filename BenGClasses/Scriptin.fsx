// Interface
type IAdd =
    abstract member Add: unit -> double
    abstract member Minus: unit -> double

type IWoof =
    abstract member Woof: unit -> unit

let someObjectExpression =
    { new IAdd with
        member _.Add() = 10.0
        member _.Minus() = 10.0
      interface IWoof with
        member _.Woof() = printfn "Woof" }

someObjectExpression.Add()
let woof = someObjectExpression :?> IWoof
woof.Woof()

// Class
// Simplest Class:
// type SomeClass() = class end

// Record
type SomeRecord = { Age: int }


type SomeClass(first: double, second: double) as self =
    do printfn $"DO: Constructing with {first} and {second}"
    let minus x y = x - y // Let bindings are private
    //let ten = 10
    
    new () = // Default constructor
        printfn $"in new()"
        SomeClass(0, 0)
    
    new (first) = // Constructor with one parameter
        printfn $"in new(first)"
        SomeClass (first, 0)
    
    member _.Ten = 10
    
    // Property
    member _.Ten'
        with get () =
            10
        and set(value: int) =
            printfn "%i" value
    
    member _.Add (x:double, y: double) =
        x + y
    member _.Add x =
            x + self.Ten
     
    member _.CAdd () =
        first + second
            
    static member Add (x:double, y:double) =
        x + y
        
    interface IAdd with
        member this.Add() = this.CAdd()
        
let someClass = SomeClass(1, 2)
someClass.Ten'
someClass.Ten' <-3

someClass.Add 20
someClass.Add (5, 16)
SomeClass.Add(5, 6)
someClass.CAdd()

let someClass2 = SomeClass(2)
someClass2.CAdd()