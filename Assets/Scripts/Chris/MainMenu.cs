using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject doorClosed;

    [SerializeField]
    GameObject door2;

    [SerializeField]
    GameObject door3;

    [SerializeField]
    GameObject doorOpen;

    [SerializeField]
    AudioSource blipSound;

    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartOpenDoor()
    {
        StartCoroutine(OpenDoor());
    }

    public void StartCloseDoor()
    {
        StartCoroutine(CloseDoor());
    }

    public IEnumerator OpenDoor()
    {
        anim.SetTrigger("GameStarted");
        yield return new WaitForSeconds(.5f);
        doorClosed.SetActive(false);
        door2.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(1f);
        door2.SetActive(false);
        door3.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(1f);
        door3.SetActive(false);
        doorOpen.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("MainGameScene");
    }


    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(.5f);
        doorOpen.SetActive(false);
        door3.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(1f);
        door3.SetActive(false);
        door2.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(1f);
        door2.SetActive(false);
        doorClosed.SetActive(true);
        blipSound.Play();
        yield return new WaitForSeconds(.5f);
        Application.Quit();

    }
}
