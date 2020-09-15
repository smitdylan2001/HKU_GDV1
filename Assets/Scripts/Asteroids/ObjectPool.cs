using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ObjectPool<T> where T : IPoolable
{
    public List<T> _activePool = new List<T>();
    private List<T> _inactivePool = new List<T>();

    /// <summary> 
    /// Add new item and add it to the active pool
    /// </summary>
    private T AddNewItemToPool()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        _activePool.Add(instance);
        return instance;
    }

    /// <summary> 
    /// Request an item. If there are items inactive use that, otherwise add a new item 
    /// </summary>
    public T RequestItem()
    {
        if (_inactivePool.Count > 0) 
        { 
            return ActivateItem(_inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    /// <summary>
    /// Same as above with overflow for smaller objects with positions. Request an item with 2 viriables. If there are items inactive use that, otherwise add a new item
    /// </summary>
    // TODO: Clean up to use an interface instead of variables in the overflow
    public T RequestItem(float size, UnityEngine.Vector3 startPos)
    {
        if (_inactivePool.Count > 0)
        {
            return ActivateItem(_inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    /// <summary> 
    /// Activate an item 
    /// </summary>
    public T ActivateItem(T item)
    {
        //TODO Use a generic
        item.OnActivate(1, new UnityEngine.Vector3(UnityEngine.Random.Range(-11, 11), UnityEngine.Random.Range(-6, 6), 0), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0.0005f, 0.03f));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }

    /// <summary> 
    /// Same as above. Activate item with overflow for smaller objects with positions 
    /// </summary>
    // TODO: Clean up to use an interface instead of variables in the overflow
    public T ActivateItem(T item, float size, UnityEngine.Vector3 startPos)
    {
        item.OnActivate(size, startPos, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0.0005f, 0.03f));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }

    /// <summary> 
    /// Return active item to inactive item list
    /// </summary>
    public T ReturnObjectToInactive(T item)
    {
        if (_activePool.Contains(item))
        {
            _activePool.Remove(item);
        }
        item.OnDisable();
        item.Active = false;
        _inactivePool.Add(item);
        return item;
    }
}
