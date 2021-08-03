using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pong.Menu
{
    public class SceneHandler : MonoBehaviour
    {
        public void LoadSiglePlayerScene()
        {
            SceneManager.LoadScene(1); //load single player scene.
        }
    }
}
