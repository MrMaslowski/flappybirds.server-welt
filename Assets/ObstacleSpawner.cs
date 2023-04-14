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
            if(data.Length == 0 || data == null)
            {
                return;
            }
            if(obstacleCounter >= data.Length)
            {
                return;
            }
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = transform.position + new Vector3(0, (data[obstacleCounter] / 100.0f) * 4f - 2f, 0);           
            Destroy(newpipe, 26);
            timer = 0;
            obstacleCounter++;
        }
        timer += Time.deltaTime;
    }
}