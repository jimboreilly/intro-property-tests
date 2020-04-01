module ArithmeticSpec

open NUnit.Framework
open FsUnit
open FsCheck

open Example.Arithmetic


[<Test>]
let ``addition is associative``() =
    let associativeProperty a b c = add a (add b c) = add (add a b) c

    Check.QuickThrowOnFailure associativeProperty

[<Test>]
let ``addition is commutative``() =
    let commutativeProperty a b = add a b = add b a

    Check.QuickThrowOnFailure commutativeProperty

[<Test>]
let ``the additive identity is zero``() =
    let identity a = add a 0 = a

    Check.QuickThrowOnFailure identity
