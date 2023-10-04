// More: https://dev.to/askpt/cancellation-tokens-in-f-28gh
open System.Threading
open Microsoft.FSharp.Control

let work = async {
    do! Async.Sleep 2000
    do printfn "Hi!"
}

work
|> Async.RunSynchronously // Run synchronously on the current thread, blocks until done

work
|> Async.Start

printfn "Did this print first?"

///////////////////////////////

let cts = new CancellationTokenSource()

let work' a b =
    async {
        //Async.CancellationToken // Access the cancellation token
        do! Async.SwitchToThreadPool()
        do! Async.Sleep 2000 // Unwrap from async and execute, unit instead of Async<unit>
        return a + b
    }
    
let workTask =
    work' 10 20
    |> Async.StartAsTask

let resultAsync = async {
    let! result = workTask |> Async.AwaitTask
    do printfn $"The result is {result}"
}

Async.Start(resultAsync, cts.Token)
//Async.OnCancel // Execute on cancellation

printfn "did this print first?"


///////////////////////////////////////////
    
// Async "Triggers"
    // Async.Start (Fire and forget on the thread pool)
    // Async.StartImmediate (Fire and forget on the current thread)
    // Async.StartChild (More like async parallel)
    // Async.StartAsTask (Fire a task on thread pool)
    // Async.RunSynchronously (Block the current thread until async is done)

// Awaiting Functions
    // Await task
    // Await Event
    // Await IAsyncResult
    // Await WaitHandle

// Synchronization Context
    // Switch to context
    // Switch to new thread
    // Switch to thread pool
    
// CancellationToken
    // All of the triggering functions support cancellation tokens
    // Async.CancellationToken
    
// Helpers
    // Async.Parallel
    // Sequential
    // Async sleep
    
//////////////////////////////////
    
// Async Parallel Example

open System.IO

let asyncRead path =
    async {
        do printfn $"Reading content from file {path}"
        let content = File.ReadAllText path
        do printfn $"Read content {content} from file {path}"
        return content
    }
    
let asyncWrite path content =
    async {
        do printfn $"Writing content {content} to file {path}"
        do File.WriteAllText(path, content)
        do printfn $"Finished writing content {content} to file {path}"
    }
    
let [<Literal>] File = "File"
let filename num = $"{File}{num}.txt"

seq {1..20}
|> Seq.map (fun num ->filename num, string num)
|> Seq.map (fun (filename, content) -> asyncWrite filename content)
//|> Async.Parallel
|> Async.Sequential
|> Async.Ignore
|> Async.Start

module Async =
    let map f x = async {
        // Defining map from bind and return
        let! x = x
        return f x
    }

let asyncReadWork =
    seq {1..20}
    |> Seq.map filename
    |> Seq.map asyncRead
    |> Async.Sequential
    //|> Async.Parallel
    |> Async.map (Array.map int)
    |> Async.map (Array.reduce (+))
    
asyncReadWork
|> Async.RunSynchronously