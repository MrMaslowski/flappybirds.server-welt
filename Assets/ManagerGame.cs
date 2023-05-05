using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    public Button restartButton;
    public GameObject Scoreboard;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        restartButton.gameObject.SetActive(false);
        WebSocketHandler.Send(new Metadata(RequestType.Pipes, WebSocketHandler.name, ""));
        Scoreboard.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //End game --> Stop time
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        WebSocketHandler.Send(new Metadata(RequestType.DeathPlayer, WebSocketHandler.name, ""));
        Time.timeScale = 0;
    }
}
