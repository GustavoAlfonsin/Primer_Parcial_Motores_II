﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;

    public static bool winCondition = false;

    public static int actualPlayer = 0;

    public static int playerInPosition = 0;

    public static int totalPlayer;

    public List<Controller_Target> targets;

    public List<Controller_Player> players;

    void Start()
    {
        Physics.gravity = new Vector3(0, -30, 0);
        gameOver = false;
        winCondition = false;
        totalPlayer = players.Count;
        SetConstraits();
    }

    void Update()
    {
        GetInput();
        CheckWin();

    }

    private void CheckWin()
    {
        int i = 0;
        foreach(Controller_Target t in targets)
        {
            if (t.playerOnTarget)
            {
                i++;
                //Debug.Log(i.ToString());
            }
        }
        playerInPosition = i;
        if (i >= 6)
        {
            winCondition = true;
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (actualPlayer <= 0)
            {
                actualPlayer = 5;
                SetConstraits();
            }
            else
            {
                actualPlayer--;
                SetConstraits();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (actualPlayer >= 5)
            {
                actualPlayer = 0;
                SetConstraits();
            }
            else
            {
                actualPlayer++;
                SetConstraits();
            }
        }
    }

    private void SetConstraits()
    {
        foreach(Controller_Player p in players)
        {
            if (p == players[actualPlayer])
            {
                p.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                p.rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
