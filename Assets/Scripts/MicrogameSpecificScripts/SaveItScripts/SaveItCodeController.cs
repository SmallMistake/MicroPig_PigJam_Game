using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveItCodeController : MonoBehaviour
{
    [SerializeField]
    float minFallTime;

    [SerializeField]
    float maxFallTime;

    [SerializeField]
    SpriteRenderer fallingObjectSpriteRenderer;

    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    [SerializeField] 
    NumberReachedGoal goalIncrementor;

    [SerializeField]
    Animator fallingObjectAnimator;

    private List<string> possibleTriggers = new List<string>() { "downward", "left", "right"};

    // Start is called before the first frame update
    void Start()
    {
        fallingObjectSpriteRenderer.sprite = GetRandomSprite(sprites);
        float fallTime = Random.Range(minFallTime/GameManager.DifficultyTimeScale, maxFallTime/GameManager.DifficultyTimeScale);
        StartCoroutine(CountdownToFall(fallTime));
    }

    Sprite GetRandomSprite(List<Sprite> sprites)
    {
        int chosenSprite = Random.Range(0, sprites.Count);
        return sprites[chosenSprite];
    }

    IEnumerator CountdownToFall(float fallTime)
    {
        yield return new WaitForSeconds(fallTime);
        fallingObjectAnimator.SetTrigger(possibleTriggers.ChooseRandom());
        fallingObjectAnimator.speed = GameManager.DifficultyTimeScale;
    }

    public void CatchObjects(List<GameObject> caughtObjects)
    {
        foreach (GameObject gameObject in caughtObjects)
        {
            fallingObjectAnimator.speed = 0;
            goalIncrementor.IncreaseNumber(1);
        }
    }
}
