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
                new[] { 1, 0, 0, 0, 1, 1, 0, 0 },
                new[] { 0, 0, 0, 0, 1, 1, 0 },
                new[] { 1, 0, 1, 0, 1, 1, 0 },
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
                .Aggregate((Backspaces: 0, Done: false), (state, currentVal) => // Reverse + Aggregate is a functional right fold
                {
                    switch (state.Done, state.Backspaces, currentVal) // evaluate the # of backspaces calculated so far and the current value
                    {
                        case (true, _, _): return state;                       // we are already done, return the current state until the end is reached 
                        case (_, 0, _):                                        // no backspaces calculated so far, assume 1 (at least) must be returned and continue 
                        case (_, 2, 1): return (1, false);                     // found a 1, but have already calculated 2 backspaces; decrease the count and continue    
                        case (_, int b, 0): return (b, true);                  // found a 0 and have already calculated a non-zero number of backspaces (we are done)
                        case (_, int b, 1) when b < 2: return (b+1, false);    // found a 1 and have calculated less than 2 backspaces; increase the count and continue
                        default: throw new Exception("This condition shouldn't occur in the context of this problem.");
                    }
                }, x => x.Backspaces);
    }
}
