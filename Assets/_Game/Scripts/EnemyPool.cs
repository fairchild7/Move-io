using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    [SerializeField] Enemy enemyPrefab;

    public List<Enemy> poolEnemy;

    public Enemy Spawn(Vector3 pos, Quaternion rot)
    {
        Enemy enemy = Spawn();
        enemy.transform.SetPositionAndRotation(pos, rot);
        return enemy;
    }

    public Enemy Spawn()
    {
        Enemy enemy;

        if (poolEnemy.Count == 0)
        {
            enemy = Instantiate(enemyPrefab);
            poolEnemy.Add(enemy);
        }

        enemy = poolEnemy[0];
        enemy.gameObject.SetActive(true);
        //enemy.currentBullet = new Boomerang(); //should random this
        enemy.OnInit();
        poolEnemy.RemoveAt(0);
        GameManager.Instance.AddEnemyToList(enemy);
        return enemy;
    }

    public void Despawn(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.gameObject.SetActive(false);
            poolEnemy.Add(enemy);
            GameManager.Instance.RemoveEnemyFromList(enemy);
        }
    }
}
