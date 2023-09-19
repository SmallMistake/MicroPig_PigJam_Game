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
    public float FixedDeltatime { get; private set; }

    /// <summary>
    /// this is a general scale muliplier, separate
    /// from difficulty multiplier.
    /// </summary>
    public float Scale { get; private set; }

    /// <summary>
    /// An optional way to increase difficulty
    /// as per game beaten instead of total time played.
    /// </summary>
    public int Counter { get; private set; }

    public TimeControl()
    {
        this.Scale = 1f;
        this.DifficultyScale = 1f;
    }

    public void SetScale(float scale)
    {
        this.Scale = scale;
    }

    public void SetDifficultyScale(float multiplier)
    {
        this.DifficultyScale = multiplier;
    }

    public void AdjustDifficultyScale(float multiplier)
    {
        this.DifficultyScale *= multiplier;
    }

    public void Update(float totalTime, float deltaTime)
    {
        this.DeltaTime = deltaTime * this.Scale * this.DifficultyScale;

    }

    public void FixedUpdate(float fixedDeltatime)
    {
        this.FixedDeltatime = fixedDeltatime * this.Scale * this.DifficultyScale;
      
    }
}
