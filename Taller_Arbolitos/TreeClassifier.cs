using System;

namespace Taller_Arbolitos
{
    public static class TreeClassifier
    {
        public static string classify<T>(ITree<T> tree) where T : IComparable<T>
        {
            if (tree.IsEmpty()) return "Vacío";
            if (tree is TreeAVL<T>) return "Árbol AVL (Balanceado)";
            return "Árbol Binario de Búsqueda (BST)";
        }
    }
}