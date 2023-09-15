using DG.Tweening.Core.Easing;
using System.Collections;
using TMPro;
using UnityEngine;

public class MicrogameHUDManager : MonoBehaviour
{
    public TextMeshProUGUI announcementText;
    public TextMeshProUGUI burnOutText;
    public TextMeshProUGUI wellDoneText;

    public FMODUnity.StudioEventEmitter successEmitter;
    public FMODUnity.StudioEventEmitter failureEmitter;

    private AudioSource audioSource;

    private void Awake()
    {
        MicrogameManager.onMicrogameAnnounce += AnnounceGame;
        MicrogameManager.onMicrogameFinished += HandleFinish;
    }

    private void OnDestroy()
    {
        MicrogameManager.onMicrogameAnnounce -= AnnounceGame;
        MicrogameManager.onMicrogameFinished -= HandleFinish;
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AnnounceGame(string gameName, float announceTime)
    {
        burnOutText.gameObject.SetActive(false);
        wellDoneText.gameObject.SetActive(false);
        announcementText.text =  $"<bounce a=0.2>{gameName}</bounce>";
        announcementText.gameObject.SetActive(true);
        StartCoroutine(HandleAnnounceFinishCoundown(announceTime));
    }

    IEnumerator HandleAnnounceFinishCoundown(float countdown)
    {
        yield return new WaitForSeconds(countdown);
        announcementText.gameObject.SetActive(false);
    }

    private void HandleFinish(bool won)
    {
        if (won)
        {
            WellDone();
        }
        else
        {
            BurnOut();
        }
    }

    private void BurnOut()
    {
        burnOutText.gameObject.SetActive(true);
        failureEmitter.Play();
    }

    private void WellDone()
    {
        wellDoneText.gameObject.SetActive(true);
        successEmitter.Play();
    }
}
