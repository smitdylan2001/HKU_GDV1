using UnityEngine;

/// <summary>
/// Similair to the Command class. But this executes from a GameObject instead.
/// </summary>
public interface IGameObjectCommand
{
	void Execute(GameObject origin);
}
