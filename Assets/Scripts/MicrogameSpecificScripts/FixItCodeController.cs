using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixItCodeController : MicrogameGoalTracker
{
    [SerializeField]
    int hitsNeededToFix;

    int currentNumberOfHits = 0;

    [SerializeField]
    SpawnObjectInArea particleSpawner;

    [SerializeField]
    FMODUnity.StudioEventEmitter bangSoundEmitter;

    public GameObject arcadeMachine;
    private float amountToRotate = 30;


    private bool onLeftHit = true;


    public void HitMachine(bool isPrimaryControl)
    {
        if(goalComplete) return;
        if((isPrimaryControl && onLeftHit) || !isPrimaryControl && !onLeftHit)
        {
            bangSoundEmitter.Play();
            particleSpawner.SpawnObject(Random.Range(2, 5));
            onLeftHit = !onLeftHit;
            currentNumberOfHits++;
            RotateArcadeMachine(isPrimaryControl);
            if (currentNumberOfHits > hitsNeededToFix)
            {
                arcadeMachine.transform.rotation = new Quaternion(0, 0, 0, 0);
                particleSpawner.SpawnObject(15);
                CompleteGoal(true);
            }
        }
    }

    private void RotateArcadeMachine(bool leftward)
    {
        int multiplier = 1;
        if(currentNumberOfHits != 1)
        {
            multiplier = 2;
        }
        if(leftward)
        {
            arcadeMachine.transform.Rotate(new Vector3(0, 0, amountToRotate * multiplier));
        }
        else
        {
            arcadeMachine.transform.Rotate(new Vector3(0, 0, -amountToRotate * multiplier));
        }
    }
}
