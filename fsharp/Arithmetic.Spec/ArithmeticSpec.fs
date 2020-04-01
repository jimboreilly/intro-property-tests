module ArithmeticSpec

open NUnit.Framework
open FsUnit

open Example.Arithmetic


[<Test>]
let ``this is a test test``() = add 2 2 |> should equal 4

[<Test>]
let ``this one is also a test``() = multiply 2 3 |> should equal 6
