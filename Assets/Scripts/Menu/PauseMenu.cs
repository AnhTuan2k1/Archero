using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnGamePaused()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OnGamePaused();
    }

    public void OnGameResume()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameResume();
    }

    //public void OnGamePaused(bool paused)
    //{
    //    isPaused = paused;

    //    if (isPaused)
    //    {
    //        foreach (var obj in gameObjectsToPause)
    //        {
    //            var rigidbody = obj.GetComponent<Rigidbody>();
    //            if (rigidbody != null)
    //            {
    //                rigidbody.isKinematic = true;
    //            }

    //            var scriptComponents = obj.GetComponents<MonoBehaviour>();
    //            foreach (var script in scriptComponents)
    //            {
    //                script.enabled = false;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (var obj in gameObjectsToPause)
    //        {
    //            var rigidbody = obj.GetComponent<Rigidbody>();
    //            if (rigidbody != null)
    //            {
    //                rigidbody.isKinematic = false;
    //            }

    //            var scriptComponents = obj.GetComponents<MonoBehaviour>();
    //            foreach (var script in scriptComponents)
    //            {
    //                script.enabled = true;
    //            }
    //        }
    //    }
    //}
}