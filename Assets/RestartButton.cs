using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RestartButton : MonoBehaviour
    {
        public void OnRestartButtonClicked()
        {
            WebSocketHandler.Send(new Metadata(RequestType.Restart, WebSocketHandler.name, ""));
            WebSocketHandler.Score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerSpawn.startTime = Time.realtimeSinceStartup;
        }
    }
}