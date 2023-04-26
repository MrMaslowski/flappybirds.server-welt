using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public ObstacleSpawner os;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartSocket();
        WebSocketHandler.os = os;
        // Use the webSocketController as needed
        if (WebSocketHandler.webSocket == null)
        {
            WebSocketHandler.Connect();
        }
    }

    private void StartSocket()
    {
        // Use the webSocketController as needed
        if (WebSocketHandler.webSocket == null)
        { 
            WebSocketHandler.Connect();
        }
        WebSocketHandler.Send(new Metadata(RequestType.Pipes, WebSocketHandler.name, ""));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //End game --> Stop time
    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
