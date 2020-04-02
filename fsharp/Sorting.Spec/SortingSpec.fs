module SortingSpec

open NUnit.Framework
open FsCheck

[<Test>]
let ``a sorted list is same size as original``() =
    let sizeIsMaintained (aList: int list) =
        let left =
            aList
            |> List.sort
            |> List.length

        let right = aList |> List.length
        left = right

    Check.QuickThrowOnFailure sizeIsMaintained

[<Test>]
let ``a sorted list is ordered ascending pairs``() =
    let orderedPairs (aList: int list) =
        aList
        |> List.sort
        |> List.pairwise
        |> List.forall (fun (a, b) -> a <= b)

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
