module Validation

open System
open FsToolkit.ErrorHandling
// open Browser

let validateFirstName firstName = 
    match String.IsNullOrEmpty(firstName) with
    | true -> Error "FirstName is required."
    | false ->
        match firstName.Length with
        | len when len < 3 -> Error "Firstname should be min 3 chars"
        | len when len > 10 -> Error "Firstname should be max 10 chars"
        | _ -> Ok firstName

let validateLastName lastName = 
    match String.IsNullOrEmpty(lastName) with
    | true -> Error "LastName is required."
    | false ->
        match lastName.Length with
        | len when len < 3 -> Error "LastName should be min 3 chars"
        | len when len > 10 -> Error "LastName should be max 20 chars"
        | _ -> Ok lastName

let titles = ["Mr"; "Mrs"; "Ms"; "Miss"]

let (|IsValidTitle|_|) title = 
    List.contains title titles
    |> function
        | true -> Some ()
        | false -> None

let validateTitle title = 
    match title with
    | null | "" -> Error "Title is required." 
    | IsValidTitle -> Ok title
    | _ -> Error $"{title} is not a valid title."


type person = {
    title: string
    firstName: string
    lastName: string
}

let validate person = 
    [
        validateTitle person.title
        validateFirstName person.firstName
        validateLastName person.lastName
    ]
    |> List.traverseResultA id
    |> function
        | Ok _ -> []
        | Error errors -> errors


// let div = document.createElement "div"
// div.innerHTML <- "Hello world!"
// document.body.appendChild div |> ignore


// let writeToFile (firstName:string) = 
//     match validateFirstName firstName with
//     | Ok name -> fs.writeFileSync("test.txt",  $"Hello {name}")
//     | Error err -> printfn $"{err}"

// writeToFile null

// printfn "Hello from F#"