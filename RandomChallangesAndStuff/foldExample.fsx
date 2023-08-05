type Fruit = Apple | Pear | Orange

type BagItem = { fruit: Fruit; quantity: int }

let takeMore (previous: BagItem list) fruit = 
    let toTakeThisTime = 
        match previous with 
        | bagItem :: otherBagItems -> bagItem.quantity + 1 
        | [] -> 1 
    { fruit = fruit; quantity = toTakeThisTime } :: previous

let inputs = [ Apple; Pear; Orange ]
 
([], inputs) ||> List.fold takeMore