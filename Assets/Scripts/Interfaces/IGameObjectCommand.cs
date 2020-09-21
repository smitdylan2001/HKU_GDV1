using UnityEngine;

public interface IGameObjectCommand
{
    void Execute(GameObject origin);
}
