using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer", order = 1)]
public class Customer : ScriptableObject
{
    public string customerName;
    public List<Sprite> sprites;
    public List<Microgame> microGames;
  
}
