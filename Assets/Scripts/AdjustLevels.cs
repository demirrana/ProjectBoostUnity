using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdjustLevels : MonoBehaviour
{
    SceneManager sceneManager;

    void Update()
    {
        RestartLevel();
        PassLevel();
    }

    void RestartLevel()
    {
        if (Input.GetKey(KeyCode.AltGr))
        {
            int activeLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeLevel);
        }
    }

    void PassLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            int activeLevel = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = activeLevel + 1;

            if (nextLevel == SceneManager.sceneCountInBuildSettings)
            {
                nextLevel = 0;
            }

            SceneManager.LoadScene(nextLevel);
        }
    }
}
