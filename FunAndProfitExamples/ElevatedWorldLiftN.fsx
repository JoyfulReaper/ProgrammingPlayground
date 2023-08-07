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