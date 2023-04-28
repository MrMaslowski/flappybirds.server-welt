using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    //private float timer = 0;
    public GameObject bird;
    private Dictionary<string, GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketHandler.ps = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // receives Player data and calculates if and where other Players are in the frame
    // playerlist: player in 8 sec radius, 
    public void spawnPlayer(List<Player> playerlist)
    {

        Debug.Log("Test5");
        // pr�fen welche neu sind -> hinzuf�gen
        if(playerlist == null ||playerlist.Count < 1)
        {
            Debug.Log("Test55");
            return;
        }
        foreach (Player player in playerlist)
        {
            Debug.Log("Test4");
            if (player != null)
            {
                // find Player in Array
                if(!players.ContainsKey(player.Name))
                {
                    // spawn Player

                    GameObject newplayer = Instantiate(bird);

                    // x-Value = Played time * speed 
                    //var played_time = (player.Playtime - player.Playtime).TotalSeconds; // falsch
                    //float x_value = ((float)(played_time) - Time.realtimeSinceStartup);
                    float x_value = -1; // Test

                    // Bird spawns at height from data/server
                    newplayer.transform.position = new Vector3(x_value, ToFloat(player.Height), 0);
                    // change opacity from other Birds
                    var trans = 0.5f;
                    var col = newplayer.GetComponent<Renderer>().material.color;
                    col.a = trans;
                    newplayer.GetComponent<Renderer>() .material.color = col;
                    // addnewplayer to local list
                    players.Add(player.Name, newplayer);
                    Debug.Log("Test5");
                }
            }
        }
        Debug.Log("Test7");
    }
    public void deletePlayer(string player)
    {
        // pr�fen welche Tod sind -> despawnen
        if (players.ContainsKey(player))
        {
            //despawn Player
            //delete from List
            players.Remove(player);
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
