// Learn more about F# at http://fsharp.org

open System

let cons a b =
    let pair f =
        f a b
    pair
;;

let car pair =
    let car_aux a b =
        a
    pair car_aux
;;

let cdr pair =
    let car_aux a b =
        b
    pair car_aux
;;

[<EntryPoint>]
let main argv =
    printfn "car(cons(3, 4)) = %d, cdr(cons(3, 4)) = %d" (car (cons 3 4)) (cdr (cons 3 4))
    let k = Console.ReadKey()
    0 // return an integer exit code
