using System;


namespace Taller_Arbolitos
{
    public interface ITree<T> where T : IComparable<T>
    {
        void Insert(T data);
        T Remove(T data);
        bool Contains(T data);
        T Min();
        T Max();
        int Count();
        int Height();
        bool IsEmpty();
        void Clear();
        void PreOrder();
        void InOrder();
        void PostOrder();
        void LevelOrder();
        NodeTree<T>? GetRoot();
    }
}