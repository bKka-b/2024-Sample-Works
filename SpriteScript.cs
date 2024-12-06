using System.Collections;
using System.Collections.Generic;
using System;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public ScoreboardScript logic;
    public AddHighscore highscoreScript;
    public ReadPlayerInput readInput;

    public PlayerInput playerInput;
    public InputAction movementAction;

    public bool playerAlive = true;
    public float moveSpeed = 4;
    public Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreboardScript>();
        highscoreScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<AddHighscore>();
        readInput = GameObject.FindGameObjectWithTag("Logic").GetComponent<ReadPlayerInput>();  
        playerInput = GameObject.FindGameObjectWithTag("TouchManager").GetComponent<PlayerInput>();
        movementAction = playerInput.actions["SpriteMovement"];
    }

    void Update()
    {
        // Checking for player movement
        if (playerAlive == true)
        {
            moveDirection = movementAction.ReadValue<Vector2>();
        }

    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = new UnityEngine.Vector2(0, moveDirection.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kills player with collision and save of name and date is created
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
