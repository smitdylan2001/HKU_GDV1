﻿using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
	public List<T> ActivePool { get; private set; } = new List<T>();
	public List<T> InactivePool { get; private set; } = new List<T>();

	/// <summary> 
	/// Add new item and add it to the active pool
	/// </summary>
	private T AddNewItemToPool()
	{
		T instance = (T)Activator.CreateInstance(typeof(T));
		ActivePool.Add(instance);
		return instance;
	}

	/// <summary> 
	/// Add new item and add it to the active pool with overflow for smaller asteroids
	/// </summary>
	private T AddNewItemToPool(float size, UnityEngine.Vector3 startPos, float rotation)
	{
		T instance = (T)Activator.CreateInstance(typeof(T));
		ActivePool.Add(instance);
		instance.OnActivate(size, startPos, rotation, UnityEngine.Random.Range(0.0005f, 0.03f));
		return instance;
	}

	/// <summary> 
	/// Request an item. If there are items inactive use that, otherwise add a new item 
	/// </summary>
	public T RequestItem()
	{
		if(InactivePool.Count > 0)
		{
			return ActivateItem(InactivePool[0]);
		}
		return ActivateItem(AddNewItemToPool());
	}

	/// <summary>
	/// Same as above with overflow for smaller objects with positions. Request an item with 2 viriables. If there are items inactive use that, otherwise add a new item
	/// </summary>
	//  TODO: Clean up to use an interface instead of variables in the overflow
	public T RequestItem(float size, UnityEngine.Vector3 startPos, float rotation)
	{
		if(InactivePool.Count > 0)
		{
			return ActivateItem(InactivePool[0], size, startPos, rotation);
		}
		return ActivateItem(AddNewItemToPool(size, startPos, rotation), size, startPos, rotation);
	}

	/// <summary> 
	/// Activate an item 
	/// </summary>
	public T ActivateItem(T item)
	{
		//              size, startPosition                                                                               , Direction                       , Speed);
		item.OnActivate(1, new UnityEngine.Vector3(UnityEngine.Random.Range(-11, 11), UnityEngine.Random.Range(-6, 6), 0), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0.0005f, 0.03f));
		item.Active = true;
		if(InactivePool.Contains(item))
		{
			InactivePool.Remove(item);
			ActivePool.Add(item);
		}
		return item;
	}

	/// <summary> 
	/// Same as above. Activate item with overflow for smaller objects with positions 
	/// </summary>
	//  TODO: Clean up to use an interface instead of variables in the overflow
	public T ActivateItem(T item, float size, UnityEngine.Vector3 startPos, float rotation)
	{
		//              size, startPosition, Direction, Speed);
		item.OnActivate(size, startPos, rotation, UnityEngine.Random.Range(0.0005f, 0.03f));
		item.Active = true;
		if(InactivePool.Contains(item))
		{
			InactivePool.Remove(item);
			ActivePool.Add(item);
		}
		return item;
	}

	/// <summary> 
	/// Return active item to inactive item list
	/// </summary>
	public T ReturnObjectToInactive(T item)
	{
		if(ActivePool.Contains(item))
		{
			ActivePool.Remove(item);
		}
		item.OnDisable();
		item.Active = false;
		InactivePool.Add(item);
		return item;
	}
}
