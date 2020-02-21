using System;

namespace Sorting
{
    public static class Sorting
    {

        private static void slowSort(int[] A, int i, int j)
        {
            if (i >= j) return;
            var m = (i + j) / 2;
            slowSort(A, i, m);
            slowSort(A, m + 1, j);
            if (A[m] > A[j])
            {
                var x = A[m];
                A[m] = A[j];
                A[j] = x;
            }
            slowSort(A, i, j - 1);
        }

        public static int[] SlowSort(this int[] array)
        {
            slowSort(array, 0, array.Length - 1);
            return array;
        }
    }
}
