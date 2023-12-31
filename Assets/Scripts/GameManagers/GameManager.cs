using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using UnityEditor.Rendering;

public enum GameState
{
    None= 0,
    MainMenu,
    Storefront,
    TransitionToGame,
    TransitionToStore,
    Microgame,

    TransitionToEnd,
    EndGame
}

public class GameManager : MonoBehaviour
{

    #region Singleton Instance
    private static GameManager _instance;
    private static GameManager instance
    {
        get
        {
            if (GameManager._instance == null)
            {
                GameManager._instance = FindObjectOfType<GameManager>();
            }
            return GameManager._instance;
        }
    }
    #endregion Singleton instance
    
    public event EventHandler GameStateChangeEvent;
    //public event EventHandler CustomerChangeEvent;

    [SerializeField]
    private bool testCycleOnEnd;

    [SerializeField]
    private UIManager uiManager;

    private List<Customer> startingCustomers;

    [SerializeField]
    private List<Customer> customers;

    [SerializeField]
    private int _lossCountForGameOver = 4;

    public bool RemoveCompletedGames = false;

    private int _losses = 0;
    public static int Losses => instance._losses;
    public static bool GameLost => instance._losses >= instance._lossCountForGameOver;

    public delegate void onHealthChanged(int currentHealthPoints);
    public static onHealthChanged OnHealthChanged;

    public delegate void onLevelSuccess(int currentLevelsFinished);
    public static onLevelSuccess OnLevelSuccess;


    private float totalTime = 0f;
    private TimeControl time = new TimeControl();
    public static float DifficultyDeltaTime => Exists ? instance.time.DeltaTime : Time.deltaTime;
    public static float DifficultyFixedDeltaTime => Exists ? instance.time.FixedDeltatime : Time.fixedDeltaTime;

    public static float DifficultyTimeScale => Exists ? instance.time.DifficultyScale : 1f; //(timesSpeedUp * speedUpAmount) : 0f;
    public static float TimeScale
    {
        get => Exists ? instance.time.Scale : Time.timeScale;
        set
        {
            if (Exists) instance.time.SetScale(value);
        }
    }

    public float StorefrontDuration = 3f;
    public float TransitionDuration = 2f;

    #region Speed Up Variables

    [SerializeField, Tooltip("a multiplier to time scale as games are completed")]
    private float difficultyScaling;

    [SerializeField]
    private int gamesBetweenSpeedUp;

    int currentSpeedUpGameIndex = 0;

    internal bool needToDisplayNotice = false;

    #endregion


    private int _completedGames = 0;
    public static int CompletedGames => instance._completedGames;

    private GameState _state;
    public static GameState State => instance._state;

    // Queue the last X number of games played so that the player does not get the same game multiple times in a row;
    private Queue<string> previouslyPlayedMicrogameNames = new Queue<string>();
    private int numberOfGamesBetweenReplays = 3;

    /// <summary>
    /// how much time has passed within the current state,
    /// used to determine when it should be over, etc.
    /// </summary>
    private float stateTimer;

    private Customer _customer;
    public static Customer Customer => instance._customer;

    private Microgame _currentMicrogame;
    private GameObject activeMicrogameInstance;
    private Dictionary<GameState, GameState> transitions = new Dictionary<GameState, GameState>()
    {
        {GameState.MainMenu, GameState.Storefront },
        {GameState.Storefront, GameState.TransitionToGame},
        {GameState.TransitionToGame, GameState.Microgame },
        {GameState.Microgame, GameState.TransitionToStore },
        {GameState.TransitionToStore, GameState.Storefront },
        {GameState.TransitionToEnd, GameState.EndGame },
    };

    // we could create state objects but let's create state actions.
    private Dictionary<GameState, Action> updateActions = new Dictionary<GameState, Action>();
    private Dictionary<GameState, Action> beginStateActions = new Dictionary<GameState, Action>();
    private Dictionary<GameState, Action<GameState>> endStateActions = new Dictionary<GameState, Action<GameState>>();
    private Action currentUpdateAction;

    public static bool Exists => instance != null;

    private void Awake()
    {
        if (GameManager._instance == null)
        {
            GameManager._instance = this;
           
        }
        else if (GameManager._instance != this)
        {
            Debug.LogWarning("A different Game Manager already existed, destroying extra.");
            Destroy(this.gameObject);
        }

        if (GameManager._instance == this)
        {
            this.startingCustomers = new List<Customer>(this.customers);
            this.InitCustomers();

            this.beginStateActions[GameState.None] = this.DoNothing;
            this.beginStateActions[GameState.MainMenu] = this.BeginMenuState;
            this.beginStateActions[GameState.Microgame] = this.BeginMicrogameState;
            this.beginStateActions[GameState.TransitionToGame] = this.BeginTransitionState;
            this.beginStateActions[GameState.TransitionToStore] = this.BeginTransitionState;
            this.beginStateActions[GameState.Storefront] = this.BeginStorefrontState;
            this.beginStateActions[GameState.TransitionToEnd] = this.BeginTransitionState;
            this.beginStateActions[GameState.EndGame] = this.BeginEndGameState;

            this.updateActions[GameState.None] = this.DoNothing;
            this.updateActions[GameState.MainMenu] = this.UpdateMenuState;
            this.updateActions[GameState.Microgame] = this.UpdateMicrogameState;
            this.updateActions[GameState.TransitionToGame] = this.UpdateTransitionState;
            this.updateActions[GameState.TransitionToStore] = this.UpdateTransitionState;
            this.updateActions[GameState.Storefront] = this.UpdateStorefrontState;
            this.updateActions[GameState.TransitionToEnd] = this.UpdateTransitionState;
            this.updateActions[GameState.EndGame] = this.UpdateEndGameState;

            this.endStateActions[GameState.None] = (state) => this.DoNothing();
            this.endStateActions[GameState.MainMenu] = this.EndMenuState;
            this.endStateActions[GameState.Microgame] = this.EndMicrogameState;
            this.endStateActions[GameState.TransitionToGame] = this.EndTransitionState;
            this.endStateActions[GameState.TransitionToStore] = this.EndTransitionState;
            this.endStateActions[GameState.Storefront] = this.EndStorefrontState;
            this.endStateActions[GameState.TransitionToEnd] = this.EndTransitionState;
            this.endStateActions[GameState.EndGame] = this.EndEndGameState;

            this.uiManager.OnGameInitialize(this);
            this.uiManager.StartGameEvent += this.OnUIStartGameEvent;
        }

    }

    public void OnEnable()
    {
        MicrogameManager.onMicrogameFinished += this.OnMicrogameFinished;
    }
    public void OnDisable()
    {
        MicrogameManager.onMicrogameFinished -= this.OnMicrogameFinished;
       
    }

    private void InitCustomers()
    {
        Debug.Log("InitCustomers()");
        this.customers.Clear();
        Customer customerCopy;
        foreach (var customer in this.startingCustomers)
        {
            customerCopy = Instantiate(customer);
            customerCopy.name = customer.name;
            this.customers.Add(customerCopy);
        }
    }

    public static void AddStateListener(EventHandler handler)
    {
        // we make sure there's no accidental double-entry
        instance.GameStateChangeEvent -= handler;
        instance.GameStateChangeEvent += handler;
    }

    public static void RemoveStateListener(EventHandler handler)
    {
        instance.GameStateChangeEvent -= handler;
    }


    public void Start()
    {
        this.GoToState(GameState.Storefront);
    }

    private void OnUIStartGameEvent(object sender, EventArgs args)
    {
        this.GoToState(GameState.Storefront);
    }

    private void OnMicrogameFinished(bool success)
    {
        if (success)
        {
            this._completedGames++;
            HandleSpeedUpFunctionality();
            OnLevelSuccess?.Invoke(this._completedGames);
            //this.time.SetCounter(this._completedGames);
            if (this.RemoveCompletedGames)
            {
                this._customer.microGames.Remove(_currentMicrogame);
                if (this._customer.microGames.Count == 0)
                {
                    this.customers.Remove(this._customer);
                    if (this.customers.Count == 0)
                    {
                        this.GoToState(GameState.TransitionToEnd);
                    }
                }
            }
        }
        else 
        {
            IncreaseLosses(1);
            if (this._losses >= this._lossCountForGameOver)
            {
                this.GoToState(GameState.TransitionToEnd);
            }
        }

        if (this._state != GameState.TransitionToEnd)
        {
            this.GoToState(GameState.TransitionToStore);
        }
    }

    internal void IncreaseLosses(int amountToChange)
    {
        this._losses += amountToChange;
        OnHealthChanged?.Invoke(_lossCountForGameOver - _losses);
    }

    public bool HandleSpeedUpFunctionality()
    {
        currentSpeedUpGameIndex++;

        if (currentSpeedUpGameIndex >= gamesBetweenSpeedUp)
        {
            currentSpeedUpGameIndex = 0;
            this.time.AdjustDifficultyScale(this.difficultyScaling);
            needToDisplayNotice = true;

            return true;
        }
        return false;
    }

    /// <summary>
    /// Anyone can try to go to a state because game jam
    /// but we reserve the right to refuse.
    /// </summary>
    public static bool TryGoToState(GameState state)
    {
        // just do it for now
        instance.GoToState(state);
        return true;
    }

    /// <summary>
    /// Changes states if it's actually different,
    /// publishes a state change event to anyone who cares
    /// </summary>
    public void GoToState(GameState state)
    {
        Debug.Log("Go to state " + state);
        if (state != this._state)
        {
            this.endStateActions[this._state](state);
            this._state = state;
            this.BeginAnyState();
            this.beginStateActions[this._state]();
            this.currentUpdateAction = this.updateActions[state];
            this.GameStateChangeEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        if (this._state != GameState.None && this._state != GameState.MainMenu)
        {
            this.totalTime += time.DeltaTime;
        }
        this.time.Update(this.totalTime, Time.deltaTime);
        this.stateTimer += this.time.DeltaTime;
        this.currentUpdateAction?.Invoke();
    }

    private void FixedUpdate()
    {
        this.time.FixedUpdate(Time.fixedDeltaTime);
    }
    /// <summary>
    /// When we start a new state we clear the timer
    /// and set up any other initial values
    /// </summary>
    private void BeginAnyState()
    {
        this.stateTimer = 0f;
    }

    /// <summary>
    /// Does nothing. Just allows us to transition 
    /// to and from GameState.None without any errors.
    /// </summary>
    private void DoNothing() { }

    private void BeginMenuState() { } // the UI will handle most of this.

    /// <summary>
    /// Upon returning to the storefront we make
    /// all necessary selections for the game to progress.
    /// Called via GoToState(GameState.Storefront),
    /// before publishing the game state change event.
    /// </summary>
    private void BeginStorefrontState() 
    {
        ChooseMicroGame();
        Destroy(this.activeMicrogameInstance);
    }

    //Choose game and don't allow instant repeats
    private void ChooseMicroGame()
    {
        Customer customer = null;
        Microgame microgame = null;
        bool succeeded = false;
        while (!succeeded)
        {
            customer = this.customers.ChooseRandom();
            microgame = customer.microGames.ChooseRandom();
            if(!previouslyPlayedMicrogameNames.Contains(microgame.MicrogameName))
            {
                succeeded = true;
                previouslyPlayedMicrogameNames.Enqueue(microgame.MicrogameName);
                if(previouslyPlayedMicrogameNames.Count >= numberOfGamesBetweenReplays)
                {
                    previouslyPlayedMicrogameNames.Dequeue();
                }
            }
        }
        this._customer = customer;
        this._currentMicrogame = microgame;
    }

    
    private void BeginTransitionState() { } // UI probably handles this.
    
    /// <summary>
    /// Spawns the randomly chosen microgame
    /// </summary>
    private void BeginMicrogameState() 
    {
        this.activeMicrogameInstance = Instantiate(this._currentMicrogame.microgamePrefab);
        MicrogameManager microgameManager = this.activeMicrogameInstance.GetComponentInChildren<MicrogameManager>();
        if(microgameManager != null )
        {
            microgameManager.SetMicroGameName(_currentMicrogame.MicrogameName);
        }
    }

    private void BeginEndGameState() {
        Destroy(this.activeMicrogameInstance);
    }
    

    private void UpdateMenuState() { }
    private void UpdateStorefrontState() 
    { 
        // the UI will be showing the customer at this time.
        // after a preset number of seconds, we load the microgame
        if (this.stateTimer >= this.StorefrontDuration)
        {
            this.GoToState(GameState.TransitionToGame);
        }
    }
    private void UpdateTransitionState() 
    {
        if (this.stateTimer >= this.TransitionDuration)
        {
            this.GoToState(this.transitions[this._state]);
        }
    }

    private void UpdateMicrogameState() { }
    private void UpdateEndGameState() 
    {
        if (this.testCycleOnEnd && this.stateTimer > 5f)
        {
            this.InitCustomers();
            this._losses = 0;
            this._completedGames = 0;
            this.totalTime = 0f;
            //this.time.SetCounter(0);
            this.GoToState(GameState.Storefront);
        }
    }

    private void EndMenuState(GameState newState) { }
    private void EndStorefrontState(GameState newState) { }

    private void EndMicrogameState(GameState newState) 
    {
      
    }

    private void EndEndGameState(GameState newState) { }

    private void EndTransitionState(GameState newState)
    {

    }


    public Microgame GetCurrentMicrogame()
    {
        return this._currentMicrogame;
    }
  

}

