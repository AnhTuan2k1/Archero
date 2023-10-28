
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private bool isMoving = false;

    public bool IsMoving
    {
        set
        {
            if(isMoving == false && value == true)
            {
                player.playerAttack.Targeted.ChangeTarget();
            }

            isMoving = value;
        }
        get => isMoving;
    }

    void Start()
    {
        player = GetComponent<Player>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        Vector3 direction = new Vector2(floatingJoystick.Direction.x, floatingJoystick.Direction.y);
        transform.position += player.Speed * Time.deltaTime * direction.normalized;

        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
            IsMoving = true;
        else
        {
            IsMoving = false;
            playerAttack.InvokeUnableReadyAttack();
        }
    }
}
