using System;
using UnityEngine;

public class BallCircle : MonoBehaviour
{
    public static readonly float radius = 1.8f;
    [SerializeField] private GameObject fireCircleBallPrefab;
    [SerializeField] private GameObject poisonCircleBallPrefab;
    [SerializeField] private GameObject boltCircleBallPrefab;
    public Animator ani;

    [SerializeField] private int fireCircleAbility;
    public int FireCircle
    {
        get => fireCircleAbility * 2;
        set
        {
            if (fireCircleAbility == value) return;
            else fireCircleAbility = value;
            AddFireCircleBullet();

            AdjustBallCirclePosition();
        }
    }

    [SerializeField] private int poisonCircleAbility;
    public int PoisonCircle
    {
        get => poisonCircleAbility * 2;
        set
        {
            if (poisonCircleAbility == value) return;
            else poisonCircleAbility = value;
            AddPoisonCircleBullet();

            AdjustBallCirclePosition();
        }
    }

    [SerializeField] private int boltCircleAbility;
    public int BoltCircle
    {
        get => boltCircleAbility * 2;
        set
        {
            if (boltCircleAbility == value) return;
            else boltCircleAbility = value;
            AddBoltCircleBullet();

            AdjustBallCirclePosition();
        }
    }

    private void AdjustBallCirclePosition()
    {
        float angleUnit = Mathf.PI / transform.childCount;
        Vector3 playerPosition = Player.Instance.transform.position;

        for (int i = 0; i < transform.childCount; i += 2)
        {
            Transform child = transform.GetChild(i);
            float angle = angleUnit * i;

            child.position = playerPosition +
                new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
        }
        for (int i = 1; i < transform.childCount; i += 2)
        {
            Transform child = transform.GetChild(i);
            float angle = angleUnit * (i - 1);

            child.position = playerPosition +
                new Vector3(-radius * Mathf.Cos(angle), -radius * Mathf.Sin(angle), 0);
        }
    }

    private void AddPoisonCircleBullet()
    {
        int numAdded = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PoisonCircleBullet>())
                numAdded++;
        }

        if (numAdded < PoisonCircle)
        {
            while (numAdded < PoisonCircle)
            {
                GameObject g = Instantiate(poisonCircleBallPrefab);
                g.transform.SetParent(transform);
                numAdded++;
            }
        }
        else if (numAdded > PoisonCircle)
        {
            foreach (Transform child in transform)
            {
                PoisonCircleBullet g = child.GetComponent<PoisonCircleBullet>();
                if (g != null)
                {
                    numAdded--;
                    Destroy(g.gameObject);
                }
                if (numAdded == PoisonCircle) return;
            }
        }
    }

    private void AddBoltCircleBullet()
    {
        int numAdded = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BoltCircleBullet>())
                numAdded++;
        }

        if (numAdded < BoltCircle)
        {
            while (numAdded < BoltCircle)
            {
                GameObject g = Instantiate(boltCircleBallPrefab);
                g.transform.SetParent(transform);
                numAdded++;
            }
        }
        else if (numAdded > BoltCircle)
        {
            foreach (Transform child in transform)
            {
                BoltCircleBullet g = child.GetComponent<BoltCircleBullet>();
                if (g != null)
                {
                    numAdded--;
                    Destroy(g.gameObject);
                }
                if (numAdded == BoltCircle) return;
            }
        }
    }

    private void AddFireCircleBullet()
    {
        int numAdded = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<FireCircleBullet>())
                numAdded++;
        }

        if (numAdded < FireCircle)
        {
            while(numAdded < FireCircle)
            {
                GameObject g = Instantiate(fireCircleBallPrefab);
                g.transform.SetParent(transform);
                numAdded++;
            }
        }  
        else if (numAdded > FireCircle)
        {
            foreach (Transform child in transform)
            {
                FireCircleBullet g = child.GetComponent<FireCircleBullet>();
                if (g != null)
                {
                    numAdded--;
                    Destroy(g.gameObject);
                }
                if (numAdded == FireCircle) return;
            }
        }
    }
}
