using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;
using static MetadataMapper;

public class WebSocketHandler : MonoBehaviour
{
    public static WebSocket webSocket;
    public static PlayerSpawn ps;
    public static ObstacleSpawner os;
    public static String name;

    public static void Connect()
    {
        // Set up WebSocket connection
        webSocket = new WebSocket("ws://116.203.41.47/");
        webSocket.OnOpen += OnWebSocketOpen;
        webSocket.OnMessage += OnWebSocketMessage;
        webSocket.OnError += OnWebSocketError;
        webSocket.OnClose += OnWebSocketClose;

        // Connect to the WebSocket server
        webSocket.Connect();
    }

    public static void OnWebSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log("WebSocket connection opened.");
    }

    public static void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        HandleRequest(e.Data);
    }

    public static void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message);
    }

    public static void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed with code " + e.Code + " and reason '" + e.Reason + "'.");
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void Send(Metadata metadata)
    {
        webSocket.Send(EncodeJson(MetadataToJson(metadata)));
    }

    public static void HandleRequest(string response)
    {
        //Map the responseData to a Metadata  Object containing Type, From and Values
        Metadata data = MetadataMapper.JsonToMetadata(response);
        Debug.Log("Message received type: " + data.RequestType);
        //Perform different Action based on RequestType
        switch (data.RequestType)
        {
            case RequestType.Pipes:
                //We Have received Pipe information
                //Map the values to a float[]
                var pipes = (data.Value as JObject)?.ToObject<Pipes>();
                //Set Data in ObstactleSpawner
                os.setObstacleDataFromWebSocketHandler(pipes.MapPipes.Select(d => (float)d).ToArray());
                break;
            case RequestType.Name:
                Send(new Metadata(RequestType.Name, name, name));
                break;
            case RequestType.NameSet:
                name = (string)data.Value;
                break;
            case RequestType.JumpPlayer:
                Debug.Log("JumpPlayer: " + data.Value);
                break;
            case RequestType.JumpOther:
                Debug.Log("JumpOther: " + data.Value);
                break;
            case RequestType.DeathPlayer:
                Debug.Log("DeathPlayer: " + data.Value);
                break;
            case RequestType.DeathOther:
                Debug.Log("DeathOther: " + data.Value);
                break;
            case RequestType.AllPlayerData:
                Debug.Log("AllPayerData: " + data.Value);
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
