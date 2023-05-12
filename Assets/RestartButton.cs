using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RestartButton : MonoBehaviour
    {
        public void OnRestartButtonClicked()
        {
            // Clear other Player data
            WebSocketHandler.clearPlayers();
            // inform server
            WebSocketHandler.Send(new Metadata(RequestType.Restart, WebSocketHandler.name, ""));
            WebSocketHandler.Score = 0;
            // Load to restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // reset the starttime
            PlayerSpawn.startTime = Time.realtimeSinceStartup;
        }
    }
}