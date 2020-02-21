using NUnit.Framework;
using Sorting;

namespace Sorting.Spec
{
    public class SortingSpec
    {

        [Test]
        public void Test1()
        {
            var backwardsArray = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.AreEqual(array, backwardsArray.SlowSort());
        }
    }
}