using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwipeItCodeManager : MicrogameGoalTracker
{
    private enum State
    {
        GreenLight,
        Warning,
        RedLight
    }

    [SerializeField]
    private Transform
        movableArm,
        startPoint,
        endPoint;

    public float ArmSpeed;
    public float RetractSpeed;

    [SerializeField]
    private GameObject
        redLightContent,
        greenLightContent,
        warningContent,
        grabbedFry;

    public Vector2 RedLightDurationRange;
    public Vector2 GreenLightDurationRange;

    [SerializeField]
    private MicrogameManager manager;

    private State state;
    private float stateTimer;
    private bool isRedLight => this.state == State.RedLight;
    private bool isGreenlight => this.state == State.GreenLight;
    private bool isWarning => this.state == State.Warning;

    public float WarningDuration = 0.2f;
    private float progress;
    private bool failed = false;
    private bool gameover => goalComplete || failed;
    private Dictionary<State, State> stateTransition = new Dictionary<State, State>()
    {
        { State.GreenLight, State.Warning },
        { State.Warning, State.RedLight },
        { State.RedLight, State.GreenLight }
    };

    private void Start()
    {
        this.SetState(State.GreenLight);
    }
    private void Update()
    {
       
        if (!gameover && Input.GetMouseButton(0))
        {
            if (this.isRedLight)
            {
                this.failed = true;
                this.manager.TimeOut();
            }
            this.progress += GameManager.DifficultyDeltaTime * this.ArmSpeed;
            if (this.progress >= 1f && !this.failed)
            {
                this.goalComplete = true;
                this.grabbedFry.SetActive(true);
                this.onGoalStatusChanged?.Invoke(true);
            }
        }

        if (this.progress < 1f && !this.failed)
        {
            this.movableArm.transform.position = Vector3.Lerp(this.startPoint.position, this.endPoint.position, this.progress);

            this.stateTimer -= GameManager.DifficultyDeltaTime;
            if (this.stateTimer <= 0f)
            {
                this.SetState(this.stateTransition[this.state]);
            }
        }
        else if (this.progress >= 1f && !this.failed)
        {
            this.movableArm.transform.position = 
                Vector3.Lerp(this.movableArm.transform.position, this.startPoint.position, GameManager.DifficultyDeltaTime * this.RetractSpeed);
        }
    }

    private void SetState(State state)
    {
        this.state = state;
        this.redLightContent.SetActive(this.isRedLight);
        this.greenLightContent.SetActive(!this.isRedLight);
        this.warningContent.SetActive(this.isWarning);
        if (!this.isWarning)
        {
            Vector2 durationRange = this.isRedLight ? this.RedLightDurationRange : this.GreenLightDurationRange;
            this.stateTimer = Random.Range(durationRange[0], durationRange[1]);
        }
        else
        {
            this.stateTimer = this.WarningDuration;
        }
    } 



}
