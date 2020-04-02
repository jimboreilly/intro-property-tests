using NUnit.Framework;
using Arithmetic;
using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arithmetic.Spec
{
    public class ArithmeticSpec
    {
        [Test]
        public void AdditionIsCommutative()
        {
            Func<int, int, bool> commutativeProperty = (x, y) => x.Add(y).Equals(y.Add(x));
            Prop.ForAll(commutativeProperty).QuickCheck();
        }

        [Test]
        public void AdditionIsAssociative()
        {
            Func<int, int, int, bool> associativeProperty = (x, y, z) => x.Add(y).Add(z).Equals(z.Add(y).Add(x));
            Prop.ForAll(associativeProperty).QuickCheck();
        }

        [Test]
        public void IdentityOfAdditionIsPlus0()
        {
            Func<int, bool> additiveIdentity = x => x.Add(0).Equals(x);
            Prop.ForAll(additiveIdentity).QuickCheck();
        }

        [Test]
        public void MultiplicationIsCommutative()
        {
            Func<int, int, bool> commutativeProperty = (x, y) => x.Multiply(y).Equals(y.Multiply(x));
            Prop.ForAll(commutativeProperty).QuickCheck();
        }

        [Test]
        public void MultiplicationIsAssociative()
        {
            Func<int, int, int, bool> associativeProperty = (x, y, z) => x.Multiply(y).Multiply(z).Equals(z.Multiply(y).Multiply(x));
            Prop.ForAll(associativeProperty).QuickCheck();
        }

        [Test]
        public void IdentityOfMultplicationIsTimes1()
        {
            Func<int, bool> multiplicativeIdentity = x => x.Multiply(1).Equals(x);
            Prop.ForAll(multiplicativeIdentity).QuickCheck();
        }

        [Test]
        public void MultiplicationIsDistributiveOverAddition()
        {
            Func<int, int, int, bool> distributiveProperty = (x, y, z) => x.Multiply(y.Add(z)).Equals(x.Multiply(y).Add(x.Multiply(z)));
            Prop.ForAll(distributiveProperty).QuickCheck();
        }

    }
}

