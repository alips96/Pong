using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadSiglePlayerScene()
    {
        SceneManager.LoadScene(1); //load single player scene.
    }
}
