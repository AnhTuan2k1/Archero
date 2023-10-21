using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public Rigidbody2D rb;
    public async void Clean(Vector2 origin)
    {
        rb.velocity = Vector2.down*15;

        await Task.Delay(1000);
        if (this == null) return;

        transform.position = origin + Vector2.up;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagDefine.Tag_Bullet))
        {
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            if (b.BulletType != ObjectPoolingType.None) b.Die();
        }
    }
}


// player die
// animation ability
// show attribute player
// boss bullet