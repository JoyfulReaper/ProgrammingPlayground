// Map function
// Common names: map, fmap, lift, select
// Common operators: <$> <!>
// Lift a function into the elevated world
// Signature: (a -> b) -> E<a> -> E<b>
// E<a> -> (a -> b) -> E<b> (parameters reveresed)

/// Map for options
// (a' -> b') -> a' option -> b' option
let mapOption f opt =
    match opt with
    | None ->
        None
    | Some x ->
        Some (f x)

/// map for lists
// (a' -> b') -> a' list -> b' list
let rec mapList f list =
    match list with
    | [] ->
        []
    | head::tail ->
        (f head) :: (mapList f tail)

// Usage examples

// Function in the normal world
// int -> int
let add1 x = x + 1

// A function lifted to the world of Options
// int option -> int option
let add1IfSomething = Option.map add1

// A function lifted to the world of Lists
let add1ToEachElement = List.map add1

Some 2 |> add1IfSomething
[1;2;3] |> add1ToEachElement

[1;2;3]
|> List.map add1

Some 2
|> Option.map add1

None
|> Option.map add1

[]
|> List.map add1