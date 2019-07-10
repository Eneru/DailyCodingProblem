// Learn more about F# at http://fsharp.org

open System
open System.Text.RegularExpressions

(* From https://markhneedham.com/blog/2009/05/10/f-regular-expressionsactive-patterns/ *)
let (|Match|_|) pattern input =
    let m = Regex.Match(input, pattern) in
    if m.Success then Some (List.tail [ for g in m.Groups -> g.Value ]) else None

type node = Node of node * string * node | None;;

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

(*
    Convert a binary tree to a string like V(VL(VLL)(VLR))(VR(VRL))
    where V is the root value, VL is the value on the left side, VR is the right etc ...
    This tree would be represented as:
    Node ( Node ( Node (None, "VLL", None) , "VL", Node (None, "VLR", None) ) , "V", Node ( Node (None, "VRL", None), "VR", None) )
            V
           / \
          /   \
         /     \
       VL       VR
      / \       /
     /   \     /
    /     \   /
   VLL   VLR VRL

   /!\ No terminal recursion. (may stack overflow)
*)
let rec serialize (curr:node) =
    match curr with
    | Node(None, value, None)   ->
        value
    | Node(None, value, right)  ->
        value+"("+(serialize right)+")"
    | Node(left, value, None)   ->
        value+"("+(serialize left)+")"
    | Node(left, value, right)  ->
        value+"("+(serialize left)+")"+"("+(serialize right)+")"                
    | _                         -> failwith "Empty node encountered during serialization."
;;

(* 
    Convert a string like V(VL(VLL)(VLR))(VR(VRL)) to a binary tree
    where V is the root value, VL is the value on the left side, VR is the right etc ...
    This tree would be represented as:
    Node ( Node ( Node (None, "VLL", None) , "VL", Node (None, "VLR", None) ) , "V", Node ( Node (None, "VRL", None), "VR", None) )
            V
           / \
          /   \
         /     \
       VL       VR
      / \       /
     /   \     /
    /     \   /
   VLL   VLR VRL

   /!\ No terminal recursion. (may stack overflow)
*)
let rec deserialize (s:string) : node =
    match s with
    | Match @"^([^()]+)(.*)$" withValue         -> (* check that a node begins with the value *)
        match List.item 1 withValue with
        | Match @"(\()(.*)(\))(.*)" withLeft    -> (* if there is something like (...) after the value, it is the left part *)
            match List.item 3 withLeft with
            | Match @"(\()(.*)(\))" withRight   -> (* if there is something like (...) after the left child, it is the right part *)
                Node(deserialize(List.item 1 withLeft), List.item 0 withValue, deserialize(List.item 1 withRight))
            | _                                 ->
                Node(deserialize(List.item 1 withLeft), List.item 0 withValue, None)
        | _                                     ->
            Node(None, List.item 0 withValue, None)
    | _                                         -> None
;;

[<EntryPoint>]
let main argv =
    let n = Node( Node( Node(None, "left.left", None), "left", None), "root", Node(None, "right", None) ) in
    let res = (value ( left( left( deserialize (serialize(n) ) ) ) ) = "left.left").ToString() in
    printfn "%s" res
    let pause = Console.ReadKey()
    0 // return an integer exit code
