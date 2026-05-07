using System;
using Taller_Arbolitos;

namespace Taller_Arbolitos
{
    public static class TreeClassifier
    {
        public static string Classify<T>(ITree<T> tree) where T : IComparable<T>
        {
            if (tree.IsEmpty()) return "Vacio";
            if (tree is TreeAVL<T>) return "Árbol AVL (Balanceado)";
            return "Árbol Binario de Búsqueda";
        }
    }
}