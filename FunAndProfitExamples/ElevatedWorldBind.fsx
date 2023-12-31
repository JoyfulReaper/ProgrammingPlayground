﻿// Bind
// Common names: bind, flatMap, andThen, collect, SelectMany
// Common operators: >>= (left to right) =<< (right to left)
// Allows you to compose world-crossing (mondadic) functions
// Signature: (a -> E<b>) -> E<a> -> E<b> With parameters reveresed: E<a> -> (a -> E<b>) -> E<b>

// Helps to deal with functions that cross between the normal an elevated world.
// A function that parses a string to an int might return an Option<int> rather than a normal int.
// World crossing signature example: a -> E<b> Input is normal world, but output is in elevated world
// Cannot be linked together using standard compositon

// Bind transforms a world-crossing function (monadic function) into a lifted function (E<a> -> E<b>)
// The resulting lifted function lives purely in the elevated world
// Ex: a -> E<b> cannot be directly composed with function of type b -> E<c>, but after bind is used the
// second function becomes E<b> -> E<c> which can be composed
// This lets us chain together any number of monadic functions

// Alt: Bind is a two parameter function that takes an elevated value (E<a>) and a monadic function (a -> E<b>) and returns
// a new elevated value (E<b>) generated by "unwrapping" the value inside the input and running the function a -> E<b> against it

module Option =
    // the bind function for Options
    // ('a -> 'b option) -> 'a option -> 'b option
    let bind f xOpt =
        match xOpt with
        | Some x -> f x
        | _ -> None

module List =
    // Bind function for lists
    let bindList (f: 'a -> 'b list) (xList: 'a list) =
        [ for x in xList do
          for y in f x do
            yield y ]

// string -> int option (cross world function)
let parseInt str =
    match str with
    | "-1" -> Some -1
    | "0" -> Some 0
    | "1" -> Some 1
    | "2" -> Some 2
    | _ -> None

type OrderQty = OrderQty of int
// int -> OrderQty option (cross world function)
let toOrderQty qty =
    if qty >= 1 then
        Some (OrderQty qty)
    else
        // Only positive numbers allowed
        None

// Option.bind toOrderQty lifts it to an int option -> OrderQty Option
// string -> OrderQty Option
let parseOrderQty str =
    parseInt str
    |> Option.bind toOrderQty

// Infix version of bind
let (<<=) =
    Option.bind

let (>>=) opt func=
    match opt with
    | Some x -> func x
    | None -> None

let parseOrderQty_alt str =
    str |> parseInt >>= toOrderQty

let parseOrderQty_alt2 str =
    toOrderQty <<= parseInt str