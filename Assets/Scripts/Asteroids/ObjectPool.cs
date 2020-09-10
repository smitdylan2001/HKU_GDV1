using System;
using System.Collections.Generic;
using System.Numerics;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> _activePool = new List<T>(); 
    private List<T> _inactivePool = new List<T>();

    private T AddNewItemToPool()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        _inactivePool.Add(instance);
        UnityEngine.Debug.Log("Added object to pool");
        return instance;
    }

    private T RequestItem()
    {
        if (_inactivePool.Count > 0) { return ActivateItem(_inactivePool[0]); }
        return ActivateItem(AddNewItemToPool());
    }
    private T ActivateItem(T item)
    {
        item.OnActivate(1, new UnityEngine.Vector2(1,2), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(1, 6));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }
    private T ActivateItem(T item, float size, Vector2 startPos)
    {
        item.OnActivate(1, new UnityEngine.Vector2(1, 2), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(1, 6));
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        return item;
    }
    private T ReturnObjectToInactive(T item)
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
