using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Start()
    {
        player = Player.Instance.transform;
        mainCamera = Camera.main;
        //minY = Terrain.Instance.LimitedBottom.transform.position.y;
        //maxY = Terrain.Instance.LimitedTop.transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 playerPosition = player.position;
        Vector3 cameraPosition = mainCamera.transform.position;

        // Gi?i h?n camera theo tr?c y
        float clampedY = Mathf.Clamp(playerPosition.y, minY, maxY);

        // C?p nh?t v? trí c?a camera
        cameraPosition.y = clampedY;
        mainCamera.transform.position = cameraPosition;
    }
}
