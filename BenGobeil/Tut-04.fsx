// Record Yupr
// Tuple
// Annoymous Record

open System

type Day = {
    DayOfTheMonth: int
    Month: int
}

type Person = {
    Name: string
    Age: int
} with
    static member (+) ({Name = n1; Age = a1}, {Name = n2; Age = a2}) =
        {Name = n1 + n2; Age = a1 + a2}

let kyle = { Name = "Kyle"; Age = 30}
let kyle2 = { Name = "Kyle"; Age = 30}

kyle = kyle2 |> printfn "Equal? %b"

let incrementAge (person: Person) = 
    { person with Age = person.Age + 1 }

type Dou = {
    Person1: Person
    Person2: Person
}

let alex = { Name = "Alex"; Age = 30}
let brothers = { Person1 = kyle; Person2 = alex}

brothers

type Dou' = Person * Person // Tuple type definition

let doSomething :Dou' =
    let kyle = { Name = "Kyle"; Age = 30}
    let alex = { Name = "Alex"; Age = 30}
    kyle, alex

(kyle, alex)

// Anonymous Records
let double = 
    {| Person1 = kyle; Person2 = alex |}

// Adding fields to an annomous record
let trio =
    {| double with Person3 = kyle|}


// Discriminated Unions

type Suit =
    | Spades
    | Hearts
    | Diamonds
    | Clubs

type NormalRectangle = {
    Height: double
    Base: double
}


// This works with Single Case Pattern matches
// Record Types
// Tuples
// Single Case Discriminated Unions

type OrderId = OrderId of int // Single Case Discriminated Union

let incrementOrderId (OrderId id) =
    OrderId <| id + 1


let incrementOrderId' =
    fun (OrderId id) ->
        OrderId <| id + 1


let area {Base = b; Height = h} = 
    b * h

let area' (b,h) = b * h

type Rectangle =
    | Normal of NormalRectangle
    | Square of side:double 

module Rectangle =
    let area = function
        | Normal {Base = b; Height = h} -> b * h
        | Square s -> s ** 2.

type Shape =
    | Rectangle of Rectangle
    | Triangle of height:double * _base:double
    | Circle of radius:double
    | Dot


let yesOrNo = function
    | true -> "Yes"
    | fase -> "No"

let isEven = function
    | x when x % 2 = 0 -> true
    | _ -> false

let isOne =
    (=) 1

let translateFizzBuzz = function
    | "Fizz" ->  Some 3
    | "Buzz" ->  Some 5
    | "FizzBuzz" ->  Some 15
    | _ -> None

let hasValue = function
    | Some _ -> true
    | None -> false

let circle = Circle 1.
let triangle = Triangle (2.,4.)


module Shape =
    let area shape =
        match shape with
        //| Rectangle rect -> rect.Base * rect.Height
        | Rectangle rect -> Rectangle.area rect
        | Triangle (height, _base) -> height * _base / 2.
        | Circle r -> r ** 2. * System.Math.PI
        | Dot -> 1

    let area' shape = function
        //| Rectangle rect -> rect.Base * rect.Height
        | Rectangle rect -> rect |> Rectangle.area
        | Triangle (height, _base) -> height * _base / 2.
        | Circle r -> r ** 2. * System.Math.PI
        | Dot -> 1

let inline add x y = x + y

kyle + alex

add kyle alex

//////////// Arrays ///////////
// Fixed size and Mutable
[|1;2;3;4;5;6;7;8;9|]
let arr = [|1..10|]

arr.[0] <- 5

/// List ////
// Immutable Linked List
[1;2;3;4;5;6;7;8;9]
[1..10]
[1..2..10]
[1. .. 0.1 .. 10.]
['a' .. 'z']

let addToList x xs =
    x::xs

let sampleList = [2;3;4]
addToList 1 sampleList

let getFirstItem = function
    | x ::_ -> Some x
    | _ -> None

let getFirstItem' list =
    List.head list

let test = sampleList |> getFirstItem'

let rec printEveryItem = function
    | x :: xs -> 
        printfn "%O" x
        printEveryItem xs
    | [] ->
        ()

printEveryItem [1;2;3;4]

/////////////////////

let printIt it =
    printfn "%O" it

let rec doForEveryItem f list =
    match list with
    | x::xs ->
        f x
        doForEveryItem f xs
    | [] ->
        ()

[1;2;3;4] |> doForEveryItem printIt

///////////////

let rec doWithEveryItem f = function
    | x::xs ->
        f x
        doWithEveryItem f xs
    | [] ->
        ()

let printEveryItem'<'a> =
    doWithEveryItem (printfn "%O")

let printEveryItem'' list =
    list
    |> List.iter (printfn "%O")

//////////////



let stringifyList (list:int list) =
    list
    |> List.map string

[1..10] |> stringifyList

Some 42
Some 42 |> Option.map string

////////

let iDunno num =
    num * 10

Some 42 |> Option.map iDunno

//////// FOLD (aggergrate) //////////
[1..10] // Sum
let sum list =
    list
    |> List.fold (fun acc item -> acc + item) 0

let sum' list =
    list
    |> List.fold (+) 0

[1..10] |> sum

//////// Reduce ////////////

let reduce list =
    list
    |> List.reduce (+)

///// Bind /////////

let bind f = function
    | Some x -> f x
    | None -> None

let divideInteger denominator numerator   =
    match numerator % denominator with
    | 0 -> Some <| numerator / denominator
    | _ -> None

let divideBy2 = divideInteger 2

24
|> divideBy2
|> Option.bind divideBy2
|> Option.bind divideBy2


////// Exceptions ////////

