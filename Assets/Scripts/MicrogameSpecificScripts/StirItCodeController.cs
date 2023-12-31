using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirItCodeController : MicrogameGoalTracker
{
    [SerializeField]
    int stirsNeeded;
    int timesStired = 0;

    [SerializeField]
    float rotateForceAmount;

    [SerializeField]
    Rigidbody2D rigidBodyToRotate;

    [SerializeField]
    List<GameObject> stirPoints = new List<GameObject>();

    private int? pastStirPointIndex;

    [SerializeField]
    private Color forwardColor;
    [SerializeField]
    private Color backwardColor;

    public void Stir(Collider2D callingStirPoint)
    {
        CalculateStir(callingStirPoint.gameObject);
        if (
            timesStired >= stirsNeeded ||
            timesStired <= -stirsNeeded
        )
        {
            CompleteGoal(true);
        }

    }

    private void CalculateStir(GameObject callingStirPoint)
    {
        int currentStirPointIndex = stirPoints.IndexOf(callingStirPoint);

        int higherStirPoint = currentStirPointIndex + 1;
        int lowerStirPoint = currentStirPointIndex - 1;

        if(higherStirPoint >= stirPoints.Count )
        {
            higherStirPoint = 0;
        }
        if(lowerStirPoint < 0)
        {
            lowerStirPoint = stirPoints.Count - 1;
        }

        if(pastStirPointIndex == null)
        {
            Spin(-1);
        }
        else if(pastStirPointIndex == lowerStirPoint)
        {
            Spin(-1);
        }
        else if (pastStirPointIndex == higherStirPoint)
        {
            Spin(1);
        }
        TurnOnAndOffTriggerAreas(higherStirPoint, lowerStirPoint);

        pastStirPointIndex = stirPoints.IndexOf(callingStirPoint);
}

    private void TurnOnAndOffTriggerAreas(int higherStirPoint, int lowerStirPoint)
    {
        for(int i = 0; i < stirPoints.Count; i++)
        {
            if (higherStirPoint == i)
            {
                stirPoints[i].SetActive(true);
                stirPoints[i].GetComponentInChildren<SpriteRenderer>().color = forwardColor;
            }
            else if(lowerStirPoint == i)
            {
                stirPoints[i].SetActive(true);
                stirPoints[i].GetComponentInChildren<SpriteRenderer>().color = backwardColor;
            }
            else{
                stirPoints[i].SetActive(false);
            }
        }
    }

    private void Spin(int amountToSpin) {
        timesStired += amountToSpin;

        if(amountToSpin > 0)
        {
            rigidBodyToRotate.AddTorque(rotateForceAmount, ForceMode2D.Impulse);
        } else if (amountToSpin < 0)
        {
            rigidBodyToRotate.AddTorque(-rotateForceAmount, ForceMode2D.Impulse);
        }
    }
}
