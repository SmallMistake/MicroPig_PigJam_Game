using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Super gross audio solutions to get this out quick. Replace with fmod later
public class SpawnAudioSource : MonoBehaviour
{
    public Transform futureParent;
    public AudioClip audioClipToPlay;
    public float volume = 1;

    public void SpawnAudio()
    {
        GameObject audioSource = new GameObject();
        audioSource.transform.parent = futureParent;
        audioSource.AddComponent<AudioSource>();
        audioSource.AddComponent<DestroyAfterTime>();
        audioSource.GetComponent<AudioSource>().PlayOneShot(audioClipToPlay, volume);
    }
}
