using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text textScore;
    GameSession gameSession;
    private void Start()
    {
        textScore = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }
    private void Update()
    {
        textScore.text = gameSession.GetScore().ToString();
    }
}
