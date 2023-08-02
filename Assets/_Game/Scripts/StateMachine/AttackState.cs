using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;

    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        enemy.Attack();
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.enemyInRange.Count == 0)
        {
            enemy.ChangeState(new MoveState());
        }
        else
        {
            if (enemy.numBullet == 0)
            {
                enemy.RandomMove();
            }
            else
            {
                if (timer >= 3f)
                {
                    enemy.StopMove();
                    enemy.Attack();
                    timer = 0f;
                }
            }
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
