using System;
using SkipList.Node;

namespace SkipList.SkipList;

public interface ISkipList<T> where T : IComparable<T>
{
    SkipListNode<T> Search(T value);
    void Insert(T value);
    void Delete(T value);
    void Update(T value, T newValue);



}
