using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : Singleton<SimplePool>
{
    public List<Bullet> bulletPool = new List<Bullet>();
    Dictionary<Type, List<Bullet>> pools = new Dictionary<Type, List<Bullet>>();

    public Bullet Spawn(Vector3 pos, Quaternion rot, Type type)
    {
        Bullet obj = Spawn(type);
        obj.transform.SetPositionAndRotation(pos, rot);
        return obj;
    }

    public Bullet Spawn(Type type)
    {
        Bullet obj;

        if (!pools.ContainsKey(type))
        {
            pools.Add(type, new List<Bullet>());
        }

        if (pools[type].Count == 0)
        {
            obj = Instantiate(bulletPool.Find(x => x.bulletType == type));
            pools[type].Add(obj);
            
        }
        obj = pools[type][0];
        obj.gameObject.SetActive(true);
        pools[type].RemoveAt(0);
        return obj;
    }

    public void Despawn(Bullet obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            pools[obj.bulletType].Add(obj);
        }
    }
}
