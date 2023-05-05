using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TMP_InputField highScoreField;

    public void setHighScoreData(List<Score> myScores)
    {
        highScoreField.text = "";
        int i = 1;
        foreach(var score in myScores)
        {
            highScoreField.text += i + ". " + score.Name + ": " + score.Value + "\n";
            i++;
        }
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
