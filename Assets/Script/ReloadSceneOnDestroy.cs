using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        var scene = SceneManager.GetActiveScene();
        var bi = scene.buildIndex;
        var temp = SceneManager.CreateScene("temp");
        SceneManager.SetActiveScene(temp);
        SceneManager.UnloadSceneAsync(scene).completed += _ => SceneManager.LoadScene(bi);
    }
}
