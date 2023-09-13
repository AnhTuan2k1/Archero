using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerAttack playerAttack;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private bool isMoving = false;
    public bool IsMoving => this.isMoving;

    void Start()
    {
        player = GetComponent<Player>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        Vector3 direction = new Vector2(floatingJoystick.Direction.x, floatingJoystick.Direction.y);
        transform.position += direction.normalized * player.Speed * Time.deltaTime;

        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
            isMoving = true;
        else
        {
            isMoving = false;
            playerAttack.InvokeUnableReadyAttack();
        }
    }
}
