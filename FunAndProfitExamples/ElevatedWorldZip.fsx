// Zip function
// Common Names: zip, zipWith, map2
// Common Operators: <*> (in context of ZipList "World")
// Combines 2 lists or other enumerables using a specified function
// Signature: E<(a -> b -> c)> -> E<a> -> E<b> -> E<c> where E is an enumberable type or E<a> -> E<b> -> E<a,b> for tupled-combined version
// Some data types may have more than one valid implementation of apply. In this implementation the corresponmding elements in each
// list are processed at the same time, and then both lists are shifted to the next element


// alternate "zip" implementation (not tail recursive)
// [f;g] apply [x;y] becomes [f x; g y]
// ('a -> 'b) list -> 'a list -> 'b list
let rec zipList fList xList =
    match fList,xList with
    | [], _
    | _,[] ->
        // either side empty, then done
        []
    | (f::fTail),(x::xTail) ->
        // new head + new tail
        (f x) :: (zipList fTail xTail)

let add10 x = x + 10
let add20 x = x + 20
let add30 x = x + 30

let result =
    let (<*>) = zipList
    [add10; add20; add30] <*> [1;2;3]

// zip list as combiner
let add x y = x + y

let resultAdd =
    let (<*>) = zipList
    [add;add] <*> [1;2] <*> [10;20]

// ZipList world
// Standard List world has apply and return. With out different version of apply we can create a differemy version of List world
// called ZipList world. In ZipList world the apply function is implemented as above. ZipList world also has a completely different return 
// implementation. In standard list world return is a list with a single element, in ZipList World it has to be an infinetly repeating value

module ZipSeq =
    // define return for ZipWorld
    let retn x = Seq.initInfinite (fun _ -> x)

    // define apply for ZipWorld
    // ('a -> 'b) seq -> 'a seq -> 'b seq
    let apply fSeq xSeq =
        Seq.map2 (fun f x -> f x) fSeq xSeq

    // define a sequence that is a combination of tweo others
    let triangularNumbers =
        let (<*>) = apply

        let addAndDivideByTwo  x y = (x + y) / 2
        let numbers = Seq.initInfinite id
        let squareNumbers = Seq.initInfinite (fun i -> i * i)
        (retn addAndDivideByTwo <*> numbers <*> squareNumbers)

// evaluate first 10 elements
// and display result
ZipSeq.triangularNumbers |> Seq.take 10 |> List.ofSeq |> printfn "%A"