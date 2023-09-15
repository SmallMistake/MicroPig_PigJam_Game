using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlHint { leftClick, alternatingClick, drag, hidden }

[Serializable]
public class Microgame
{
    public string MicrogameName;
    public GameObject microgamePrefab;
    public ControlHint controlHint;
}
