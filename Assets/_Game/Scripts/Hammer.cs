using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Bullet
{
    public override void OnInit()
    {
        rb.velocity = transform.forward * 5f;
        rb.angularVelocity = Vector3.up * 5f;
        StartCoroutine(DespawnOnLifeTime());
    }
}
