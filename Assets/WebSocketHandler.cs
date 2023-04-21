using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocket ws;
    private PlayerSpawn ps;
    public ObstacleSpawner spawner;

    // Start is called before the first frame update
    async Task Start()
    {
        //Connect to Websocket
        ws = new WebSocket("ws://116.203.41.47/");
        ws.Connect();
        ////Retrieve all data
        //ws.Send(ws.OnMessage += (sender, e) =>
        //{
        //    sendObstacleDataToSpawner(e.Data);
        //});
        //Retrieve data for pipes
        ws.OnMessage += (sender, e) =>
        {
            sendObstacleDataToSpawner(e.Data);
        };
    }

    public void sendObstacleDataToSpawner(string response)
    {
        //Parse pipes data from server
        string[] string_values = response.Substring(response.IndexOf('[') + 1).Split(']')[0].Split(',');
        float[] values = new float[100];
        int counter = 0;
        foreach(string value in string_values)
        {
            //Write to console if parsing failed
            if(!float.TryParse(value, out values[counter]))
                Debug.Log("Parsing obstacle data from string to float failed! Data: " + value);
            counter++;
        }
        //Set data in ObstacleSpawner
        spawner.setObstacleDataFromWebSocketHandler(values);
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            return;
        }
        //If space is pressed, send ozan a nice comment about his sexy body
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hallo Ozan du geile Sau!");
        }
    }
    void Datahandler(string response)
    {
        // response string in gewünschte Variablen parsen
        // player spawnen
        
    }
}
