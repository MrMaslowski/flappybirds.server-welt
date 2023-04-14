using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_movement : MonoBehaviour
{
    public ManagerGame gm;
    public float velocity = 1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Add rigibody to component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If space button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //Jump
            rb.velocity = Vector2.up * velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //call gameover function in game manager
        gm.GameOver();
    }
}
