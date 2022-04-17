using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerMA : MonoBehaviour
{
    #region Singleton

    public static PlayerManagerMA instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject Player;
}
