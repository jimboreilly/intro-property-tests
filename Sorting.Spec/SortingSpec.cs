using NUnit.Framework;
using Sorting;
using System.Linq;

namespace Sorting.Spec
{
    public class SortingSpec
    {

        [Test]
        public void Test1()
        {
            var backwardsArray = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var sortedList = backwardsArray.SlowSort().ToList();
            var pairWise = sortedList.Zip(sortedList.Skip(1));
            foreach (var pair in pairWise)
            {
                Assert.IsTrue(pair.First <= pair.Second);
            }
        }
    }
}