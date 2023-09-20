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
    GameObject fallingObject;

    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    [SerializeField] 
    NumberReachedGoal goalIncrementor;

    // Start is called before the first frame update
    void Start()
    {
        fallingObject.GetComponent<SpriteRenderer>().sprite = GetRandomSprite(sprites);
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
        var rb = fallingObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = GameManager.DifficultyTimeScale;
    }

    public void CatchObjects(List<GameObject> caughtObjects)
    {
        foreach (GameObject gameObject in caughtObjects)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            goalIncrementor.IncreaseNumber(1);
        }
    }
}
