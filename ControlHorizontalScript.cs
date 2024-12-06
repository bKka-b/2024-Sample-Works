using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Numerics;

public class HorizontalControlScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public ScoreboardScript logic;
    public AddHighscore highscoreScript;

    public ReadPlayerInput readInput;
    public GameObject gridContent;

    public bool playerAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreboardScript>();
        highscoreScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<AddHighscore>();
        readInput = GameObject.FindGameObjectWithTag("Logic").GetComponent<ReadPlayerInput>();   
        
    }
    void Update()
    {
        setPlayerHeight();
    }

    private void setPlayerHeight()
    {
        UnityEngine.Vector2 scrollPos = transform.position;
        scrollPos.y = ((gridContent.transform.position.x) / (float)(2.05)) + (float)(0.05);
        transform.position = scrollPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 3) && (playerAlive == true) && (collision.gameObject.tag == "Obstacle"))
        {
            logic.gameOver();
            myRigidbody.velocity = UnityEngine.Vector2.right * 5;
            playerAlive = false;

            int score = logic.playerScore;
            string name = ReadPlayerInput.playerName;
            
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            highscoreScript.AddHighscoreEntry(score, name, year, month, day);
        }
    }
}
