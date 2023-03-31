using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

public class WebSocketHandler : MonoBehaviour
{
    WebSocket ws;
    Spawner spawner = new Spawner();
    // Start is called before the first frame update
    async Task Start()
    {
        ws = new WebSocket("ws://116.203.41.47/");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            sendObstacleDataToSpawner(e.Data);
        };
    }

    public void sendObstacleDataToSpawner(string response)
    {
        Debug.Log(response);
        spawner.getObstacleDataFromWebSocketHandler(Array.ConvertAll(response.replace('[','').replace(']','').Message.Split(new[] { ',', }, StringSplitOptions.RemoveEmptyEntries), Double.Parse));
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hallo Ozan du geile Sau!");
        }
    }
}
