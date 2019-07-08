open System
open System.Diagnostics

(* From https://stackoverflow.com/a/6062399 *)
let genRandomNumbers count min max =
    let rnd = System.Random()
    List.init count (fun _ -> rnd.Next (min, max))

(* Time -> O(n*(n-1)) = O(n²), space -> O(n) *)
let rec naiveSum_Helper (curr:int) (prev:int list) (next:int list) (k:int) : bool =
    match prev, next with
    | [],[]     -> false
    | _,t::r    -> (curr + t) = k || (naiveSum_Helper curr (t::prev) r k)
    | t::r,[]   -> naiveSum_Helper t [] r k
;;

let naiveSum (input:int list) (k:int) : bool =
    match input with
    | []    -> false
    | [x]   -> false
    | t::r  -> naiveSum_Helper t [] r k
;;

(* Time -> O(n) + O(2*log(n)) = O(n), space worst case -> O(n²)
    O(log(n)) to append in the Set
    O(log(n)) to check if we already read the needed number
    O(n) to go through the entire list
*)
let rec naiveSpaceSum_Helper (curr:int list) (present:int Set) (k:int) : bool =
    match curr with
    | []    -> false
    | t::r  ->
        let needed = k - t in
        (Set.contains needed present) || (naiveSpaceSum_Helper r (Set.add t present) k)
 ;;

 let naiveSpaceSum (input:int list) (k:int) : bool =
    naiveSpaceSum_Helper input (Set.empty) k
;;

[<EntryPoint>]
let main argv =
    let sampleK = 26712067 in
    let sampleList = (genRandomNumbers 267120 1 sampleK) in
    let stopWatch = Stopwatch() in
    stopWatch.Start() ;
    let resNaive = (naiveSum sampleList sampleK).ToString() in
    let naiveTime = stopWatch.ElapsedMilliseconds in
    stopWatch.Restart() ;
    let resNaiveSpace = (naiveSpaceSum sampleList sampleK).ToString() in
    let naiveSpaceTime = stopWatch.ElapsedMilliseconds in
    printfn "%s (%d) | %s (%d)" resNaive naiveTime resNaiveSpace naiveSpaceTime ;
    let key = Console.ReadKey() in
    0 // return an integer exit code
