
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string type;
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
    [SerializeField] Dictionary<string, Queue<GameObject>> poolDictionary;

    private GameObject _objectPoolHolder;
    private static GameObject _Arrow;
    private static GameObject _CircleBullet;
    private static GameObject _GoldCoin;
    private static GameObject _FloatingText;
    public static Vector2 poolPosition;

    private void Awake()
    {
        _objectPoolHolder = new GameObject("Pooled Objects");
        _objectPoolHolder.transform.SetParent(transform);
        poolPosition = new Vector2(-5, 0);

        _Arrow = new GameObject("Arrows");
        _Arrow.transform.SetParent(_objectPoolHolder.transform);
        _CircleBullet = new GameObject("CircleBullets");
        _CircleBullet.transform.SetParent(_objectPoolHolder.transform);
        _GoldCoin = new GameObject("GoldCoin");
        _GoldCoin.transform.SetParent(_objectPoolHolder.transform);
        _FloatingText = new GameObject("FloatingText");
        _FloatingText.transform.SetParent(_objectPoolHolder.transform);
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        InitPoolDictionary();
    }

    private void InitPoolDictionary()
    {            
        foreach (Pool pool in pools)
        {
            InitPool(pool);
        }

    }

    private async void InitPool(Pool pool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
        int counter = 0;
        for (int i = 0; i < pool.size; i++, counter++)
        {
            if (this == null) return; // game over
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);

            obj.transform.SetParent(GetParent(obj));
            //Debug.Log("object num " + i + " with tag " + pool.type + " was created");
            if (counter > 20)
                await Task.Delay(200);
            else if (counter > 5)
                await Task.Delay(100);
        }

        poolDictionary.Add(pool.type, objectPool);
    }

    private GameObject InitMoreObject(GameObject gameObject)
    {
        GameObject obj = Instantiate(GetObjectPoolPrefab(gameObject));
        
        obj.SetActive(false);
        obj.transform.SetParent(GetParent(obj));
        return obj;
    }

    public GameObject GetObject(GameObject gameObject)
    {
        string type = GetType(gameObject);
        GameObject obj;
        if (poolDictionary[type].Count == 0)
        {
            obj = InitMoreObject(gameObject);
        }
        else
        {
            obj = poolDictionary[type].Dequeue();
        }

        obj.SetActive(true);
        return obj;
    }

    public GameObject GetObject(GameObject gameObject, Vector3 position)
    {
        string type = GetType(gameObject);
        GameObject obj;
        if (poolDictionary[type].Count == 0)
        {
            obj = InitMoreObject(gameObject);
        }
        else
        {
            obj = poolDictionary[type].Dequeue();
        }

        obj.SetActive(true);
        obj.transform.position = position;
        return obj;
    }

    public async void ReturnObject(GameObject gameObject, int time = 0)
    {
        if(time > 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            await Task.Delay(time);
            if (gameObject == null) return; // end game
            gameObject.GetComponent<Collider2D>().enabled = true;


            gameObject.transform.position = poolPosition;
            string type = GetType(gameObject);
            gameObject.SetActive(false);
            poolDictionary[type].Enqueue(gameObject);
        }
        else
        {
            gameObject.transform.position = poolPosition;
            string type = GetType(gameObject);
            gameObject.SetActive(false);
            poolDictionary[type].Enqueue(gameObject);
        }
    }

    private string GetType(GameObject gameObject)
    {
        if (gameObject.GetComponent<Arrow>()) return "Arrow";
        else if (gameObject.GetComponent<CircleBullet>()) return "CircleBullet";
        else if (gameObject.GetComponent<GoldCoin>()) return "GoldCoin";
        else if (gameObject.GetComponent<FloatingText>()) return "FloatingText";
        else return null;
    }

    private GameObject GetObjectPoolPrefab(GameObject gameObject)
    {
        if (gameObject.GetComponent<Arrow>()) return pools.Where(a => a.type == "Arrow").First().prefab;
        else if (gameObject.GetComponent<CircleBullet>()) return pools.Where(a => a.type == "CircleBullet").First().prefab;
        else if (gameObject.GetComponent<GoldCoin>()) return pools.Where(a => a.type == "GoldCoin").First().prefab;
        else if (gameObject.GetComponent<FloatingText>()) return pools.Where(a => a.type == "FloatingText").First().prefab;
        else return null;
    }

    private Transform GetParent(GameObject obj)
    {
        return GetType(obj) switch
        {
            "Arrow" => _Arrow.transform,
            "CircleBullet" => _CircleBullet.transform,
            "GoldCoin" => _GoldCoin.transform,
            "FloatingText" => _FloatingText.transform,
            _ => null,
        };
    }


    //public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    //{
    //    if (!poolDictionary.ContainsKey(tag))
    //    {
    //        Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
    //        return null;
    //    }

    //    GameObject objectToSpawn = poolDictionary[tag].Dequeue();
    //    objectToSpawn.transform.SetPositionAndRotation(position, rotation);
    //    objectToSpawn.SetActive(true);


    //    poolDictionary[tag].Enqueue(objectToSpawn);
    //    return objectToSpawn;
    //}
}
