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

        // ---Eliminación con auto-balanceo para AVL ---
        public override T Remove(T data)
        {
            // Primero verifica si el valor existe dentro del árbol.
            // Esto evita intentar eliminar algo inexistente.
            if (Contains(data))
            {
                root = removeAVL(root, data);
            }
            return data;
        }

        private NodeTree<T> removeAVL(NodeTree<T>? node, T data)
        {
            if (node == null) return node;
            //si el dato es menor, ira buscando por la izquierda
            if (data.CompareTo(node.Data) < 0) node.Left = removeAVL(node.Left, data);
            //si el dato es mayor, ira buscando por la derecha
            else if (data.CompareTo(node.Data) > 0) node.Right = removeAVL(node.Right, data);
            else
            {   ///caso 1: el nodo un hijo o no tiene ninguno
                if ((node.Left == null) || (node.Right == null))
                {
                    ///temp guarda el hijo que exista(en caso de que haya un hijo)
                    NodeTree<T> temp = null == node.Left ? node.Right : node.Left;
                    ///si temp es nulo(no hay hijos), temp apuntara al noddo para luego eliminarlo
                    if (temp == null) { temp = node; node = null; } 
                    ///en caso ded que haya un solo hijo, el hijo reemplazara al nodo eliminado
                    else { node = temp; }
                    count--;
                }
                ///caso 2: el nodo tiene 2 hijos
                else
                {   ///busca el nodo mas pequeño del arbol derecho
                    NodeTree<T> tempMin = node.Right;
                    ///ira completamente hacia la izquierda
                    while (tempMin.Left != null) tempMin = tempMin.Left;
                    /// Reemplaza el valor del nodo actual con el valor del sucesor
                    node.Data = tempMin.Data;
                    ///elimina el sucesor duplicado del subarbol derecho
                    node.Right = removeAVL(node.Right, tempMin.Data);
                }
            }

            if (node == null) return node;

            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            int balance = getBalance(node);

            if (balance > 1 && getBalance(node.Left) >= 0) return rightRotate(node);
            if (balance > 1 && getBalance(node.Left) < 0) { node.Left = leftRotate(node.Left); return rightRotate(node); }
            if (balance < -1 && getBalance(node.Right) <= 0) return leftRotate(node);
            if (balance < -1 && getBalance(node.Right) > 0) { node.Right = rightRotate(node.Right); return leftRotate(node); }

            return node;
        }
        // -----------------------------------------------------

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