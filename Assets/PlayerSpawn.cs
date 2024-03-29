using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject bird;
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    public static float startTime;
    // A variable that stores the name of the remote player
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketHandler.ps = this;
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteAll()
    {
        players.Clear();
    }
    public  GameObject getOnlinePlayer(string playername) 
    { 
        return players[playername]; 
    }
    public bool isPlayerOnline(string playername)
    {
        return players.ContainsKey(playername);
    }

    // receives Player data and calculates if and where other Players are in the frame
    // playerlist: player in 8 sec radius, 
    public void spawnPlayer(List<Player> playerlist)
    {
        // pr�fen welche neu sind -> hinzuf�gen
        if(playerlist == null || playerlist.Count < 1)
        {
            return;
        }
        foreach (Player player in playerlist)
        {
            if (player != null)
            {
                // find Player in Array
                if(!players.ContainsKey(player.Name))
                {
                    // spawn Player
                    GameObject newplayer = Instantiate(bird);
                    Debug.Log("SPAWN: " +  player.Name);
                    // Also set the name of the bird
                    text.text = player.Name;

                    // calculate played time since restart
                    float elapsedTime = Time.realtimeSinceStartup - startTime;

                    // x-Value = Played time * speed => OnPlTime - Mytime
                    float x_value = -(float)player.Playtime.Subtract(DateTime.Now.AddSeconds(-elapsedTime)).TotalSeconds;

                    // Bird spawns at height from data/server
                    newplayer.transform.position = new Vector3(x_value, ToFloat(player.Height), 0);
                    // change opacity from other Birds
                    var trans = 0.5f;
                    var col = newplayer.GetComponent<Renderer>().material.color;
                    col.a = trans;
                    newplayer.GetComponent<Renderer>() .material.color = col;
                    // addnewplayer to local list
                    players.Add(player.Name, newplayer);
                }
            }
        }
    }
    public void deletePlayer(string playername)
    {
        // pr�fen welche Tod sind -> despawnen
        if (players.ContainsKey(playername))
        {
            //despawn Player
            Destroy(players[playername]);
            //delete from List
            players.Remove(playername);
        }
    }
    private static float ToFloat(double value)
    {
        return (float)value;
    }
    // DateTime in Seconds
    public static double ConvertToUnixTimestamp(DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }
}
