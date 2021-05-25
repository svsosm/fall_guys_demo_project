﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public void PlayGame()
    {
        SceneLoader.Instance.NextScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
