using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float maxTime;
    private float timer = 0;
    public GameObject pipe;
    private int obstacleCounter = 0;
    private float[] data;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newpipe = Instantiate(pipe);
    }

    //load obstacle height data
    public void setObstacleDataFromWebSocketHandler(float[] data)
    {
        this.data = data;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            Debug.Log("SPAWNED NEW PIPE!");
            GameObject newpipe = Instantiate(pipe);
            // First pipe spawns infinite times ? I dont know why
            newpipe.transform.position = transform.position + new Vector3(0, (data[obstacleCounter] / 100.0f) * 2.5f, 0);           
            Destroy(newpipe, 20);   //After 20 Seconds the pipe is destroyed --> Maybe 20sec isnt the most efficient value? --> Hasn't been tested yet
            timer = 0;
            obstacleCounter++;
        }
        timer += Time.deltaTime;
    }
}