using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : IGameObjectCommand
{
    public void Execute(GameObject origin)
    {
        new Bullet(origin.transform.position, 0.25f, origin.transform.rotation, 300f);
    }
}
