using System;
using System.Collections.Generic;

public class Inventory
{
    private List<IItem> _items;

    public Inventory()
    {
        _items = new();
    }

    public event Action<IItem> ItemAdded;
    public event Action<IItem> ItemRemoved;

    public void Add(IItem item)
    {
        if (item == null)
            return;

        _items.Add(item);
        ItemAdded?.Invoke(item);
    }

    public IItem Take(IItem item)
    {
        if (item == null)
            return null;

        if (Contains(item) == false)
            return null;

        _items.Remove(item);
        ItemRemoved?.Invoke(item);

        return item;
    }

    public bool Contains(IItem item)
    {
        return _items.Contains(item);
    }
}
