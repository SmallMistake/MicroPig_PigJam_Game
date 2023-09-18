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
    private CustomerHolderController customerHolderController;

    [SerializeField]
    private RectTransform shutterScreen;

    [SerializeField] 
    private Animator controlHintAnimator; //Found on the shutter

    [SerializeField]
    private List<GameObject>
        microgameLayer,
        storefrontLayer,
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
        foreach (GameObject layerPart in endGameLayer)
        {
            layerPart.SetActive(false);
        }
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
                foreach (GameObject layerPart in storefrontLayer)
                {
                    layerPart.SetActive(true);
                }
                foreach (GameObject layerPart in microgameLayer)
                {
                    layerPart.SetActive(false);
                }
                foreach (GameObject layerPart in endGameLayer)
                {
                    layerPart.SetActive(false);
                }
                this.customerHolderController.SetCustomer(GameManager.Customer);
                break;
            case GameState.Microgame:
                PlayOutShutter();
                foreach (GameObject layerPart in storefrontLayer)
                {
                    layerPart.SetActive(false);
                }
                foreach (GameObject layerPart in microgameLayer)
                {
                    layerPart.SetActive(true);
                }

                break;
            case GameState.EndGame:
                foreach (GameObject layerPart in endGameLayer)
                {
                    layerPart.SetActive(true);
                }
                foreach (GameObject layerPart in storefrontLayer)
                {
                    layerPart.SetActive(false);
                }
                foreach (GameObject layerPart in microgameLayer)
                {
                    layerPart.SetActive(false);
                }
                levels.text = GameManager.CompletedGames.ToString();
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
