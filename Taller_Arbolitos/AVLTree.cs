using System;
using System.Collections.Generic;
using System.Text;

namespace Taller_Arbolitos
{
    public class AVLTree<T> where T : IComparable<T> /* TreeBinary<T> where T : IComparable<T>*/
    {
        public NodeTree<T>? Root;

        private int Height(NodeTree<T>? node)
        {
            // Si el nodo no existe, altura = 0
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(NodeTree<T>? node)
        {
            //si el nodo no existe, regresa al anterior
            if (node == null)
                return 0;

            // altura izquierda - altura derecha
            return Height(node.Left) - Height(node.Right);
        }

        private NodeTree<T> RightRotate(NodeTree<T> y)
        {
            NodeTree<T> x = y.Left!;
            NodeTree<T>? T2 = x.Right;

            // Rotación
            x.Right = y;
            y.Left = T2;

            // Actualizar alturas
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            // Nueva raíz
            return x;
        }

        private NodeTree<T> LeftRotate(NodeTree<T> x)
        {
            NodeTree<T> y = x.Right!;
            NodeTree<T>? T2 = y.Left;

            // Rotación
            y.Left = x;
            x.Right = T2;

            // Actualizar alturas
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            // Nueva raíz
            return y;
        }

        public void Insert(T data)
        {
            //llama a un metodo recursivo privado
            Root = InsertRec(Root, data);
        }

        private NodeTree<T> InsertRec(NodeTree<T>? node, T data)
        {
            // INSERTAR COMO BST NORMAL
            if (node == null)
                return new NodeTree<T>(data);

            int comparison = data.CompareTo(node.Data);

            if (comparison < 0)
                node.Left = InsertRec(node.Left, data);

            else if (comparison > 0)
                node.Right = InsertRec(node.Right, data);

            else
                return node; // no duplicados

            // ACTUALIZAR ALTURA
            node.Height =
                1 + Math.Max(Height(node.Left), Height(node.Right));


            // CALCULAR BALANCE
            int balance = GetBalance(node);

            // CASO 1 — Left Left
            if (balance > 1 && data.CompareTo(node.Left!.Data) < 0)
                return RightRotate(node);

            // CASO 2 — Right Right
            if (balance < -1 && data.CompareTo(node.Right!.Data) > 0)
                return LeftRotate(node);

            // CASO 3 — Left Right
            if (balance > 1 && data.CompareTo(node.Left!.Data) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // CASO 4 — Right Left
            if (balance < -1 && data.CompareTo(node.Right!.Data) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public bool Contains(T data)
        {
            //al igual que "insert", "contains" llama al metodo recursivo
            return ContainsRec(Root, data);
        }

        private bool ContainsRec(NodeTree<T>? node, T data)
        {
            // Si llegamos a null, no existe
            if (node == null)
                return false;

            int comparison = data.CompareTo(node.Data);

            // Encontrado
            if (comparison == 0)
                return true;

            // Buscar izquierda
            if (comparison < 0)
                return ContainsRec(node.Left, data);

            // Buscar derecha
            return ContainsRec(node.Right, data);
        }

        public T Remove(T data)
        {
            if (!Contains(data))
                throw new Exception("El valor no existe.");

            Root = RemoveRec(Root, data);

            return data;
        }

        private NodeTree<T>? RemoveRec(NodeTree<T>? node, T data)
        {
            if (node == null)
                return null;

            int comparison = data.CompareTo(node.Data);

            // BUSCAR EL NODO
            if (comparison < 0)
            {
                node.Left = RemoveRec(node.Left, data);
            }
            else if (comparison > 0)
            {
                node.Right = RemoveRec(node.Right, data);
            }

            // NODO ENCONTRADO
            else
            {
                // CASO 1: SIN HIJOS
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }

                // CASO 2: SOLO HIJO DERECHO
                if (node.Left == null)
                {
                    return node.Right;
                }

                // CASO 3: SOLO HIJO IZQUIERDO
                if (node.Right == null)
                {
                    return node.Left;
                }

                // CASO 4: DOS HIJOS
                NodeTree<T> successor = GetMin(node.Right);

                // Reemplazar valor
                node.Data = successor.Data;

                // Eliminar sucesor
                node.Right = RemoveRec(node.Right, successor.Data);
            }

            // ACTUALIZAR ALTURA (AVL)
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));


            // BALANCE AVL
            int balance = GetBalance(node);

            // Left Left
            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RightRotate(node);

            // Left Right
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Right Right
            if (balance < -1 && GetBalance(node.Right) <= 0)
                return LeftRotate(node);

            // Right Left
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            return node;
        }

        private NodeTree<T> GetMin(NodeTree<T> node)
        {
            NodeTree<T> current = node;

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }


    }
}
