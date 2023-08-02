using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : Singleton<UIGamePlay>
{
    public void SetParentToThis(Enemy enemy)
    {
        enemy.arrowPrefab.SetParent(this.transform);
    }

    public void CheckNavigation(Enemy enemy)
    {
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(enemy.transform.position);

        if (viewPosition.x >= 0 && viewPosition.x <= 1 && viewPosition.y >= 0 && viewPosition.y <= 1)
        {
            enemy.arrowPrefab.gameObject.SetActive(false);
        }
        else
        {
            enemy.arrowPrefab.gameObject.SetActive(true);
            DrawNavigation(enemy);
        }
        //Camera.main.WorldToScreenPoint; world to screen camera
        //RectTransformUtility.WorldToScreenPoint; -> world to screen canvas
    }

    public void DrawNavigation(Enemy enemy)
    {
        Vector3 botPos = RectTransformUtility.WorldToScreenPoint(Camera.main, enemy.transform.position);
        Vector3 playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, GameManager.Instance.player.transform.position);
        Vector3 dir = botPos - playerPos;
   
        Vector3 arrowPos = new Vector3(Mathf.Clamp(dir.x, -900, 900f), Mathf.Clamp(dir.y, -500f, 500f), dir.z);
        enemy.arrowPrefab.transform.localPosition = arrowPos;
        enemy.arrowPrefab.eulerAngles = new Vector3(0f, 0f, GetAngle(enemy.transform));
    }

    private float GetAngle(Transform target)
    {
        Vector3 dir = target.position - GameManager.Instance.player.transform.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        return angle;
    }
}
