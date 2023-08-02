using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] protected Rigidbody rb;

    public Transform throwPoint;

    public int level;

    protected bool canAttack;
    protected bool attackDelay;
    protected bool isDead;

    public int numBullet;
    public List<Character> enemyInRange = new List<Character>();
    public Bullet currentBullet;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        OnInit();
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            if (enemyInRange.Count > 0)
            {
                if (canAttack && numBullet > 0)
                {
                    Attack();
                }
            }
        }
    }

    public virtual void OnInit()
    {
        numBullet = 1;
        isDead = false;
        level = 0;
        canAttack = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
        if (currentBullet.attackType == AttackType.Melee)
        {
            currentBullet.id = GetInstanceID();
        }
    }

    public void ChangeAnimatorParameter(string parameter, bool state)
    {
        animator.SetBool(parameter, state);
    }

    public void AddEnemy(Character enemy)
    {
        enemyInRange.Add(enemy);
    }

    public void RemoveEnemy(Character enemy)
    {
        enemyInRange.Remove(enemy);
    }

    public Character SetTarget()
    {
        float minDist = 10f;
        Character nearestEnemy = null;
        foreach (Character c in enemyInRange)
        {
            if (c.isDead)
            {
                continue;
            }
            float distance = Vector3.Distance(c.transform.position, transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                nearestEnemy = c;
            }
        }
        return nearestEnemy;
    }

    public virtual void Attack()
    {
        if (currentBullet.attackType == AttackType.Melee)
        {
            MeleeAttack();
        }
        else
        {
            RangedAttack();
        }

        Invoke(nameof(DeactiveAttack), 0.5f);
    }

    public virtual void RangedAttack()
    {  
        Character target = SetTarget();
        if (target != null)
        {
            ChangeAnimatorParameter("IsAttack", true);

            Vector3 direction = target.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            transform.rotation = newRotation;

            Bullet bullet = SimplePool.Instance.Spawn(throwPoint.position, Quaternion.LookRotation(transform.forward), currentBullet.bulletType);
            bullet.OnInit();
            bullet.id = GetInstanceID();
            numBullet--;
        }
    }

    public virtual void MeleeAttack()
    {
        ChangeAnimatorParameter("IsAttack", true);

        Character target = SetTarget();
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            transform.rotation = newRotation;
        }
        numBullet--;
    }

    protected virtual void Dead()
    {
        ChangeAnimatorParameter("IsDead", true);
        isDead = true;
    }

    private void DeactiveAttack()
    {
        ChangeAnimatorParameter("IsAttack", false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponentInParent<Bullet>() != null && collider.GetComponentInParent<Bullet>().id != GetInstanceID())
        {
            if (!Physics.GetIgnoreCollision(GetComponentInChildren<SphereCollider>(), collider))
            {
                Physics.IgnoreCollision(GetComponentInChildren<SphereCollider>(), collider);
                return;
            }

            Dead();
            GameManager.Instance.LevelUp(collider.GetComponentInParent<Bullet>().id);
            collider.GetComponentInParent<Bullet>().OnDespawn();
        }
    }
}
