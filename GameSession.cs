using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    private void Awake()
    {
        SetUpSing();
    }

    private void SetUpSing()
    {
        int numGameSesh = FindObjectsOfType<GameSession>().Length;
        if (numGameSesh > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreVal)
    {
        score += scoreVal;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
