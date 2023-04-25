using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void StartGame()
    {
        Debug.Log("test");
        SceneManager.LoadScene("SampleScene");
    }

    public void OnTextInputChanged(string newValue)
    {
        Debug.Log("New text input value: " + newValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
