// Return function
// Common names: return, pure, unit, yield, point
// Lifts a single value into the elevated world
// Signature: a -> E<a>

// Return create an elevated value from a normal value

// A value lifted to the world of Options
// 'a -> 'a option
let returnOption x = Some x

// A value lifted to the world of lists
let returnList x = [x]