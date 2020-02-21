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
        private System.Random r = new System.Random();

        private IEnumerable<int> nRandomInts(int n)
        {
            return Enumerable.Range(0, n).Select(x => r.Next());
        }
        [Test]
        public void AdditionIsCommutative()
        {
            Func<int, int, bool> commutativePropertyOfAddition = (x, y) => x.Add(y).Equals(y.Add(x));
            Prop.ForAll(commutativePropertyOfAddition).QuickCheck();
        }

        [Test]
        public void AdditionIsAssociative()
        {
            Func<int, int, int, bool> associativePropertyOfAddition = (x, y, z) => x.Add(y).Add(z).Equals(z.Add(y).Add(x));
            Prop.ForAll(associativePropertyOfAddition).QuickCheck();
        }

        [Test]
        public void IdentityOfAdditionIsPlus0()
        {
            Func<int, bool> identityOfAddition = x => x.Add(0).Equals(x);
            Prop.ForAll(identityOfAddition).QuickCheck();
        }

        [Test]
        public void MultiplcationIsCommutative()
        {
            Func<int, int, bool> commutativePropertyOfAddition = (x, y) => x.Add(y).Equals(y.Add(x));
            Prop.ForAll(commutativePropertyOfAddition).QuickCheck();
        }

        [Test]
        public void MultiplcationIsAssociative()
        {
            Func<int, int, int, bool> associativePropertyOfMultiplication = (x, y, z) => x.Multiply(y).Multiply(z).Equals(z.Multiply(y).Multiply(x));
            Prop.ForAll(associativePropertyOfMultiplication).QuickCheck();
        }

        [Test]
        public void IdentityOfMultiplicationIsTimes1()
        {
            Func<int, bool> identityOfMultiplication = x => x.Multiply(1).Equals(x);
            Prop.ForAll(identityOfMultiplication).QuickCheck();
        }

        [Test]
        public void DistributiveProperty()
        {
            Func<int, int, int, bool> distributivePropertyOfAddition = (x, y, z) => x.Multiply(y.Add(z)).Equals(x.Multiply(y).Add(x.Multiply(z)));
            Prop.ForAll(distributivePropertyOfAddition).QuickCheck();
        }

    }
}

