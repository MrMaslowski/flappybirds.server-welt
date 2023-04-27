using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private float timer = 0;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // receives Player data and calculates if and where other Players are in the frame
    private void spawnPlayer(List<Player> playerlist)
    {
        foreach(Player player in playerlist)
        {
            if (player != null)
            {
                //GameObject newplayer = Instantiate(player);
                //float x_value = (player.getPlaytime() - Time.realtimeSinceStartup) * 1;
                ////Pipe spawns at height from data/server
                //newplayer.transform.position = new Vector3(x_value, player.getHeight()f, 0);
                ////Destroy player after every frame
                //Destroy(newpipe, /*??*/);
                
            }
        }
    }
}
