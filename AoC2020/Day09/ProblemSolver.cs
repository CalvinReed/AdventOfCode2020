using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Day09
{
    public class ProblemSolver
    {
        private readonly Queue<long> queue;
        private readonly HashSet<long> set;

        public ProblemSolver(IEnumerable<long> preamble)
        {
            var arr = preamble.ToArray();
            queue = new Queue<long>(arr);
            set = new HashSet<long>(arr);
        }

        public void Enqueue(long n)
        {
            var oldest = queue.Dequeue();
            set.Remove(oldest);
            queue.Enqueue(n);
            set.Add(n);
        }

        public bool IsValid(long n)
        {
            return queue
                .Select(l => new {l, diff = n - l})
                .Where(t => t.diff > 0 && t.diff != t.l)
                .Select(t => t.diff)
                .Any(set.Contains);
        }
    }
}
