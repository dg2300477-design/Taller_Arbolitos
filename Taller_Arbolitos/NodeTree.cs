using System;

namespace Taller_Arbolitos
{
    public class NodeTree<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public NodeTree<T>? Left { get; set; }
        public NodeTree<T>? Right { get; set; }
        public int Height { get; set; }

        public NodeTree(T data)
        {
            Data = data;
            Left = null;
            Right = null;
            // Un nodo nuevo inicia con altura 1
            Height = 1;
        }
    }
}