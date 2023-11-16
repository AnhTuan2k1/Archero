
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectPoolingType
{
    None,
    Arrow,
    RedCircleBullet,
    GoldCoin,
    FloatingText,
    LightningBolt,
    Laser,
    Bat,
    Enemy2001,
    FireBullet,
    BounceFireBullet,
    SuperBat,
    GrimReaper
}

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ObjectPoolingType type;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    private static ObjectPooling _instance;
    public static ObjectPooling Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ObjectPooling>();
            return _instance;
        }
    }
    #endregion

    [SerializeField] List<Pool> pools;
    [SerializeField] Dictionary<ObjectPoolingType, Queue<GameObject>> poolDictionary;

    [SerializeField] private GameObject _objectPoolHolder;
    [SerializeField] private GameObject _Arrow;
    [SerializeField] private GameObject _RedCircleBullet;
    [SerializeField] private GameObject _GoldCoin;
    [SerializeField] private GameObject _FloatingText;
    [SerializeField] private GameObject _LightningBolt;
    [SerializeField] private GameObject _Laser;
    [SerializeField] private GameObject _Bat;
    [SerializeField] private GameObject _Enemy2001;
    [SerializeField] private GameObject _FireBullet;
    [SerializeField] private GameObject _BounceFireBullet;
    [SerializeField] private GameObject _SupperBat;
    [SerializeField] private GameObject _GrimReaper;
    [SerializeField] private Vector2 poolPosition;

    private void Awake()
    {
        poolPosition = new Vector2(-5, 0);
    }

    private void Start()
    {
        poolDictionary = new Dictionary<ObjectPoolingType, Queue<GameObject>>();
        InitPoolDictionary();
    }

    private void InitPoolDictionary()
    {            
        foreach (Pool pool in pools)
        {
            InitPool(pool);
        }

    }

    private /*async*/ void InitPool(Pool pool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        int counter = 0;
        for (int i = 0; i < pool.size; i++, counter++)
        {
            if (this == null) return; // game over
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);

            obj.transform.SetParent(GetParent(pool.type));
            //Debug.Log("object num " + i + " with tag " + pool.type + " was created");
            //if (counter > 20)
            //    await Task.Delay(200);
            //else if (counter > 5)
            //    await Task.Delay(100);
        }

        poolDictionary.Add(pool.type, objectPool);
    }

    private GameObject InitMoreObject(ObjectPoolingType type)
    {
        GameObject obj = Instantiate(GetObjectPoolPrefab(type));
        
        obj.SetActive(false);
        obj.transform.SetParent(GetParent(type));
        return obj;
    }

    public GameObject GetObject(ObjectPoolingType type)
    {
        GameObject obj;
        if (poolDictionary[type].Count == 0)
        {
            //don't create more LightningBolt
            if (type == ObjectPoolingType.LightningBolt) return null;

            obj = InitMoreObject(type);
        }
        else
        {
            obj = poolDictionary[type].Dequeue();
        }

        obj.SetActive(true);
        return obj;
    }

    public GameObject GetObject(ObjectPoolingType type, Vector3 position)
    {
        GameObject obj;
        if (poolDictionary[type].Count == 0)
        {
            //don't create more LightningBolt
            if (type == ObjectPoolingType.LightningBolt) return null;

            obj = InitMoreObject(type);
        }
        else
        {
            obj = poolDictionary[type].Dequeue();
        }

        obj.SetActive(true);
        obj.transform.position = position;
        return obj;
    }

    public void ReturnObject(GameObject gameObject,
        ObjectPoolingType type = ObjectPoolingType.None)
    {
        gameObject.transform.position = poolPosition;
        if (type == ObjectPoolingType.None) type = GetType(gameObject);
        gameObject.SetActive(false);
        poolDictionary[type].Enqueue(gameObject);
    }

    private ObjectPoolingType GetType(GameObject gameObject)
    {
        if (gameObject.GetComponent<Arrow>()) return ObjectPoolingType.Arrow;
        else if (gameObject.GetComponent<RedCircleBullet>()) return ObjectPoolingType.RedCircleBullet;
        else if (gameObject.GetComponent<GoldCoin>()) return ObjectPoolingType.GoldCoin;
        else if (gameObject.GetComponent<FloatingText>()) return ObjectPoolingType.FloatingText;
        else if (gameObject.GetComponent<LightningBolt>()) return ObjectPoolingType.LightningBolt;
        else if (gameObject.GetComponent<Laser>()) return ObjectPoolingType.Laser;
        else if (gameObject.GetComponent<Bat>()) return ObjectPoolingType.Bat;
        else if (gameObject.GetComponent<Enemy2001>()) return ObjectPoolingType.Enemy2001;
        else if (gameObject.GetComponent<FireBullet>()) return ObjectPoolingType.FireBullet;
        else if (gameObject.GetComponent<BounceFireBullet>()) return ObjectPoolingType.BounceFireBullet;
        else if (gameObject.GetComponent<SuperBat>()) return ObjectPoolingType.SuperBat;
        else if (gameObject.GetComponent<GrimReaper>()) return ObjectPoolingType.GrimReaper;

        else throw new System.Exception("can't determinate gameobject type");
    }

    private GameObject GetObjectPoolPrefab(ObjectPoolingType type)
    {
        return pools.Where(a => a.type == type).First().prefab;
    }

    private Transform GetParent(ObjectPoolingType type)
    {
        return type switch
        {
            ObjectPoolingType.Arrow => _Arrow.transform,
            ObjectPoolingType.RedCircleBullet => _RedCircleBullet.transform,
            ObjectPoolingType.GoldCoin => _GoldCoin.transform,
            ObjectPoolingType.FloatingText => _FloatingText.transform,
            ObjectPoolingType.LightningBolt => _LightningBolt.transform,
            ObjectPoolingType.Laser => _Laser.transform,
            ObjectPoolingType.Bat => _Bat.transform,
            ObjectPoolingType.Enemy2001 => _Enemy2001.transform,
            ObjectPoolingType.FireBullet => _FireBullet.transform,
            ObjectPoolingType.BounceFireBullet => _BounceFireBullet.transform,
            ObjectPoolingType.SuperBat => _SupperBat.transform,
            ObjectPoolingType.GrimReaper => _GrimReaper.transform,
            _ => null,
        };
    }
}
