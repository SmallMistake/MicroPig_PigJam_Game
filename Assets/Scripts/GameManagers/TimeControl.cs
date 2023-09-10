using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A custom clock for the game allowing us to internally
/// scale time for difficulty. Games can just reference
/// GameManager.DeltaTime for an auto-scaled time.
/// </summary>
public class TimeControl
{
    public float DifficultyScale { get; private set; }
    public float DeltaTime { get; private set; }

    public float Scale { get; private set; }

    /// <summary>
    /// An optional way to increase difficulty
    /// as per game beaten instead of total time played.
    /// </summary>
    public int Counter { get; private set; }

    public void SetScale(float scale)
    {
        this.Scale = scale;
    }

    public void SetCounter (int counter)
    {
        this.Counter = counter;
    }

    public void Update(float totalTime, float deltaTime)
    {
        this.DeltaTime = deltaTime;
    }
}
