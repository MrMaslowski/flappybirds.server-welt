using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocket ws;
    public ObstacleSpawner spawner;

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
        string[] string_values = response.Substring(response.IndexOf('[') + 1).Split(']')[0].Split(',');
        float[] values = new float[100];
        int counter = 0;
        foreach(string value in string_values)
        {
            if(!float.TryParse(value, out values[counter]))
                Debug.Log("Parsing obstacle data from string to float failed! Data: " + value);
            counter++;
        }
        spawner.setObstacleDataFromWebSocketHandler(values);
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
