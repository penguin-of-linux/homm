using System;
using System.Collections.Generic;
using System.Linq;

namespace HoMM.Generators
{
    public static class Graph
    {
        public static IEnumerable<T> DepthFirstTraverse<T>(
            T start,
            Func<T, IEnumerable<T>> neighborhood,
            Func<T, bool> skip = null)
        {
            var stack = new Stack<T>();

            return Traverse(t => stack.Push(t), () => stack.Pop(), () => stack.Count == 0,
                new Dictionary<T, T>(), start, neighborhood, t => false, skip);
        }

        public static IEnumerable<T> BreadthFirstTraverse<T>(
            T start,
            Func<T, IEnumerable<T>> neighborhood,
            Func<T, bool> skip = null)
        {
            var queue = new Queue<T>();

            return Traverse(t => queue.Enqueue(t), () => queue.Dequeue(), () => queue.Count == 0, 
                new Dictionary<T, T>(), start, neighborhood, t => false, skip);
        }

        public static IEnumerable<T> DepthFirstSearch<T>(
            T start,
            Func<T, IEnumerable<T>> neighborhood,
            Func<T, bool> end,
            Func<T, bool> skip = null)
        {
            var stack = new Stack<T>();
            var parent = new Dictionary<T, T>();

            var last = Traverse(t => stack.Push(t), () => stack.Pop(), () => stack.Count == 0,
                parent, start, neighborhood, end, skip).Last();

            return end(last) ? Trace(parent, last) : Enumerable.Empty<T>();
        }

        public static IEnumerable<T> BreadthFirstSearch<T>(
            T start,
            Func<T, IEnumerable<T>> neighborhood,
            Func<T, bool> end,
            Func<T, bool> skip = null)
        {
            var queue = new Queue<T>();
            var parent = new Dictionary<T, T>();

            var last = Traverse(t => queue.Enqueue(t), () => queue.Dequeue(), () => queue.Count == 0,
                parent, start, neighborhood, end, skip).Last();

            return end(last) ? Trace(parent, last) : Enumerable.Empty<T>();
        }

        private static IEnumerable<T> Trace<T>(Dictionary<T, T> parent, T end)
        {
            var current = end;

            while (current != null) {
                yield return current;
                current = parent[current]; 
            }
        }

        private static IEnumerable<T> Traverse<T>(Action<T> push, Func<T> pop, Func<bool> empty, 
            Dictionary<T, T> parent,
            T start,
            Func<T, IEnumerable<T>> neighborhood,
            Func<T, bool> end,
            Func<T, bool> skip = null)
        {
            push(start);
            parent[start] = default(T);

            while (!empty())
            {
                var current = pop();

                if (skip != null && skip(current))
                    continue;

                foreach (var neighbour in neighborhood(current)
                    .Where(n => !parent.ContainsKey(n)))
                {
                    parent[neighbour] = current;
                    push(neighbour);
                }
                
                yield return current;

                if (end(current)) yield break;
            }
        }
    }
}
