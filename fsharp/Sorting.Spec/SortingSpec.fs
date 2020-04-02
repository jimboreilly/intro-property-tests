module SortingSpec

open NUnit.Framework
open FsCheck

[<Test>]
let ``a sorted list is same size as original``() =
    let sizeIsMaintained (x: int list) =
        let left =
            x
            |> Seq.sort
            |> Seq.length

        let right = x |> Seq.length
        left = right

    Check.QuickThrowOnFailure sizeIsMaintained

[<Test>]
let ``a sorted list is ordered ascending pairs``() =
    let orderedPairs (x: int list) =
        x
        |> Seq.sort
        |> Seq.pairwise
        |> Seq.forall (fun (a, b) -> a <= b)

    Check.QuickThrowOnFailure orderedPairs

[<Test>]
let ``sorted list is a permutation of the original list``() =
    let rec removeElem elem list =
        match list with
        | [] -> raise (System.ArgumentException("value is not in the list to be removed"))
        | car :: cdr when car = elem -> cdr
        | car :: cdr -> car :: (removeElem elem cdr)

    let rec isPermutation aList bList =
        match aList with
        | [] -> List.isEmpty bList
        | car :: cdr -> List.contains car bList && isPermutation cdr (removeElem car bList)

    let sortedIsPermutationOfOriginal (aList: int list) = isPermutation aList (List.sort aList)

    Check.QuickThrowOnFailure sortedIsPermutationOfOriginal
