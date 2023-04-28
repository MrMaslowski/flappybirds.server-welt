using System;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json.Linq;
using WebSocket = WebSocketSharp.WebSocket;
using static MetadataMapper;
using System.Collections.Generic;
using WebSocketSharp;

public class WebSocketHandler : MonoBehaviour
{
    public static WebSocket webSocket;
    public static PlayerSpawn ps;
    public static ObstacleSpawner os;
    public static String name;
    public static int Score = 0;

    public static void Connect()
    {
        // Set up WebSocket connection
        webSocket = new WebSocket("ws://116.203.41.47/");
        //Action for OpenedConnection
        webSocket.OnOpen += OnWebSocketOpen;
        //Action for ReceivedMessage
        webSocket.OnMessage += OnWebSocketMessage;
        //Action for Error
        webSocket.OnError += OnWebSocketError;
        //Action for ClosedConnection
        webSocket.OnClose += OnWebSocketClose;

        // Connect to the WebSocket server
        webSocket.Connect();
    }

    public static void OnWebSocketOpen(object sender, System.EventArgs e)
    {
        //Action on Open
        Debug.Log("WebSocket connection opened.");
    }

    public static void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        //Handle the received Data
        HandleRequest(e.Data);
    }

    public static void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        //Action on Error
        Debug.LogError("WebSocket error: " + e.Message);
    }

    public static void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        //Action on Close
        Debug.Log("WebSocket connection closed with code " + e.Code + " and reason '" + e.Reason + "'.");
    }

    public static void Send(Metadata metadata)
    {
        //Send a Metadata Object to the Server
        webSocket.Send(EncodeJson(MetadataToJson(metadata)));
    }

    public static void HandleRequest(string response)
    {
        //Map the responseData to a Metadata Object containing Type, From and Values
        Metadata data = MetadataMapper.JsonToMetadata(response);
        //Perform different Action based on RequestType
        switch (data.RequestType)
        {
            case RequestType.Pipes:
                //We Have received Pipe information
                //Map the values to a float[]
                var pipes = (data.Value as JObject)?.ToObject<Pipes>();
                //Set Data in ObstactleSpawner
                os.setObstacleDataFromWebSocketHandler(pipes.MapPipes.Select(d => (float)d).ToArray());
                Send(new Metadata(RequestType.AllPlayerData, "",""));
                break;
            case RequestType.Name:
                //Server Requests the Users Name
                Send(new Metadata(RequestType.Name, name, name));
                break;
            case RequestType.NameSet:
                //The Name has been accepted, so we can save it
                name = (string)data.Value;
                break;
            case RequestType.JumpPlayer:
                Debug.Log("JumpPlayer: " + data.Value);
                break;
            case RequestType.JumpOther:
                Debug.Log("JumpOther: " + data.Value);
                // Bekommt spielernamen, dieses Spielerobjekt suchen und jumpfunktion ausf�hren
                break;
            case RequestType.DeathPlayer:
                Debug.Log("DeathPlayer: " + data.Value);
                break;
            case RequestType.DeathOther:
                var playername = data.Value as string;
                ps.deletePlayer(playername);
                break;
            case RequestType.AllPlayerData:
                List<Player> playerlist = (data.Value as JArray)
                    .ToObject<JToken[]>()
                    .Select(j => j.ToObject<Player>())
                    .ToList();;
                ps.spawnPlayer(playerlist);
                break;
            case RequestType.Score:
                break;
            case RequestType.Highscore:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
