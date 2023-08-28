/// Replace all commas with a specified replacement value
let replaceCommas (input: string) (replacement : string) : string =
    input.Replace(",", replacement)

// Usage example
let test = "Do the dishes, then dry them"

// Replace all commas with semicolons
let newTest = replaceCommas test "; "

printfn "%s" newTest