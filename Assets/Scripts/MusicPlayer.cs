﻿using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        CreateSingletonObject();
    }
    
    private void CreateSingletonObject()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
