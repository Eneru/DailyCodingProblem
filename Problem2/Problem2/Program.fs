open System

(* 
    Naive version with division allowed
    Complexity O(2n) = O(n):
    - O(n) for the reduce loop
    - O(n) for the map loop
    Space O(2n+1)
*)
let naiveProd (l:int list) =
    let prod = List.reduce (fun acc xs -> acc * xs) l in
    List.map (fun x -> prod / x) l
;;

(* 
    Naive version without division allowed
    Complexity O(n*(n-1)) = O(n²):
    - O(n) for loop on the n numbers
    - O(n-1) for the inner loop (reduce left, then reduce right)
    Space O(2n)
*)
let naiveWithoutDivision (l:int list) =
    let rec naiveWithoutDivision_Helper (prev:int list) (next:int list) (res:int list) =
        match prev,next with
        | _,[]      -> List.rev res
        | [], [x]   -> res
        | _, [x]    ->
            let prodLeft = List.reduce (fun acc xs -> acc * xs) prev in
            naiveWithoutDivision_Helper (x::prev) [] (prodLeft::res)
        |[], t::r   ->
            let prodRight = List.reduce (fun acc xs -> acc * xs) r in
            naiveWithoutDivision_Helper (t::prev) r (prodRight::res)
        | _, t::r   ->
            let prodLeft = List.reduce (fun acc xs -> acc * xs) prev in
            let prodRight = List.reduce (fun acc xs -> acc * xs) r in
            let prod = prodLeft * prodRight in
            naiveWithoutDivision_Helper (t::prev) r (prod::res)
    in
    naiveWithoutDivision_Helper [] l []
;;

[<EntryPoint>]
let main argv =
    let sampleInput = 1::2::3::4::5::[] in
    let sampleOutput = 120::60::40::30::24::[] in
    let naiveOutput = naiveProd sampleInput in
    let naiveWithoutDivOutput = naiveWithoutDivision sampleInput in
    let naiveTest = ((List.compareWith (fun (x:int) (y:int) -> x.CompareTo(y)) sampleOutput naiveOutput) = 0).ToString() in
    let naiveWihtoutDivTest = ((List.compareWith (fun (x:int) (y:int) -> x.CompareTo(y)) sampleOutput naiveWithoutDivOutput) = 0).ToString() in
    printfn "%s, %s\r\nPress a key to leave." naiveTest naiveWihtoutDivTest;
    let a = Console.ReadKey()
    0 // return an integer exit code
