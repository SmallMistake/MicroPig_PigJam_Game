using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedUpNoticeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    float triggerOffset;

    public UnityEvent onSpeedUpNoticeTrigger;

    private void Start()
    {
        if(gameManager == null)
        {
            gameManager = GetComponent<GameManager>();
        }
    }

    private void OnEnable()
    {
        if (gameManager.needToDisplayNotice)
        {
            gameManager.needToDisplayNotice = false;
            StartCoroutine(HandleTrigger());
        }
    }

    IEnumerator HandleTrigger()
    {
        yield return new WaitForSeconds(triggerOffset);
        onSpeedUpNoticeTrigger?.Invoke();
    }
}
