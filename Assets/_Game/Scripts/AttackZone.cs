using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    Character parent;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (other.GetInstanceID() != parent.GetInstanceID())
            {
                parent.AddEnemy(other.GetComponent<Character>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (other.GetInstanceID() != parent.GetInstanceID())
            {
                parent.RemoveEnemy(other.GetComponent<Character>());
            }
        }
    }
}
