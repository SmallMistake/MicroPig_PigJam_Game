using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Net.NetworkInformation;

public class ChangeItCodeManager : MicrogameGoalTracker
{
    [SerializeField]
    private List<GameObject> channels;
    [SerializeField]
    private GameObject swapNoise;
    private int startIndex;

    public GameObject ThumbsUp;

    public TextMeshPro channelTextMesh;

    private GameObject activeChannelObject;

    private IEnumerator channelChangeRoutine;

    public MicrogameManager Manager;
    public int CurrentChannel;

    public int DesiredChannel;

    private bool gameover = false;


    [SerializeField]
    private float timeTillSuccessShortcut; //If a player gets the correct awnser and has to wait forever it is boring. So this allows the player to skip after a set time.
    private Coroutine successCoroutine;


    private void Awake()
    {
        channelTextMesh.text = "";
        this.DesiredChannel = Random.Range(0, channels.Count);
        this.startIndex = (this.DesiredChannel + Random.Range(1, channels.Count - 1)) % this.channels.Count;
        if (this.startIndex < 0) this.startIndex += this.channels.Count;
        this.channels[startIndex].SetActive(true);
        this.CurrentChannel = this.startIndex;
        this.activeChannelObject = this.channels[startIndex];
    }

    private void OnDisable()
    {
        if(successCoroutine != null)
        {
            StopCoroutine(successCoroutine);
        }
    }

    private void Update()
    {
        if (!this.gameover)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.ChangeChannel(1);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                this.ChangeChannel(-1);
            }
            this.channelChangeRoutine?.MoveNext();
        }
    }

    private void ChangeChannel(int adjustment)
    {
        this.CurrentChannel = (this.CurrentChannel + adjustment) % this.channels.Count;
        if (this.CurrentChannel < 0) this.CurrentChannel += this.channels.Count;
        channelTextMesh.text = (CurrentChannel + 1).ToString();
        if (this.channelChangeRoutine == null)
        {
            this.channelChangeRoutine = this.ChannelChangeProcess();
        }
        
    }

    private IEnumerator ChannelChangeProcess()
    {

        if(successCoroutine != null)
        {
            StopCoroutine(successCoroutine);
            successCoroutine = null;
        }
        if (this.DesiredChannel == this.CurrentChannel)
        {
            successCoroutine = StartCoroutine(CountdownToSuccess());
        }

        this.swapNoise.SetActive(true);
        float time = 0.1f;
        while (time > 0f)
        {
            time -= GameManager.DifficultyDeltaTime;
            yield return null;
        }
        
        this.activeChannelObject.SetActive(false);
        this.activeChannelObject = this.channels[this.CurrentChannel];
        this.activeChannelObject.SetActive(true);
        this.swapNoise.SetActive(false);

        this.channelChangeRoutine = null;
        this.RefreshThumbsUp();
    }

    public void TimeOut()
    {
        this.gameover = true;
        this.onGoalStatusChanged?.Invoke(true);
        if (this.CurrentChannel == this.DesiredChannel)
        {
            this.goalComplete = true;
        }
        else
        {
            Manager.TimeOut();
        }
    }

    private void RefreshThumbsUp()
    {
        if (this.DesiredChannel == this.CurrentChannel)
        {
            this.ThumbsUp.transform.DOKill();
            this.ThumbsUp.transform.localScale = Vector3.one * 0.5f;
            this.ThumbsUp.SetActive(true);
            this.ThumbsUp.transform.DOScale(1f, 0.5f);
         
        }
        else
        {
            this.ThumbsUp.SetActive(false);
        }
    }

    IEnumerator CountdownToSuccess()
    {
        yield return new WaitForSeconds(timeTillSuccessShortcut);
        CompleteGoal(true);
        successCoroutine = null;
    }
}
