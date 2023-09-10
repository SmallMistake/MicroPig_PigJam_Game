using TMPro;
using UnityEngine;

public class MicrogameHUDManager : MonoBehaviour
{
    public TextMeshProUGUI announcementText;
    public TextMeshProUGUI burnOutText;
    public TextMeshProUGUI wellDoneText;

    public AudioClip victoryAudioClip;
    public AudioClip lossAudioClip;

    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        MicrogameManager.onMicrogameFinished += HandleFinish;
    }

    private void OnDisable()
    {
        MicrogameManager.onMicrogameFinished -= HandleFinish;
    }

    public void AnnounceGame(string gameName)
    {
        announcementText.gameObject.SetActive(true);
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
        audioSource.PlayOneShot(lossAudioClip);
    }

    private void WellDone()
    {
        wellDoneText.gameObject.SetActive(true);
        audioSource.PlayOneShot(victoryAudioClip);
    }
}
