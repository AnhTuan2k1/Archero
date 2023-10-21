using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] Renderer Renderer;

    private void Start()
    {
        StartCoroutine(ObjectVisibleInCamera());
    }

    private IEnumerator ObjectVisibleInCamera()
    {
        yield return new WaitForSeconds(2);
        while (!IsObjectVisible())
        {
            mainCamera.fieldOfView++;

            yield return new WaitForEndOfFrame();
        }
    }

    bool IsObjectVisible()
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.
                CalculateFrustumPlanes(mainCamera), Renderer.bounds);
    }
}
