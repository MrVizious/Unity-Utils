using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsTest : MonoBehaviour
{
    public void LogMessage()
    {
        Debug.Log("GameEvent Test");
    }
    public void LogInt(int value)
    {
        Debug.Log("GameEvent Test: " + value);
    }
}