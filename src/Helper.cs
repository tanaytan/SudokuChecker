using System;
using System.Collections.Generic;
using System.Text;

namespace sudokuChecker
{
    public static class Helper
    {
        public static IEnumerable<int> Range(int start, int stop, int step = 1)
        {
            if (step == 0)
                throw new ArgumentException(nameof(step));

            return RangeIterator(start, stop, step);
        }
        private static IEnumerable<int> RangeIterator(int start, int stop, int step)
        {
            int x = start;

            do
            {
                yield return x;
                x += step;
                if (step < 0 && x <= stop || 0 < step && stop <= x)
                    break;
            }
            while (true);
        }
        public static IEnumerable<int> Range(int stop) => RangeIterator(0, stop, 1);

      
    }
}
