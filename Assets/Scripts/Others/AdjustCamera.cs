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
        Screen.orientation = ScreenOrientation.Portrait;
        StartCoroutine(ObjectVisibleInCamera());
    }

    private IEnumerator ObjectVisibleInCamera()
    {
        yield return new WaitForSeconds(2);

        if (IsObjectVisible())
        {
            mainCamera.fieldOfView--;
            yield return new WaitForEndOfFrame();

            while (IsObjectVisible())
            {
                mainCamera.fieldOfView--;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            mainCamera.fieldOfView++;
            yield return new WaitForEndOfFrame();

            while (!IsObjectVisible())
            {
                mainCamera.fieldOfView++;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    bool IsObjectVisible()
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.
                CalculateFrustumPlanes(mainCamera), Renderer.bounds);
    }
}
