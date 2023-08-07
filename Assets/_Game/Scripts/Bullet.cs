using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    BasicBullet,
    Hammer,
    Boomerang,
    Arrow,
    Blade,
}

public enum AttackType
{
    Melee,
    Ranged,
}

public class Bullet : MonoBehaviour
{
    public int id;
    public Rigidbody rb;

    [SerializeField]
    public WeaponData weaponData;

    public virtual void OnInit()
    {
        rb.velocity = transform.forward * 5f;
        StartCoroutine(DespawnOnLifeTime());
    }

    public virtual void OnDespawn()
    {
        StopAllCoroutines();
        SimplePool.Instance.Despawn(this);
    }

    protected IEnumerator DespawnOnLifeTime()
    {
        yield return new WaitForSeconds(weaponData.bulletLifeTime);
        OnDespawn();
    }
}
