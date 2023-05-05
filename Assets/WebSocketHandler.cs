using System;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NativeWebSocket;
using static MetadataMapper;
using System.Collections.Generic;
using TMPro;

public class WebSocketHandler : MonoBehaviour
{
    public static WebSocket webSocket;
    public static PlayerSpawn ps;
    public static ObstacleSpawner os;
    public static ScoreBoard sb;
    public static String name;
    public static int Score = 0;

    // Start is called before the first frame update
    async void Start()
    {
        webSocket = new WebSocket("ws://116.203.41.47:5000");

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
                Send(new Metadata(RequestType.AllPlayerData, "","")); // was macht des?
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
            //case RequestType.JumpOther:
            //    Debug.Log("JumpOther: " + data.Value);
            //    // Bekommt spielernamen, dieses Spielerobjekt suchen und jumpfunktion ausf�hren
            //    // Muss ich anfrage senden oder kommt des einfach so?
            //    var playername = data.Value as string;
            //    var player = ps.getOnlinePlayer(playername);
            //    OnlinePlayer_Movement opm = player.GetComponent<OnlinePlayer_Movement>();
            //    opm.Jump();
            //    break;
            case RequestType.JumpOther: // wird jeden Frame ausgeführt
                Debug.Log("JumpOther: " + data.Value);
                var playerHeight = (float)data.Value;
                var playerName = ps.getOnlinePlayer(data.From);
                OnlinePlayer_Movement opm = playerName.GetComponent<OnlinePlayer_Movement>();
                opm.transform.position = new Vector3(opm.transform.position.x, playerHeight, 0);
                break;
            case RequestType.DeathOther:
                var playername = data.Value as string;
                ps.deletePlayer(playername);
                break;
            case RequestType.AllPlayerData:
                List<Player> playerlist = (data.Value as JArray)
                    .ToObject<JToken[]>()
                    .Select(j => j.ToObject<Player>())
                    .ToList();
                Debug.Log("AllPlayerData: " + playerlist);
                ps.spawnPlayer(playerlist);
                break;
            case RequestType.Score:
                break;
            case RequestType.Highscore:
                Debug.Log("HighScore [" + data.From +  "]: " + data.Value);


                string lol = data.Value.ToString();
                sb.setHighScoreData(int.Parse(lol), data.From);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
