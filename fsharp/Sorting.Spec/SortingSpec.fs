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
