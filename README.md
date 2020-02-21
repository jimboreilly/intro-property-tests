# Property Testing using FsCheck

The motivation of this mini project is to demonstrate the value of property-based testing over traditional input/output pair testing. Two simple examples of functions that have well defined properties are chosen: Addition and Multiplication. Using FsCheck, I prove the commutative, associative and identity properties of each function as well as the distributive property which uses both.



## How to run

This project was initialized using the dotnet CLI and can be easily built/tested using the same



Open a terminal in the directory of the `.sln` file and run the following to build or test

```
dotnet restore
dotnet build
dotnet test
```



## What are the Properties?

* Commutative

  When multiple numbers are added/multiplied, the sum/product is the same regardless of the order of the numbers

  `x + y = y + z`

* Associative

  When multiple numbers are added/multiplied, the sum/product is the same regardless of the groupings of the numbers

  `(x + y) + z = x + (y + z)`

* Identity
  * Any number added with 0 is that original number
  	
    `x + 0 = x`
  	
  * Any number multiplied with 1 is that original number

    `x * 1 = x`

* Distributive

  The sum of two numbers multiplied with a third is equal to the sum of each number multiplied by that third number individually

  `x * (y + z) = x * y + x * z`



## Why Test Properties?

Lets write some tests before we define our implementation of `Add` or `Multiply` to see why property tests could deliver additional confidence in our solution. For these examples, we'll assume that our implementation for both functions will be an extension to the `int` type.

### Testing with Input/Output Pairs

Straightforward unit testing might suggest a calling the function with a known input and output.

```csharp
[Test]
public void OneAndOneIsTwo() {
    Assert.AreEqual(2, 1.Add(1));
}
```

A pessimistic reviewer of this unit test might suggest that, while this is an expected input/output pair, it does not prove that our `Add` function doesn't _always_ return 2 so for quality sake we should add a second test.

```csharp
[Test]
public void TenAndTenIsTwenty() {
    Assert.AreEqual(20, 10.Add(10));
}
```

While true that we gain additional confidence with this extra case, how much confidence did we _really_ gain? We don't know without inspecting the source if the actual implementation for `Add` surmised to something like: 

```csharp
public static int Add(this int x, int y) {
    if(x == 10 && y == 10) return 20;
    else return 2;
}
```

While this is still an incredibly generous assumption for how difficult it might be to incorrectly implement addition, how confident can we be our implementation is actually correct without more test cases? For this reason, it is often be useful to attempt more than one test case for even a single use case. 

Assuming we followed suit and implemented a similar two cases for multiplication we could argue with some little confidence that we have sufficient proof that our implementation of `Add` and `Multiply` are correct. We also know that for maximal confidence we need more cases.

### Reimplementing the Wheel

Any tests produced should ideally prove that `Add` and `Multiply` behave correctly for _any_ input values and not just any preselected ones. These assertions would protect us against a incredibly specific declarative implementation that only works for certain cases. This is similar to checking that a particular FizzBuzz implementation does not just print the correct list for the first 100 values.

```csharp
public static FizzBuzz() {
    Console.WriteLine("1");
    Console.WriteLine("2");
    Console.WriteLine("Fizz");
    Console.WriteLine("4");
    Console.WriteLine("Buzz");
    
    ///... still works!
}
```

Well have to generate test data each time we run the test suite in a way that the generated cases are different each time and that the total number of cases is high enough to be confident that its not just a particular set cases that works, but all of them.

```csharp
[Test]
public void AddingTwoNumbersAsExpected([Random(10)] int x, [Random(10)] int y) {
	var expected = x + y;
    Assert.AreEqual(expected, x.Add(y));
}
```

Using NUnit's random input data feature, we've quickly expanded our test cases to 100 combinations. The problem with these tests now is that since the expected value is unknown until run-time, addition had to be reimplemented to produce the expected value. A test the reimplements its source for the sake of comparing the a result to an expected value does not confirm behavior, but merely that the two implementations behave identically. These are fundamentally different. For simple logic, such as arithmetic, this might be easy to grant as an exception. Its easy to see that if the above is true then the `Add` function is behaving correctly, however for more complicated solutions that luxury will not always be available.  If reimplementing the source to prove that it works as expected is off the table, other _properties_ about the source will have to be proven.

### Onto Properties

Addition and Multiplication make great examples for property-based testing because the properties that define their unique behavior have simple proofs that you have likely seen before. Well start by defining and testing the properties that they share in common: the Associative and Commutative properties.

FsCheck is a .NET testing utility that will allow us to define the property of a function as a specification and then use a large number of randomly generated test cases to try and prove that property false. The specifications written for FsCheck tests will clearly define the unique expected behavior of `Add` and `Multiply` in better detail than input/output pairs by being examples of the concrete laws that apply over all cases (or cases within optional specified constraints, such as a function `Division` being undefined for 0). Starting first with the Commutative property of addition, we'll define a function that returns a `bool` and implements the property.

```    csharp
[Test]
public void AdditionIsCommutative() {
    Func<int, int, bool> commutativeProperty = (x, y) => x.Add(y).Equals(y.Add(x));
    Prop.ForAll(commutativeProperty).QuickCheck();
}
```

This defines the Commutative property of `x + y = y + z` as a function that takes two `int`'s and returns a `bool` and passes it to FsCheck's `Prop.ForAll` interface, which is a way to say "for all integers x, y" and will generate random input data and test it against our implementation. The default for `QuickCheck` is to attempt 100 cases, matching our previous random data test but without reimplementing the `Add` function. This test gives confidence in our implementation because the Commutative property of Addition is true across the set of all numbers so any failure would indicate a regression. We can repeat the same for the Associative property  `(x + y) + z = x + (y + z)` but these two properties are shared with Multiplication so we also need a proof uniquely true to addition, namely the Additive Identity `x + 0 = x`.

```    csharp
[Test]
public void AdditionIsAssociative() {
	Func<int, int, int, bool> associativeProperty = (x, y, z) 
    	=> x.Add(y).Add(z).Equals(z.Add(y).Add(x));
    Prop.ForAll(associativeProperty).QuickCheck();
}

[Test]
public void IdentityOfAdditionIsPlus0() {
	Func<int, bool> additiveIdentity = x => x.Add(0).Equals(x);
    Prop.ForAll(additiveIdentity).QuickCheck();
}
```

We can also implement all three property tests for Multiplication.

```    csharp
[Test]
public void MultiplicationIsCommutative() {
    Func<int, int, bool> commutativeProperty = (x, y) =>
        x.Multiply(y).Equals(y.Multiply(x));
    Prop.ForAll(commutativeProperty).QuickCheck();
}

[Test]
public void MultiplicationIsAssociative() {
	Func<int, int, int, bool> associativeProperty = (x, y, z) 
    	=> x.Multiply(y).Multiply(z).Equals(z.Multiply(y).Multiply(x));
    Prop.ForAll(commutativeProperty).QuickCheck();
}

[Test]
public void IdentityOfMultplicationIsTimes1() {
	Func<int, bool> multiplicativeIdentity = x => x.Multiply(1).Equals(x);
    Prop.ForAll(multiplicativeIdentity).QuickCheck();
}
```

Through these 6 tests, we can have confidence that our implementations of `Add` and `Multiply` behave correctly for all possible inputs in a way that input/output pair testing. More over we can test the _integration_ between `Add` and `Multiply` with another property: The Distributive property of multiplication over addition.

### Properties upon Properties

The distributive property `x * (y + z) = x * y + x * z`  is unique compared to the properties tested above in that it interacts with both functions. Although the above tests define unique specifications to implement `Add` and `Multiply` against, there is also a property defining how they interact together with a given input. This test can be implemented as easily as any of the above examples.

```csharp
[Test]
public void MultiplicationIsDistributiveOverAddition() {
    Func<int, int, int, bool> distributiveProperty = (x, y, z) =>
        x.Multiply(y.Add(z)).Equals(x.Multiply(y).Add(x.Multiply(z)));
    Prop.ForAll(distributiveProperty).QuickCheck();
}
```

This test would only work for all input if `Add` and `Multiply` both behave correctly. This test in itself doesn't provide _too much_ additional confidence of our source implementation but with an existing property test framework such as FsCheck imported, several property tests written, the effort to add one more is minimal. The number of tests written is a tradeoff with confidence in source behavior but with very few tests all bases of mathematical properties for these two arithmetic operators are covered.

### Summary

Confidence in the behavior of a source code implementation is found in testing, be it manual or a automated testing suite. Not all tests provide equal levels of confidence, as a single input/output pair cannot alone prove that the behavior over all cases, and writing all test cases is not scalable. 

Tools like NUnit and FsCheck provide generators for random input data to scale the number of test cases run against our source, but random data cannot have an expected value without reimplementing the source.

Behaviors defined through property based tests provide a specification that code can be built against without relying on a reimplementation. By writing tests for the properties that uniquely identify one function relative to any other, we have a specification for a scalable test suite to maximize our confidence in the source with a small number of tests. Confidence in behavior can be had without a massive tradeoff in time spent writing tests.
