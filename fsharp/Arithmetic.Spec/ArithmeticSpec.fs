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
let ``the additive identity is 0``() =
    let identity a = add a 0 = a

    Check.QuickThrowOnFailure identity

[<Test>]
let ``multiplication is associative``() =
    let associativeProperty a b c = multiply a (multiply b c) = multiply (multiply a b) c

    Check.QuickThrowOnFailure associativeProperty

[<Test>]
let ``multiplication is commutative``() =
    let commutativeProperty a b = multiply a b = multiply b a

    Check.QuickThrowOnFailure commutativeProperty

[<Test>]
let ``the multiplicative identity is 1``() =
    let identity a = multiply a 1 = a

    Check.QuickThrowOnFailure identity

[<Test>]
let ``multiplication is distributive over addition``() =
    let distributiveProperty a b c = multiply (add a b) c = add (multiply a c) (multiply b c)

    Check.QuickThrowOnFailure distributiveProperty
