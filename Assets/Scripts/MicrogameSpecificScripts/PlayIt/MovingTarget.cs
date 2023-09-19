using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingTarget : MicrogameGoalTracker
{
    public event EventHandler KilledEvent;

    public int HitPoints = 1;


    public Vector2 MoveRangeX;
    public Vector2 MoveRangeY;

    public float DistanceThreshold = 0.25f;

    public float MoveSpeed;

    public Rigidbody2D Rigidbody;

    private Vector2 goal;
    private Vector2 viewportGoal;
    private Vector2 screenGoal;
    private bool isAlive;
    

    public GameObject 
        AliveContent, 
        DeadContent;
   
    private void Start()
    {
        this.SetAlive(true);
        this.AliveContent.SetActive(true);
        this.DeadContent.SetActive(false);
        this.GetNewGoal();
    }

    private void Update()
    {
        if (this.isAlive)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.goal, this.MoveSpeed * GameManager.DifficultyDeltaTime);
            if (Vector3.Distance(this.transform.position, goal) < this.DistanceThreshold)
            {
                this.GetNewGoal();
            }
        }

    }

    public void SetAlive(bool alive)
    {
        this.isAlive = alive;
        this.AliveContent.SetActive(alive);
        this.DeadContent.SetActive(!alive);
        if (!this.isAlive)
        {
            this.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            this.goalComplete = true;
            this.onGoalStatusChanged?.Invoke(true);
        }
        
    }
    void GetNewGoal()
    {

        this.viewportGoal.x = Random.Range(MoveRangeX[0], MoveRangeX[1]);
        this.viewportGoal.y = Random.Range(MoveRangeY[0], MoveRangeY[1]);

        this.screenGoal = Camera.main.ViewportToScreenPoint(this.viewportGoal);

        this.goal = Camera.main.ScreenToWorldPoint(screenGoal) + (Vector3.forward * 10f);
        if (this.goal.x > this.transform.position.x)
        {
            this.transform.localEulerAngles = Vector3.up * 180f;
        }
        else
        {
            this.transform.localEulerAngles = Vector3.zero;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (--this.HitPoints <= 0)
        {
            this.SetAlive(false);
            this.KilledEvent?.Invoke(this, EventArgs.Empty);
        }
    
    }
}
