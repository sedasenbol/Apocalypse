using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.text = "";
        scoreText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowScore(int score)
    {
        scoreText.text = "Score: " + score.ToString("0000000");
    }
    public void GameOverText()
    {
        gameOverText.text = "Game Over";
    }
}
