﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedData : MonoBehaviour
{
    public int difficulty;
    public int timer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}