using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public event EventHandler StartGameEvent;

    [SerializeField]
    private CustomerUI customerUI;

    [SerializeField]
    private RectTransform shutterScreen;

    [SerializeField] 
    private Animator controlHintAnimator; //Found on the shutter

    [SerializeField]
    private GameObject
        microgameLayer,
        storefrontLayer,
        mainMenuLayer,
        endGameLayer;

    [SerializeField]
    private Image endGameBackdrop;

    public TextMeshProUGUI levels;

    private GameManager gameManager;

    public void OnGameInitialize(GameManager manager)
    {
        manager.GameStateChangeEvent -= this.OnGameStateChangeEvent;
        manager.GameStateChangeEvent += this.OnGameStateChangeEvent;
    }

    private void Awake()
    {
        this.endGameLayer.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnGameStateChangeEvent(object sender, EventArgs args)
    {
        
        switch (GameManager.State)
        {
            case GameState.TransitionToGame:
                controlHintAnimator.SetTrigger(gameManager.GetCurrentMicrogame().controlHint.ToString());
                PlayInShutter();
                break;
            case GameState.TransitionToStore:
                controlHintAnimator.SetTrigger(ControlHint.hidden.ToString());
                PlayInShutter();
                break;
            case GameState.Storefront:
                PlayOutShutter();
                this.customerUI.SetCustomer(GameManager.Customer);
                this.storefrontLayer.SetActive(true);
                this.microgameLayer.SetActive(false);
                this.mainMenuLayer.SetActive(false);
                this.endGameLayer.SetActive(false);
                break;
            case GameState.Microgame:
                PlayOutShutter();
                this.storefrontLayer.SetActive(false);
                this.microgameLayer.SetActive(true);
                
                break;
            case GameState.MainMenu:
                this.storefrontLayer.SetActive(false);
                this.microgameLayer.SetActive(false);
                this.mainMenuLayer.SetActive(true);
                this.endGameLayer.SetActive(false);
                break;
            case GameState.EndGame:
                levels.text = GameManager.CompletedGames.ToString();
                this.endGameLayer.SetActive(true);
                this.storefrontLayer.SetActive(false);
                this.microgameLayer.SetActive(false);
                this.mainMenuLayer.SetActive(false);
                break;

           
        }
        
    }

    public void ClickStart()
    {
        this.StartGameEvent?.Invoke(this, EventArgs.Empty);
    }

    private void PlayInShutter()
    {
        Debug.Log("play in shutter");
        this.shutterScreen.DOKill();
        this.shutterScreen.DOLocalMoveY(0f, 1.5f);
    }

    private void PlayOutShutter()
    {
        this.shutterScreen.DOKill();
        this.shutterScreen.DOLocalMoveY(1080f, 1.5f);
    }

}
