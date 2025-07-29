using System;
using System.Collections;
using System.Globalization;
using System.Xml.Schema;
using SkipList.Node;

namespace SkipList.SkipList;

public class SkipList<T>(int maxLevel = 16) : IEnumerable<T>, ISkipList<T> where T : IComparable<T>
{
    private readonly SkipListNode<T> _header = new(default!, maxLevel);
    private readonly SkipListNode<T> _end = new(default!, maxLevel);
    private readonly int _maxLevel = maxLevel;
    private readonly Random random = new();

    public void Delete(T value)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
}
