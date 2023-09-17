namespace FSharpBasics

module Arithmetic =
    module Addition =
        let add x y = x + y

module Other =
    open Arithmetic
    let program =
        Arithmetic.Addition.add 5 3
        Addition.add