using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NativeWebSocket;
using static MetadataMapper;

public class WebSocketHandler : MonoBehaviour
{
    public static WebSocket webSocket;
    public static PlayerSpawn ps;
    public static ObstacleSpawner os;
    public static String name;
    public static int Score = 0;

    // Start is called before the first frame update
    async void Start()
    {
        webSocket = new WebSocket("ws://116.203.41.47");

        webSocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        webSocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        webSocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        webSocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            HandleRequest(message);
        };

        // waiting for messages
        await webSocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            webSocket.DispatchMessageQueue();
        #endif
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    

    private async void OnApplicationQuit()
    {
        await webSocket.Close();
    }


    public static async void Send(Metadata metadata)
    {
        //Send a Metadata Object to the Server
        await webSocket.Send(EncodeJson(MetadataToJson(metadata)));
    }

    public static void HandleRequest(string response)
    {
        //Map the responseData to a Metadata  Object containing Type, From and Values
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
                break;
            case RequestType.DeathPlayer:
                Debug.Log("DeathPlayer: " + data.Value);
                break;
            case RequestType.DeathOther:
                Debug.Log("DeathOther: " + data.Value);
                break;
            case RequestType.AllPlayerData:
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
