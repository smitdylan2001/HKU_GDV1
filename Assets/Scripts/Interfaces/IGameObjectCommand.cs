using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObjectCommand
{
    void Execute(GameObject origin);
}
