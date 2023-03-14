using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private const string ScoreFormat = "Score: {0}";
    [SerializeField] private TMP_Text m_Score;
    private int m_CurrentScore;

    public void AddScore(int score)
    {
        m_CurrentScore += score;
        UpdateText();
    }


    public void Restart()
    {
        m_CurrentScore = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        m_Score.SetText(string.Format(ScoreFormat, m_CurrentScore.ToString()));
    }
}