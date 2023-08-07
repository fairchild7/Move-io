using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    [SerializeField] LayerMask wallLayer;

    private int enemyCount;

    private List<GameObject> wallList = new List<GameObject>();

    public List<Enemy> listEnemy = new List<Enemy>(); 

    private void Awake()
    {
        enemyCount = 0;
    }

    private void Update()
    {
        foreach (Enemy e in listEnemy)
        {
            if (e.currentState != null)
            {
                e.currentState.OnExecute(e);
            }
            UIGamePlay.Instance.CheckNavigation(e);
        }

        CheckWall();
    }

    public void PauseGame()
    {
        foreach (Enemy e in listEnemy)
        {
            e.agent.isStopped = true;
        }
    }

    public void ResumeGame()
    {
        foreach (Enemy e in listEnemy)
        {
            e.agent.isStopped = false;
            e.ChangeState(new MoveState());
        }
    }

    public void AddEnemyToList(Enemy enemy)
    {
        listEnemy.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        listEnemy.Remove(enemy);
    }

    public void LevelUp(int characterId)
    {
        if (player.GetInstanceID() == characterId)
        {
            IncreaseSize(player);
        }
        else
        {
            foreach (Enemy e in listEnemy)
            {
                if (e.GetInstanceID() == characterId)
                {
                    IncreaseSize(e);
                }
            }
        }
    }

    private void IncreaseSize(Character character)
    {
        character.level++;
        Vector3 currentSize = character.transform.localScale;
        currentSize = new Vector3(1f, 1f, 1f) * (1 + 0.1f * character.level);
        character.transform.localScale = currentSize;
        player.throwPoint.position = new Vector3(player.throwPoint.position.x, 1.2f, player.throwPoint.position.z);
    }

    public void StartGame()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (enemyCount < 50)
        {
            if (listEnemy.Count < 2)
            {
                EnemyPool.Instance.Spawn(new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f)), Quaternion.identity);
                enemyCount++;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void CheckWall()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - Camera.main.transform.position;
        Vector3 origin = Camera.main.transform.position - direction;

        if (Physics.Raycast(origin, direction, out hit, 500f, wallLayer))
        {
            if (!wallList.Contains(hit.collider.gameObject))
            {
                wallList.Add(hit.collider.gameObject);
            }

            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        }
        else
        {
            foreach (GameObject wall in wallList)
            {
                wall.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }
}
