using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    public static class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] values) where T : IComparable
        {
            var tree = new BinaryTree<T>();
            foreach (var value in values)
                tree.Add(value);
            return tree;
        }
    }

    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        private SortedSet<T> sortedSet = new SortedSet<T>();

        public class Vertex
        {
            internal T Value;
            internal Vertex Left;
            internal Vertex Right;
            public Vertex(T Value)
            {
                this.Value = Value;
                Left = null;
                Right = null;
            }
        }

        private Vertex Root { get; set; }
        public T Value => Root.Value;
        public Vertex Left => Root.Left;
        public Vertex Right => Root.Right;

        public void Add(T element)
        {
            if (Root == null)
            {
                Root = new Vertex(element);
                sortedSet.Add(element);
                return;
            }

            Vertex nowVertex = Root;

            while (true)
            {
                if (element.CompareTo(nowVertex.Value) == -1 || element.CompareTo(nowVertex.Value) == 0)
                   if (nowVertex.Left != null) 
                        nowVertex = nowVertex.Left;
                   else
                   {
                        nowVertex.Left = new Vertex(element);
                        sortedSet.Add(element);
                        break;
                   }
                if (element.CompareTo(nowVertex.Value) == 1)
                    if (nowVertex.Right != null)
                        nowVertex = nowVertex.Right;
                    else
                    {
                        nowVertex.Right = new Vertex(element);
                        sortedSet.Add(element);
                        break;
                    }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in sortedSet)
                yield return element;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
