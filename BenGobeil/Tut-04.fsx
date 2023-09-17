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
}

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
    | "Fizz" -> string 3
    | "Buzz" -> string 5
    | "FizzBuzz" -> string 15
    | x -> x

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