using System;
using System.Collections.Generic;
using System.Linq;

namespace kanji
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test harness
            var testCases = new List<int[]> {
                new[] { 0, 1, 0 },
                new[] { 1, 1, 1, 0 },
                new[] { 0, 1, 1, 0 },
                new[] { 1, 1, 1, 1, 0 },
                new[] { 0, 0, 1, 0 },
                new[] { 1, 0, 1, 0, 1, 0 },
                new[] { 1, 0, 1, 0, 1 },
                new[] { 0, 1, 0, 1, 0, 1 },
                new[] { 0, 1, 0, 1, 0 },
                new[] { 1, 0, 0, 0, 1, 0, 0 },
                new[] { 1, 0, 0, 0, 1, 1, 0 },
                new[] { 1, 0 },
                new[] { 0, 1 },
                new[] { 1 },
                new[] { 0 },
                new int[] { }
            };

            foreach (var testCase in testCases)
            {
                testCase.ToList().ForEach(x => Console.Write($"{x} "));
                Console.WriteLine($": {CalculateBackspaces(testCase)}");
            }
        }

        /// <summary>
        /// This solution uses a right-fold to accumulate the number of backspaces, 
        /// carrying that value forward, and evaluating each character, returning an updated number of backspaces
        /// </summary>
        /// <param name="bytes">A list of ints (in this case should be only 0's and 1's) to evaluate</param>
        /// <returns>The number of backspaces required in order to delete the last character (be it Kanji or ASCII)</returns>
        public static int CalculateBackspaces(int[] bytes) => bytes
                .Reverse()
                .Aggregate(0, (backspaces, val) => // Reverse + Aggregate is a functional right fold
                {
                    switch (backspaces, val) // evaluate the # of backspaces calculated so far and the current value
                    {
                        case (0, _): return backspaces + 1;                     // no backspaces yet
                        case (int b, 1) when b == 2: return backspaces - 1;     // 2 backspaces and encountered a 1
                        case (int b, 1) when b < 2: return backspaces + 1;      // < 2 backspaces and encountered a 1
                        case (_, 0): return backspaces;                         // encountered a 0, but already have calculated some backspaces
                        default: throw new Exception("This condition shouldn't occur in the context of this problem.");
                    }
                });
    }
}
