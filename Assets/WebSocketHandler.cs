using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocket = WebSocketSharp.WebSocket;
using static MetadataMapper;
using System.Collections.Generic;

public class WebSocketHandler : MonoBehaviour
{
    private WebSocket ws;
    public PlayerSpawn ps;
    public ObstacleSpawner spawner;
    private String name;

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
        Task.Run(() =>
        {
            ws.OnMessage += (sender, e) =>
            {
                HandleRequest(e.Data);
            };
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null)
        {
            return;
        }
    }

    public void Send(Metadata metadata)
    {
        ws.Send(EncodeJson(MetadataToJson(metadata)));
    }

    public void HandleRequest(string response)
    {
        //Map the responseData to a Metadata Object containing Type, From and Values
        Metadata data = MetadataMapper.JsonToMetadata(response);
        //Perform different Action based on RequestType
        switch (data.RequestType)
        {
            case RequestType.Pipes:
                //We Have received Pipe information
                //Map the values to a float[]
                var pipes = (data.Value as JObject).ToObject<Pipes>();
                //Set Data in ObstactleSpawner
                spawner.setObstacleDataFromWebSocketHandler(pipes.MapPipes.Select(d => (float)d).ToArray());
                Send(new Metadata(RequestType.AllPlayerData, "string", "string"));
                break;
            case RequestType.Name:
                break;
            case RequestType.NameSet:
                break;
            case RequestType.JumpPlayer:
                break;
            case RequestType.JumpOther:
                // Bekommt spielernamen, dieses Spielerobjekt suchen und jumpfunktion ausführen
                break;
            case RequestType.DeathPlayer:
                break;
            case RequestType.DeathOther:
                var playername = data.Value as string;
                ps.deletePlayer(playername);
                break;
            case RequestType.AllPlayerData:
                List<Player> playerlist = data.Value as List<Player>;
                Debug.Log("Playerlist: " + data.Value);
                ps.spawnPlayer(playerlist);
                Debug.Log("Test444");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
