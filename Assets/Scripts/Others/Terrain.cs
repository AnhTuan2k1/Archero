using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public GameObject LimitedLeft;
    public GameObject LimitedTop;
    public GameObject LimitedRight;
    public GameObject LimitedBottom;

    private static Terrain _instance;
    public static Terrain Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Terrain>();
            return _instance;
        }
    }
}
