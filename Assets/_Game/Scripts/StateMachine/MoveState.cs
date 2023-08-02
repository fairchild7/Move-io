using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    float timer;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        enemy.RandomMove();
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.enemyInRange.Count > 0)
        {
            enemy.ChangeState(new AttackState());
        }
        else
        {
            if (timer >= 5f)
            {
                enemy.RandomMove();
                timer = 0;
            }
            else
            {
                if (enemy.GetDistance(enemy.GetDestination()) > 0.1f)
                {
                    return;
                }
                else
                {
                    enemy.StopMove();
                    enemy.RandomMove();
                    timer = 0;
                }
            }
        }
    }

    public void OnExit(Enemy enemy)
    {
        enemy.ChangeAnimatorParameter("IsIdle", true);
    }
}
