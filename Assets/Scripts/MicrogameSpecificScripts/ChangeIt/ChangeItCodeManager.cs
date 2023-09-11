using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItCodeManager : MicrogameGoalTracker
{
    [SerializeField]
    private List<GameObject> channels;
    private GameObject swapNoise;
    private int channelIndex;
    public GameObject ActiveChannel;

    private IEnumerator channelChangeRoutine;

    public MicrogameManager Manager;

    public int DesiredIndex;
    private void Awake()
    {
        this.DesiredIndex = Random.Range(0, channels.Count);
    }

    private void Update()
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

    private void ChangeChannel(int channel)
    {
        this.channelIndex = (this.channelIndex + channel) % this.channels.Count;
        
        if (this.channelChangeRoutine == null)
        {
            this.channelChangeRoutine = this.ChannelChangeProcess();
        }
        
    }

    private IEnumerator ChannelChangeProcess()
    {
        this.swapNoise.SetActive(true);
        float time = 0.6f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        this.ActiveChannel.SetActive(false);
        this.ActiveChannel = this.channels[this.channelIndex];
        this.ActiveChannel.SetActive(true);
        this.swapNoise.SetActive(false);

        this.channelChangeRoutine = null;
    }

    public void TimeOut()
    {
        if (this.channelIndex == this.DesiredIndex)
        {
            this.goalComplete = true;
            this.onGoalStatusChanged?.Invoke(true);
        }
        else
        {
            Manager.TimeOut();
        }
    }
}
