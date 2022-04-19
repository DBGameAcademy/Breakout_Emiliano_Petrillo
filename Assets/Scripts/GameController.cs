using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int Score = 0;

    public UIController UIController;

    public Ball Ball;

    public Vector3 BallResetPosition;

    public int Lives = 3;

    private bool isPlaying = false;
    private bool isPaused = false;

    public void Start()
    {
        UIController.UpdateScoreText(Score);
        UIController.UpdateLives(Lives);

        PauseGame();
    }

    private void Update()
    {
        if(!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if(isPaused && Input.anyKeyDown)
        {
            UnpauseGame();
        }
    }

    public void AddScore(int _value)
    {
        Score += _value;

        UIController.UpdateScoreText(Score);
    }

    public void BallLost()
    {
        Ball.transform.position = BallResetPosition;

        Vector3 currentVelocity = Ball.Velocity;
        currentVelocity.y = Mathf.Abs(currentVelocity.y);
        Ball.Velocity = currentVelocity;

        Lives--;
        UIController.UpdateLives(Lives);
        if (Lives < 0)
        {
            Debug.Log("game lost");
            GameOver();
        }
    }

    void GameOver()
    {
        UIController.ShowGameOver();
        isPlaying = false;
        PauseGame();
    }

    public void StartGame()
    {
        isPlaying = true;
        ResetGame();
        UnpauseGame();
    }

    public void ResetGame()
    {
        Lives = 3;
        Score = 0;
        UIController.UpdateScoreText(Score);
        UIController.UpdateLives(Lives);
        UIController.HideGameOver();
        UIController.HideStartGamePanel();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        if (isPlaying)
        {
            UIController.ShowPausePanel();
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;
        isPlaying = true;
        Time.timeScale = 1;
        UIController.HidePausePanel();
        UIController.HideStartGamePanel();
    }

    public void QuitGame()
    {
        isPlaying = false;
        PauseGame();
        UIController.HideGameOver();
        UIController.HidePausePanel();
        UIController.ShowStartGamePanel();

        Ball.transform.position = BallResetPosition;
    }
}
