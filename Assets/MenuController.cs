using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public TMP_InputField inputField;

    public void Start()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeBoarderColor() 
    {
        Color color = Color.red;
        // Get the input field's background image component
        Image inputFieldImage = inputField.image;

        // Modify the image's properties to change the border color
        inputFieldImage.type = Image.Type.Sliced;
        inputFieldImage.color = color;
        inputFieldImage.fillCenter = true;
        
        GameObject webSocketObject = GameObject.Find("WebSocket");
    }

    public void OnTextInputChanged(string newValue)
    {
        Debug.Log("New text input value: " + newValue);
        WebSocketHandler.Send(new Metadata(RequestType.Name, newValue, newValue));

        PlayerSpawn.startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
