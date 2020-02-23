using NUnit.Framework;
using Sorting;
using System;
using System.Linq;
using FsCheck;


namespace Sorting.Spec
{
    public class SortingSpec
    {

        [Test]
        public void SortedListIsOrderedAscending()
        {
            Func<int[], bool> leftNeverGreaterThanRight = array =>
            {
                var sorted = array.SlowSort().ToList();
                return sorted.Zip(sorted.Skip(1)).All(pair => pair.First <= pair.Second);
            };

            Prop.ForAll(leftNeverGreaterThanRight).QuickCheckThrowOnFailure();
        }
    }
}