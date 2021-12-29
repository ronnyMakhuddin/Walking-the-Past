using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    private AsyncOperation sceneAsync;

    public void GoToScene(string sceneName, List<GameObject> objectsToCarryOver)
    {
        StartCoroutine(LoadScene(sceneName, objectsToCarryOver));
    }

    private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToCarryOver)
    {
        SceneManager.LoadSceneAsync(sceneName);
        SceneManager.sceneLoaded += (newScene, mode) =>
        {
            SceneManager.SetActiveScene(newScene);
        };

        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
        if (objectsToCarryOver != null)
        {
            foreach (GameObject obj in objectsToCarryOver)
            {
                SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
            }
        }
        yield return null;
    }
}
