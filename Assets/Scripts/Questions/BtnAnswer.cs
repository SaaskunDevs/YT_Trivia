using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAnswer : MonoBehaviour
{
    [SerializeField] int currentIndex;

    public void SetIndex(int index)
    {
        currentIndex = index;
    }

    public int GetIndex()
    {
        return currentIndex;
    }
}
