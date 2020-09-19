using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The types of events
/// </summary>
public enum EventType // EventType overwrites UnityEngine.EventType
{
	ON_GAME_START = 0,
	ON_GAME_END = 1,
	ON_LOGIC_UPDATE = 2,
	ON_PHYSICS_UPDATE = 3,
	ON_ASTEROID_HIT = 4,
	ON_ASTEROID_DESTROYED = 5
}
/// <summary>
/// The manager that handles all the events without generic (Such as game start and end)
/// </summary>
public static class EventManager
{
	/// <summary>
	/// The dictionary all events are stored in
	/// </summary>
	private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

	/// <summary>
	/// Adds a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void AddListener(EventType type, System.Action function)
	{
		if(!eventDictionary.ContainsKey(type))
		{
			eventDictionary.Add(type, null);
		}
		eventDictionary[type] += function;
	}
	/// <summary>
	/// Removes a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void RemoveListener(EventType type, System.Action function)
	{
		if(eventDictionary.ContainsKey(type) && eventDictionary[type] != null)
		{
			eventDictionary[type] -= function;
		}
	}

	/// <summary>
	/// Executes the event
	/// </summary>
	/// <param name="type"></param>
	public static void InvokeEvent(EventType type)
	{
		//if(eventDictionary[type] != null) Debug.Log(eventDictionary[type]);
		eventDictionary[type]?.Invoke();
	}
}

/// <summary>
/// The manager that handles all the events with generic (Such as game start and end)
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EventManager<T>
{
	/// <summary>
	/// The dictionary all events are stored in
	/// </summary>
	private static Dictionary<EventType, System.Action<T>> EVENT_DICTIONARY = new Dictionary<EventType, System.Action<T>>();

	/// <summary>
	/// Adds a listener
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void AddListener(EventType type, System.Action<T> function)
	{
		if(!EVENT_DICTIONARY.ContainsKey(type))
		{
			EVENT_DICTIONARY.Add(type, null);
		}
		EVENT_DICTIONARY[type] += function;
	}

	/// <summary>
	/// Removes a listener (First in first out)
	/// </summary>
	/// <param name="type"></param>
	/// <param name="function"></param>
	public static void RemoveListener(EventType type, System.Action<T> function)
	{
		if(EVENT_DICTIONARY.ContainsKey(type) && EVENT_DICTIONARY[type] != null)
		{
			EVENT_DICTIONARY[type] -= function;
		}
	}

	/// <summary>
	/// Execute the event
	/// </summary>
	/// <param name="type"></param>
	/// <param name="arg1"></param>
	public static void InvokeEvent(EventType type, T arg1)
	{
		EVENT_DICTIONARY[type]?.Invoke(arg1);
	}
}

