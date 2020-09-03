using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The types of events
/// </summary>
public enum EventType // EventType overwrites UnityEngine.EventType
{
    ON_GAME_START = 0,
    ON_GAME_END = 1
}

/// <summary>
/// The manager that handles all the events (Such as game start and end)
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EventManager<T> 
{
    private static Dictionary<EventType, System.Action<T>> EVENT_DICTIONARY = new Dictionary<EventType, System.Action<T>>();

    /// <summary>
    /// Adds a listener
    /// </summary>
    /// <param name="type"></param>
    /// <param name="function"></param>
    public static void AddListener(EventType type, System.Action<T> function)
    {
        if (!EVENT_DICTIONARY.ContainsKey(type))
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
        if (EVENT_DICTIONARY.ContainsKey(type) && EVENT_DICTIONARY[type] != null)
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
