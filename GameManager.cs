
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    new public Camera camera;

    public GameObject ball;
    public TMP_Text scoreUi;
    public TMP_Text livesUi;
    public TMP_Text finalScoreUi;
    public TMP_Text maxScoreUi;
    public GameObject endScreen;
    public Button restartButton;
    

    private int score;
    private int lives;
    private int maxScore;


    private String state;




    // Start is called before the first frame update
    void Start()
    {
        restart();
        maxScore = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Game")
        {
            scoreUi.text = $"score: {score}";
            livesUi.text = $"lives: {lives}";


            if(lives == 0)
            {
                endGame();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(ball, new Vector3(-1 + UnityEngine.Random.Range(1f, 6f), 4, 0), new Quaternion(0, 0, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                restart();
            }

        }
        
    }

    
    public void looseLife()
    {
        lives--;
    }
    public void addScore()
    {
        score++;
    }
    public void endGame()
    {
        state = "Over";
        if (score > maxScore)
        {
            maxScore = score;
        }
        finalScoreUi.text = $"final Score: {score}";
        maxScoreUi.text = $"max Score: {maxScore}";

        endScreen.SetActive(true);

    }

    public void restart()
    {
        endScreen.SetActive(false);
        state = "Game";
        score = 0;
        lives = 3;
    }

}

