// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System

type node = Node of node * bool * node | None;;

let left (n:node) =
    match n with
    | None                      -> failwith "The current leaf hasn't got a left child."
    | Node(x, _, _)             -> x
;;

let right (n:node) =
    match n with
    | None                      -> failwith "The current leaf hasn't got a right child."
    | Node(_, _, x)             -> x
;;

let value (n:node) =
    match n with
    | Node(_, v, _) -> v
    | None          -> failwith "Their is no leaf at the current place."
;;

let isLeaf (n:node) =
    match n with
    | Node(None, _, None)   -> true
    | _                     -> false
;;

let hasLeftLeaf (n:node) =
    isLeaf(left(n))
;;

let hasRightLeaf (n:node) =
    isLeaf(right(n))
;;

let cptUnival (n:node) =
    match n with
    | Node(l, v, r) ->
        let rec cptUnival_aux (current:bool) (leftChild:node) (rightChild:node) =            
            if leftChild = None && rightChild = None then
                1
            else
                let leftV = value(leftChild) in
                let rightV = value(rightChild) in
                if leftV <> rightV || current <> leftV || current <> rightV then
                    (cptUnival_aux leftV (left(leftChild)) (right(leftChild)))
                    + (cptUnival_aux rightV (left(rightChild)) (right(rightChild)))
                else
                    1 + (cptUnival_aux leftV (left(leftChild)) (right(leftChild)))
                    + (cptUnival_aux rightV (left(rightChild)) (right(rightChild)))
        in cptUnival_aux v l r
    | None          -> 0
;;

[<EntryPoint>]
let main argv = 
    let example = Node(Node(None, true, None), false,
                    Node(Node(Node(None, true, None),true, Node(None, true, None)), false,
                        Node(None, false, None)))
    let res = cptUnival example
    printfn "%d" res
    let pause = Console.ReadKey()
    0 // return an integer exit code
