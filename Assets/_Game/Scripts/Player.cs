using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick joystick;

    [SerializeField] float moveSpeed = 5f;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentBullet.GetComponent<Bullet>() != null)
            {
                currentBullet = new Boomerang();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
            ChangeAnimatorParameter("IsIdle", false);
            canAttack = false;
            numBullet = 1;
        }
        else
        {
            ChangeAnimatorParameter("IsIdle", true);
            canAttack = true;
        }
    }
}
