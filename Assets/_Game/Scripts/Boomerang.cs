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
        StartCoroutine(GoBack());
    }

    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(2f);
        rb.velocity = -dir * 10f;
        yield return new WaitForSeconds(2f);
        OnDespawn();
    }
}
