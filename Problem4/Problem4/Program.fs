// Learn more about F# at http://fsharp.org

open System

let rec naiveSolution_Helper (input:int list) (existing:int Set) (notexisting:int Set) : int =
    match input with
    | []    -> Set.toList notexisting |> List.head
    | t::r  ->
        
;;

let naiveSolution (input:int list) : int =
    naiveSolution_Helper input Set.empty Set.empty
;;

[<EntryPoint>]
let main argv =
    let testArray = 3::4::-1::1::[] in
    printfn "Hello World from F#!"
    0 // return an integer exit code
