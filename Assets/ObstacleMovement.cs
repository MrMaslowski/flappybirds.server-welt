using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleMovement : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private bool passed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Obstacles move from right to left. You can set their speed in Unity!
        transform.position += Vector3.left * speed * Time.deltaTime;
        //Check if Obstacle has passed the Player, has to be + 12f, because the player sees the center as 0 while the 
        //pipes see the actual beginning as 0
        if (transform.position.x < player.transform.position.x + 12f && !passed)
        {
            //Increase the Score inGame
            WebSocketHandler.Score++;
            //Send a Request to the Server, that an obstacle has been passed so the Score should be increased
            WebSocketHandler.Send(new Metadata(RequestType.Score, WebSocketHandler.name, ""));
            //Score has been Set, so we no longer need to check 
            passed = true;
        }
    }
}
