// LiftN
// Common Names: lift2, lift3, lift4
// Combines two (or N) elevated values using a specified function
// Signature: linft2: (a -> b -> c) -> E<a> -> E<b> -> E<c>
// apply and return can be used to define helper functions lift2 ... liftN that take a normal function
// with N parameters and transform it into a corresponding elevated function (lift1 is map...)

// liftN can be used to make code more readable becasue using liftN <*> syntax can be avoided

module Option =
    // The apply function for Options
    let apply fOpt xOpt =
        match fOpt, xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

    let (<*>) = apply
    let (<!>) = Option.map

    let lift2 f x y =
        f <!> x <*> y

    let lift3 f x y z =
        f <!> x <*> y <*> z

    let lift4 f x y z w =
        f <!> x <*> y <*> z <*> w

    // Lift2 can be used as an alternative basis for defining apply
    let applyLift2 fOpt xOpt =
        lift2 (fun f x -> f x) fOpt xOpt

module List =
    // The apply function for Lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a -> 'b) list) (xList: 'a list) =
        [ for f in fList do
          for x in xList do
            yield f x]

    let (<*>) = apply
    let (<!>) = List.map

    let lift2 f x y =
        f <!> x <*> y

// Two parameter example function
let addPair x y = x + y

// lift two-param function
let addPairOpt = Option.lift2 addPair

// call as normal
addPairOpt (Some 1) (Some 2)
addPairOpt None (Some 2)

// Three parameter example function
let addTriple x y z = x + y + z

let addTripleOpt = Option.lift3 addTriple

addTripleOpt (Some 1) (Some 2) (Some 3)

// lift2 as a "combiner"
// first parameter is how to combine the values
Option.lift2 (+) (Some 2) (Some 3)
Option.lift2 (*) (Some 2) (Some 3)



//////Generic Way to combine Values/////////////

// Tuple creation function
let tuple x y = x,y

// create generic combiner of options
// with the tuple constructor baked in
let combineOpt x y = Option.lift2 tuple x y

// create generic combiner of lists
// with tuple constructor baked in
let combineList x y = List.lift2 tuple x y

combineOpt (Some 1) (Some 2)
combineList [1;2] [100;200]


combineOpt (Some 1) (Some 2)
|> Option.map (fun (x,y) -> x + y)

combineList [1;2] [100;200]
|> List.map (fun (x,y) -> x + y)

combineOpt (Some 1) (Some 2)
|> Option.map (fun (x,y) -> x * y)

combineList [1;2] [100;200]
|> List.map (fun (x,y) -> x * y)

//////////One Sided Combiners//////////////
// If you have two elevated values and want to discard the value from one side or the other

let ( <* ) x y =
    List.lift2 (fun left right -> left) x y

let ( *> ) x y =
    List.lift2 (fun left right -> right) x y

[1;2] <* [3;4;5]
[2;3] *> [3;4;5]