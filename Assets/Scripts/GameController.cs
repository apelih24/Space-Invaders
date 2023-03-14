using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int PauseSeconds = 3;
    [SerializeField] private EnemiesGroupController m_EnemiesGroup;
    [SerializeField] private GameObject m_Field;
    [SerializeField] private TMP_Text m_TimerText;
    [SerializeField] private PlayerController m_PlayerController;
    [SerializeField] private ScoreController m_ScoreController;
    [SerializeField] private EndGamePopup m_EndGamePopup;

    private void Awake()
    {
        m_PlayerController.OnGameOver += OnGameOver;
        m_PlayerController.OnTakeDamage += OnPlayerTakeDamage;
        m_EnemiesGroup.OnEnemyDeath += OnEnemyDeath;
        m_EnemiesGroup.OnAllEnemiesDied += OnGameOver;
        m_EndGamePopup.OnRestart += OnRestartButton;
        StartGame();
    }

    private void OnPlayerTakeDamage()
    {
        m_PlayerController.Pause();
        m_EnemiesGroup.Pause();
        StartCoroutine(StartGameCoroutine());
    }

    private void OnEnemyDeath(int score)
    {
        m_ScoreController.AddScore(score);
    }

    private void OnRestartButton()
    {
        m_PlayerController.Restart();
        m_EnemiesGroup.Restart();
        m_ScoreController.Restart();
        StartCoroutine(StartGameCoroutine());
    }

    private void OnGameOver()
    {
        m_EndGamePopup.Show();
        m_PlayerController.Pause();
        m_EnemiesGroup.Pause();
    }

    private void StartGame()
    {
        m_Field.gameObject.SetActive(true);
        m_EnemiesGroup.CreateEnemies();
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        var waitForSecond = new WaitForSeconds(1);
        var currentTimer = PauseSeconds;
        m_TimerText.gameObject.SetActive(true);
        m_TimerText.SetText(currentTimer.ToString());
        while (currentTimer > 0)
        {
            yield return waitForSecond;
            currentTimer--;
            m_TimerText.SetText(currentTimer.ToString());
        }

        m_TimerText.gameObject.SetActive(false);
        m_EnemiesGroup.UnPause();
        m_PlayerController.UnPause();
    }
}