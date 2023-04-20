using System;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocket = WebSocketSharp.WebSocket;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocket ws;
    public ObstacleSpawner spawner;

    // Start is called before the first frame update
    async Task Start()
    {
        //Connect to Websocket
        ws = new WebSocket("ws://116.203.41.47/");
        ws.Connect();
        //Retrieve data for pipes
        Task.Run(() =>
        {
            ws.OnMessage += (sender, e) =>
            {
                HandleRequest(e.Data);
            };
        });
    }

    public void sendObstacleDataToSpawner(float[] pipes)
    {
        //Set data in ObstacleSpawner
        spawner.setObstacleDataFromWebSocketHandler(pipes);
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

    public void HandleRequest(string request)
    {
        Metadata data = MetadataMapper.JsonToMetadata(request);
        switch (data.RequestType)
        {
            case RequestType.Pipes:
                var pipes = (data.Value as JObject)?["MapPipes"]!.ToObject<float[]>();
                sendObstacleDataToSpawner(pipes);
                break;
            case RequestType.Name:
                break;
            case RequestType.NameSet:
                break;
            case RequestType.JumpPlayer:
                break;
            case RequestType.JumpOther:
                break;
            case RequestType.DeathPlayer:
                break;
            case RequestType.DeathOther:
                break;
            case RequestType.AllPlayerData:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
