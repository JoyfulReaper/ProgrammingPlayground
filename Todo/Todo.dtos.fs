namespace Todo.Dtos

[<CLIMutable>]
type Todo =
    {
        TodoId : int
        Name: string
        Description: string
        Completed: bool
    }