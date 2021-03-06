﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    public float xInitialForce;
    public float yInitialForce;

    private Vector2 trajectoryOrigin;

    public GameManager gameManager;

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
        gameManager.ResetPlayerScale();
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        float speed = new Vector2(xInitialForce, yInitialForce).magnitude;

        if(randomDirection < 1f)
        {
            Vector2 direction = new Vector2(-xInitialForce, yRandomInitialForce).normalized;
            rigidBody2D.AddForce(direction * speed);
        }
        else
        {
            Vector2 direction = new Vector2(xInitialForce, yRandomInitialForce).normalized;
            rigidBody2D.AddForce(direction * speed);
        }
    }

    void RestartGame()
    {
        ResetBall();
        CancelInvoke("PushBall");
        Invoke("PushBall", 2);
    }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
