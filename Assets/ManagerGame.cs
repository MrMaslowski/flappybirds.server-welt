using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
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
