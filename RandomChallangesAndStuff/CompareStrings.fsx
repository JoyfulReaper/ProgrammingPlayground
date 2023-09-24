let stringComp (s1:string) (s2:string) =
    s1.Length = s2.Length
    
let rec factorial n =
    match n with
    | 0 -> 1
    | 1 -> 1
    | _ -> n * factorial (n-1)
    
let frames a b =
    a * b * 60
    
let ctoa (c:char) =
    int c