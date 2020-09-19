using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustCommand : IGameObjectCommand
{
    private float _thrustPower = 50;
    private Rigidbody2D rb;
    public void Execute(GameObject origin)
    {
        if(!rb) rb = origin.GetComponent<Rigidbody2D>();
        rb.AddForce(origin.transform.transform.up * _thrustPower * Time.deltaTime);
    }
}
