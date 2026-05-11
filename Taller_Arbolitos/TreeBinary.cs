using System;

namespace Taller_Arbolitos
{
    public class TreeBinary<T> : ITree<T> where T : IComparable<T>
    {
        protected NodeTree<T>? root;
        protected int count;
        public string recorridoActual { get; protected set; } = "";

        public virtual void Insert(T data) => root = insertRecursive(root, data);

        protected virtual NodeTree<T> insertRecursive(NodeTree<T>? node, T data)
        {
            if (node == null) { count++; return new NodeTree<T>(data); }
            if (data.CompareTo(node.Data) < 0) node.Left = insertRecursive(node.Left, data);
            else if (data.CompareTo(node.Data) > 0) node.Right = insertRecursive(node.Right, data);

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return node;
        }

        // ---Lógica de Eliminación para BST ---
        public virtual T Remove(T data)
        {
            if (Contains(data))
            {
                root = removeRecursive(root, data);
            }
            return data;
        }

        protected virtual NodeTree<T> removeRecursive(NodeTree<T>? node, T data)
        {
            if (node == null) return null;

            if (data.CompareTo(node.Data) < 0)
                node.Left = removeRecursive(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = removeRecursive(node.Right, data);
            else
            {
                // Caso 1 y 2: Sin hijos o con un solo hijo
                if (node.Left == null) { count--; return node.Right; }
                if (node.Right == null) { count--; return node.Left; }

                // Caso 3: Dos hijos. Buscamos el menor del subárbol derecho
                node.Data = minValue(node.Right);
                node.Right = removeRecursive(node.Right, node.Data);
            }
            return node;
        }

        protected T minValue(NodeTree<T> node)
        {
            T minv = node.Data;
            while (node.Left != null) { minv = node.Left.Data; node = node.Left; }
            return minv;
        }
        // ----------------------------------------------

        public bool Contains(T data) => findRecursive(root, data) != null;

        private NodeTree<T>? findRecursive(NodeTree<T>? node, T data)
        {
            if (node == null || node.Data.CompareTo(data) == 0) return node;
            return data.CompareTo(node.Data) < 0 ? findRecursive(node.Left, data) : findRecursive(node.Right, data);
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

        public int obtenerAlturaExterna(NodeTree<T>? node) => GetHeight(node);

        public bool IsEmpty() => root == null;
        public void Clear() { root = null; count = 0; recorridoActual = ""; }
        public NodeTree<T>? GetRoot() => root;

        public void PreOrder() { recorridoActual = ""; preOrderRec(root); }
        private void preOrderRec(NodeTree<T>? node) { if (node != null) { recorridoActual += node.Data + " "; preOrderRec(node.Left); preOrderRec(node.Right); } }

        public void InOrder() { recorridoActual = ""; inOrderRec(root); }
        private void inOrderRec(NodeTree<T>? node) { if (node != null) { inOrderRec(node.Left); recorridoActual += node.Data + " "; inOrderRec(node.Right); } }

        public void PostOrder() { recorridoActual = ""; postOrderRec(root); }
        private void postOrderRec(NodeTree<T>? node) { if (node != null) { postOrderRec(node.Left); postOrderRec(node.Right); recorridoActual += node.Data + " "; } }

        public void LevelOrder()
        {
            recorridoActual = "";
            if (root == null) return;

            System.Collections.Generic.Queue<NodeTree<T>> cola = new System.Collections.Generic.Queue<NodeTree<T>>();
            cola.Enqueue(root);

            while (cola.Count > 0)
            {
                var actual = cola.Dequeue();
                recorridoActual += actual.Data + " ";
                if (actual.Left != null) cola.Enqueue(actual.Left);
                if (actual.Right != null) cola.Enqueue(actual.Right);
            }
        }
    }
}