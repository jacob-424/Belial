using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;
    [SerializeField] Text winText;
    [SerializeField] Text loseText;
    internal static bool gameOver;
    internal static bool gameWon;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        gameWon = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameWon) 
        {
            restartButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
        }

        if (gameOver) 
        {
            restartButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            loseText.gameObject.SetActive(true);
        }
    }

    public void OnRestartButtonPress() 
    {
        SceneManager.LoadScene("Belial");
    }

    public void OnMenuButtonPress() 
    {
        SceneManager.LoadScene("Menu");
    }
}
