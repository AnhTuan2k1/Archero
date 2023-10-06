
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;
    private static GameObject _particleSystemEmpty;
    private static GameObject _gameObjectsEmpty;

    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }
    public static PoolType Poolingtype;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");
        _particleSystemEmpty = new GameObject("Particle Effects");
        _particleSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.Id == objectToSpawn.name);
        
        // If the pool doesn't exist, create it
        if(pool == null)
        {
            pool = new PooledObjectInfo()
            {
                Id = objectToSpawn.name
            };
            ObjectPools.Add(pool);
        }

        // check if there any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if(spawnableObj == null)
        {
            // Find the parent of the empty object
            GameObject parentObject = SetParentObject(poolType);
            
            // If there are no inactive objects, creae a new one
            spawnableObj = Instantiate(objectToSpawn, position, rotation);

            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        
        }
        else
        {
            // If there is an inacticve object reactive it
            spawnableObj.transform.position = position;
            spawnableObj.transform.rotation = rotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform transform)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.Id == objectToSpawn.name);

        // If the pool doesn't exist, create it
        if (pool == null)
        {
            pool = new PooledObjectInfo()
            {
                Id = objectToSpawn.name
            };
            ObjectPools.Add(pool);
        }

        // check if there any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            // If there are no inactive objects, creae a new one
            spawnableObj = Instantiate(objectToSpawn, transform);
        }
        else
        {
            // If there is an inacticve object reactive it
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInfo pool = ObjectPools.Find(p => p.Id == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
            new GameObject("ddd");
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemEmpty;

            case PoolType.GameObject:
                return _gameObjectsEmpty;

            case PoolType.None:
                return null;

            default:
                return null;
        }
    }
}


public class PooledObjectInfo
{
    public string Id;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}