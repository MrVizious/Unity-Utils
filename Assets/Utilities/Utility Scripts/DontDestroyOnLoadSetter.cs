// Created by: Javier Riera (https://mrvizious.github.io)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadSetter : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    #endregion

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
