using System;
using Taller_Arbolitos;

namespace Taller_Arbolitos
{
    public class TreeBinary<T> : ITree<T> where T : IComparable<T>
    {
        protected NodeTree<T>? root;
        protected int count;

        public string RecorridoActual { get; protected set; } = "";

        public virtual void Insert(T data) => root = InsertRecursive(root, data);

        protected virtual NodeTree<T> InsertRecursive(NodeTree<T>? node, T data)
        {
            if (node == null)
            {
                count++;
                return new NodeTree<T>(data);
            }

            if (data.CompareTo(node.Data) < 0)
                node.Left = InsertRecursive(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = InsertRecursive(node.Right, data);

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            return node;
        }

        public virtual T Remove(T data) { return default; }

        public bool Contains(T data) => FindRecursive(root, data) != null;

        private NodeTree<T>? FindRecursive(NodeTree<T>? node, T data)
        {
            if (node == null || node.Data.CompareTo(data) == 0) return node;
            return data.CompareTo(node.Data) < 0 ? FindRecursive(node.Left, data) : FindRecursive(node.Right, data);
        }

        public T Min()
        {
            if (root == null) throw new InvalidOperationException("Árbol vacío");
            NodeTree<T> current = root;
            while (current.Left != null) current = current.Left;
            return current.Data;
        }

        public T Max()
        {
            if (root == null) throw new InvalidOperationException("Árbol vacío");
            NodeTree<T> current = root;
            while (current.Right != null) current = current.Right;
            return current.Data;
        }

        public int Count() => count;
        public int Height() => GetHeight(root);
        protected int GetHeight(NodeTree<T>? node) => node == null ? 0 : node.Height;

        public bool IsEmpty() => root == null;
        public void Clear() { root = null; count = 0; RecorridoActual = ""; }

        public NodeTree<T>? GetRoot() => root;

        public void PreOrder() { RecorridoActual = ""; PreOrderRec(root); }
        private void PreOrderRec(NodeTree<T>? node) { if (node != null) { RecorridoActual += node.Data + " "; PreOrderRec(node.Left); PreOrderRec(node.Right); } }

        public void InOrder() { RecorridoActual = ""; InOrderRec(root); }
        private void InOrderRec(NodeTree<T>? node) { if (node != null) { InOrderRec(node.Left); RecorridoActual += node.Data + " "; InOrderRec(node.Right); } }

        public void PostOrder() { RecorridoActual = ""; PostOrderRec(root); }
        private void PostOrderRec(NodeTree<T>? node) { if (node != null) { PostOrderRec(node.Left); PostOrderRec(node.Right); RecorridoActual += node.Data + " "; } }

        public void LevelOrder() { }
    }
}