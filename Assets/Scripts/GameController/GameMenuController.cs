/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Manages menu scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonPress() {
        SceneManager.LoadScene("Belial");
    }

    public void OnExitButtonPress() {
        Application.Quit();
    }
}
