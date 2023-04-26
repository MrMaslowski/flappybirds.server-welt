using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float maxTime;
    private float timer = 0;
    public GameObject pipe;
    private int obstacleCounter = 0;
    public float[] data;

    // Start is called before the first frame update
    void Start()
    {
        WebSocketHandler.os = this;
    }

    //load obstacle height data
    public void setObstacleDataFromWebSocketHandler(float[] data)
    {
        //Set data!
        this.data = data;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            //If data is null, dont spawn pipes, otherwise an error occurs
            if(data.Length == 0 || data == null)
            {
                return;
            }
            //If All pipes were spawned. Dont try to spawn new ones!
            if(obstacleCounter >= data.Length)
            {
                return;
            }
            GameObject newpipe = Instantiate(pipe);
            //Pipe spawns at height from data/server
            newpipe.transform.position = transform.position + new Vector3(23, (data[obstacleCounter]) * 4f - 2f, 0);   
            //Destroy pipes after 26 sec!
            Destroy(newpipe, 26);
            timer = 0;
            obstacleCounter++;
        }
        timer += Time.deltaTime;
    }
}