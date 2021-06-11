using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ScneneRestarter : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}