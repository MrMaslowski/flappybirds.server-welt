using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bird_movement : MonoBehaviour
{
    public ManagerGame gm;
    public float velocity = 1;
    private Rigidbody2D rb;
    public GameObject progressBar;
    AudioSource audioData;
    private double PlayerHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Add rigibody to component
        rb = GetComponent<Rigidbody2D>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If left mouse button is pressed (or spacebar)
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //WebSocketHandler.Send(new Metadata(RequestType.JumpPlayer, WebSocketHandler.name, transform.position.y));
    }

    public void Jump()
    {
        //Jump
        rb.velocity = Vector2.up * velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //call gameover function in game manager
        gm.GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OUCH");
        PlayerHealth -= 0.1;
        progressBar.GetComponent<Image>().fillAmount = (float)PlayerHealth;
        if(PlayerHealth <= 0)
        {
            gm.GameOver();
        }
        audioData.Play();
    }
}
