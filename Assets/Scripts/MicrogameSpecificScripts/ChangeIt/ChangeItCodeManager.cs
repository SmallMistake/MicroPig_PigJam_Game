using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeItCodeManager : MicrogameGoalTracker
{
    [SerializeField]
    private List<GameObject> channels;
    [SerializeField]
    private GameObject swapNoise;
    private int startIndex;

    public GameObject ThumbsUp;

    private GameObject activeChannelObject;

    private IEnumerator channelChangeRoutine;

    public MicrogameManager Manager;
    public int CurrentChannel;

    public int DesiredChannel;

    private bool gameover = false;

    private void Awake()
    {
        this.DesiredChannel = Random.Range(0, channels.Count);
        this.startIndex = (this.DesiredChannel + Random.Range(1, channels.Count - 1)) % this.channels.Count;
        if (this.startIndex < 0) this.startIndex += this.channels.Count;
        this.channels[startIndex].SetActive(true);
        this.CurrentChannel = this.startIndex;
        this.activeChannelObject = this.channels[startIndex];
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
        if (this.channelChangeRoutine == null)
        {
            this.channelChangeRoutine = this.ChannelChangeProcess();
        }
        
    }

    private IEnumerator ChannelChangeProcess()
    {
        this.swapNoise.SetActive(true);
        float time = 0.1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
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
        if (this.CurrentChannel == this.DesiredChannel)
        {
            this.goalComplete = true;
            this.onGoalStatusChanged?.Invoke(true);
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
}
