
// int -> int -> int
let add x y = x + y

// int -> int -> int using a lambda
let add' = fun x y -> x + y

// int -> int -> int using currying / baking in a parameter
// Bake in a paramer "x" to a function "(x + y)"
// Function that takes one parameter and returns a function that takes another parameter
let add'' x = fun y -> x + y
// The add'' function takes a single parameter and returns a function that takes one parameter


// int -> int
let add5 x = x + 5

// Partial Application
let add5' = add 5 // Point free: Not specifing all the arguments that are being used

// (2 * (number + 3)) ^ 2
let operation number = (2. * (number + 3.)) ** 2.

let add3 number = number + 3

// 3 + number
let add3' number = (+) 3 number


let add3'' = (+) 3.
let times2 = (*) 2.
let pow2 number = number ** 2.

// (2 * (number + 3)) ^ 2
let operation' number = pow2 (times2 (add3 number))

// More readable - Solution is most programming languages
let operation'' number =
    let x = add3'' number
    let y = times2 x
    pow2 y

// Pipe operator - // (2 * (number + 3)) ^ 2
let operation''' number =
    number |> add3''
    |> times2
    |> pow2
    
// Composition Operator (Point free)
let operation'''' =
    add3'' 
    >> times2 
    >> pow2

operation''' 1.
operation'''' 1.

// Defining Operators
let (>>) f g =
    fun x ->
        x
        |> f
        |> g
        // g(f x)