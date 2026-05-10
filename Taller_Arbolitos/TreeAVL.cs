using System;

namespace Taller_Arbolitos
{
    public class TreeAVL<T> : TreeBinary<T> where T : IComparable<T>
    {
        public override void Insert(T data) => root = insertAVL(root, data);

        private NodeTree<T> insertAVL(NodeTree<T>? node, T data)
        {
            if (node == null) { count++; return new NodeTree<T>(data); }

            if (data.CompareTo(node.Data) < 0) node.Left = insertAVL(node.Left, data);
            else if (data.CompareTo(node.Data) > 0) node.Right = insertAVL(node.Right, data);
            else return node;

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = getBalance(node);

            if (balance > 1 && data.CompareTo(node.Left.Data) < 0) return rightRotate(node);
            if (balance < -1 && data.CompareTo(node.Right.Data) > 0) return leftRotate(node);
            if (balance > 1 && data.CompareTo(node.Left.Data) > 0) { node.Left = leftRotate(node.Left); return rightRotate(node); }
            if (balance < -1 && data.CompareTo(node.Right.Data) < 0) { node.Right = rightRotate(node.Right); return leftRotate(node); }

            return node;
        }

        private int getBalance(NodeTree<T>? node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        private NodeTree<T> rightRotate(NodeTree<T> y)
        {
            NodeTree<T> x = y.Left;
            NodeTree<T> T2 = x.Right;
            x.Right = y; y.Left = T2;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            return x;
        }

        private NodeTree<T> leftRotate(NodeTree<T> x)
        {
            NodeTree<T> y = x.Right;
            NodeTree<T> T2 = y.Left;
            y.Left = x; x.Right = T2;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            return y;
        }
    }
}