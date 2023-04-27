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
        if (transform.position.x < player.transform.position.x + 12f && !passed)
        {
            Debug.Log("sending Data");
            WebSocketHandler.Send(new Metadata(RequestType.Score, WebSocketHandler.name, ""));
            passed = true;
        }
    }
}
