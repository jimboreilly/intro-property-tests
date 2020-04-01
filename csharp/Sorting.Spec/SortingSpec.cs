using NUnit.Framework;
using System.Linq;
using FsCheck;
using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;
using System.Collections.Generic;

namespace Sorting.Spec
{
    [TestFixture]
    public class SortingSpec
    {

        [Property]
        public static bool SortedListIsAscendingPairwise(int[] array)
        {
            var sorted = array.SlowSort().ToList();
            return sorted.Zip(sorted.Skip(1)).All(pair => pair.First <= pair.Second);
        }

        [Property]
        public static bool SortedListMaintainsSizeOfOriginal(int[] array) => array.SlowSort().Count() == array.Count();

        [Property]
        public static bool SortedListIsPermutationOfOriginal(int[] array)
        {
            var originalMap = new Dictionary<int, int>();
            foreach (var x in array)
            {
                if (originalMap.ContainsKey(x)) originalMap[x] += 1;
                else originalMap.Add(x, 1);
            }

            var sorted = array.SlowSort();

            foreach (var x in sorted)
            {
                if (!originalMap.ContainsKey(x) || originalMap[x] == 0) return false;
                if (originalMap.ContainsKey(x)) originalMap[x] -= 1;
            }

            foreach (var pair in originalMap)
            {
                if (pair.Value != 0) return false;
            }

            return true;
        }


    }
}