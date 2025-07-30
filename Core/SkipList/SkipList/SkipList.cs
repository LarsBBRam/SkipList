using System;
using System.Collections;
using System.Globalization;
using System.Xml.Schema;
using SkipList.Node;

namespace SkipList.SkipList;

public class SkipList<T>(int maxLevel = 16) : IEnumerable<T>, ICollection<T> where T : IComparable<T>
{
    private readonly SkipListNode<T> _header = new(default!, maxLevel);
    private readonly SkipListNode<T> _end = new(default!, maxLevel);
    private readonly int _maxLevel = maxLevel;
    private readonly Random random = new();

    public int Count { get; private set; } = 0;

    public bool IsReadOnly { get; private set; }

    public bool Delete(T value)
    {
        var current = _header;
        var updateArray = new SkipListNode<T>[_maxLevel + 1];
        for (var i = _maxLevel; i >= 0; i--)
        {
            while (current.Forwards[i] is not null && current.Forwards[i].Value.CompareTo(value) < 0)
            {
                current = current.Forwards[i];
            }
            updateArray[i] = current;
        }
        current = current.Forwards[0];

        if (current is not null && current.Value.CompareTo(value) == 0)
        {
            for (var i = 0; i <= _maxLevel; i++)
            {
                if (updateArray[i].Forwards[i] != current) break;
                updateArray[i].Forwards[i] = current.Forwards[i];
            }
            Count--;
            return true;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = _header.Forwards[0];
        while (current is not null)
        {
            yield return current.Value;
            current = current.Forwards[0];
        }
    }

    public void Insert(T value)
    {
        var current = _header;
        SkipListNode<T>[] updates = new SkipListNode<T>[_maxLevel + 1];
        for (var i = _maxLevel; i >= 0; i--)
        {
            while (current.Forwards[i] != null && current.Forwards[i].Value.CompareTo(value) < 0)
            {
                current = current.Forwards[i];
            }
            updates[i] = current;
        }

        current = current.Forwards[0];
        if (current == null || current.Value.CompareTo(value) != 0)
        {
            int level = RandomLevel();
            SkipListNode<T> node = new(value, level);
            for (var i = 0; i <= level; i++)
            {
                node.Forwards[i] = updates[i].Forwards[i];
                updates[i].Forwards[i] = node;
            }
        }
    }

    public SkipListNode<T> Search(T value)
    {
        var current = _header;
        for (var i = _maxLevel; i >= 0; i--)
        {
            while (current.Forwards[i] is not null && current.Forwards[i].Value.CompareTo(value) < 0)
            {
                current = current.Forwards[i];
            }

        }
        current = current.Forwards[0];
        return current;
    }


    public void Update(T value, T newValue)
    {
        Delete(value);
        Insert(newValue);
    }

    private int RandomLevel()
    {
        int level = 0;
        while (random.NextDouble() < 0.5 && level < _maxLevel)
        {
            level++;
        }
        return level;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item) => Insert(item);

    public void Clear()
    {

    }

    public bool Contains(T item)
    {
        return Search(item) is not null;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        try
        {
            var items = this.Skip(arrayIndex).ToArray();
            for (var i = arrayIndex; i < array.Length; i++)
            {
                array[i] = items[i];
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public bool Remove(T item) => Delete(item);

}
