using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour
{
    void Update() //Exits the game as soon as the key escape is held.
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }       
    }
}
