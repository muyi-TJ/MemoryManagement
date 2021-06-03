﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : Button
{
    // Start is called before the first frame update
    void Start()
    {
        this.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("Initial");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}