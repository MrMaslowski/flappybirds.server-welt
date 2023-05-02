using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RestartButton : MonoBehaviour
    {
        public void OnRestartButtonClicked()
        {
            WebSocketHandler.Send(new Metadata(RequestType.Restart, WebSocketHandler.name, ""));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}