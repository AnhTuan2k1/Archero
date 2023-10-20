
using UnityEngine;

public class Trap : MonoBehaviour
{
    private static readonly float TRAP_DAMAGE = 150;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagDefine.Tag_Player))
        {
            Player.Instance.
                TakeDamage(LevelManager.Instance.CurrentLevel*5+TRAP_DAMAGE);
        }
    }
}
