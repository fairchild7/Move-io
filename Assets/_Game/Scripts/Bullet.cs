using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    TestBullet,
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

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletData", order = 1)]
public class Bullet : MonoBehaviour
{
    public Type bulletType;
    public AttackType attackType;
    public GameObject prefab;

    protected Rigidbody rb;

    private float bulletLifeTime = 1.5f;

    public int id;

    private void Start()
    {
        
    }

    public virtual void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 5f;
        StartCoroutine(DespawnOnLifeTime());
    }

    public virtual void OnDespawn()
    {
        StopAllCoroutines();
        SimplePool.Instance.Despawn(this);
    }
    
    private IEnumerator DespawnOnLifeTime()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        OnDespawn();
    }
}
