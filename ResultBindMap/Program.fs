

let add x y =
    x + y

let add5 =
    add 5

// SUM type / OR Types
type Ticket = 
    | ParkingFine  // Values in two different sets
    | DogOffLeash

// PRODUCT type / AND type
// Cartesian Product
type Something =
    {
        SomeInt: int  // all ints are potential values
        SomeFloat: float
    }