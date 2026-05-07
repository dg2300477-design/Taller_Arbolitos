using System;
using System.Collections.Generic;
using System.Text;

namespace Taller_Arbolitos
{
    public class AVLTree<T> where T : IComparable<T>
    {
        public NodeTree<T>? Root;
        private int Height(NodeTree<T>? node)
        {
            // Si el nodo no existe, altura = 0
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(NodeTree<T>? node)
        {
            //si el nodo no existe, va al anterior
            if (node == null)
                return 0;

            // altura izquierda - altura derecha
            return Height(node.Left) - Height(node.Right);
        }
    }
}
