// Learn more about F# at http://fsharp.org

open System

let cons (a:int) (b:int) =
    let pair f =
        f a b
    pair
;;

let car (a:int) (b:int) =
    a
;;

let cdr (a:int) (b:int) =
    b
;;

[<EntryPoint>]
let main argv =
    let input = cons 3 4
    printfn "car(cons(3, 4)) = %d, cdr(cons(3, 4)) = %d" (input car) (input cdr)
    let k = Console.ReadKey()
    0 // return an integer exit code
