using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    Vector3 dir;

    public override void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        dir = transform.forward;
        rb.velocity = dir * 10f;
        rb.angularVelocity = Vector3.up * 5f;
        Invoke(nameof(GoBack), 2f);
    }

    private void GoBack()
    {
        rb.velocity = -dir * 10f;
        rb.angularVelocity = Vector3.up * 5f;
        Invoke(nameof(OnDespawn), 2f);
    }
}
