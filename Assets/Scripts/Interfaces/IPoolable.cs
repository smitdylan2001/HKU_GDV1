using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    bool Active { set; get; }
    void OnActivate(float size, Vector2 startPos, float direction, float speed);
    void OnDisable();
}
