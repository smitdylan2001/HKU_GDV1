using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ObjectPool<T> where T : IPoolable
{
    public List<T> _activePool = new List<T>();
    public List<T> _inactivePool = new List<T>();

    private T AddNewItemToPool()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        _activePool.Add(instance);
        return instance;
    }

    public T RequestItem()
    {
        if (_inactivePool.Count > 0) 
        { 
            return ActivateItem(_inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    public T RequestItem(float size, UnityEngine.Vector3 startPos)
    {
        if (_inactivePool.Count > 0)
        {
            return ActivateItem(_inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T item)
    {
        item.OnActivate(1, new UnityEngine.Vector3(UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-10, 10), 0), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(1, 6));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }

    public T ActivateItem(T item, float size, UnityEngine.Vector3 startPos)
    {
        item.OnActivate(size, startPos, UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(1, 6));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }

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
