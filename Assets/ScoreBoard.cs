using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TMP_InputField highScoreField;
    private int HighScoreData;

    public void setHighScoreData(int data)
    {
        HighScoreData = data;
        highScoreField.text = "Highscore: " + HighScoreData;
    }


    // Start is called before the first frame update
    void Start()
    {
        WebSocketHandler.sb = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
