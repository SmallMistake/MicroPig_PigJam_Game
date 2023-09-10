using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   public static T ChooseRandom<T>(this List<T> list, bool remove = false)
    {
        int index = Random.Range(0, list.Count);
        T ret_selection = list[index];
        if (remove) list.RemoveAt(index);
        return ret_selection;
    }
}
