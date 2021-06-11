using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class ScneneRestarter : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}