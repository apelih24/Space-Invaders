using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_ReturnButton;

    private void Awake()
    {
        m_RestartButton.onClick.AddListener(OnRestartButton);
        m_ReturnButton.onClick.AddListener(OnReturnButton);
    }

    private void OnReturnButton()
    {
        DisableButtons();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void DisableButtons()
    {
        m_ReturnButton.interactable = false;
        m_ReturnButton.interactable = false;
    }

    public event Action OnRestart;

    private void OnRestartButton()
    {
        Hide();
        OnRestart?.Invoke();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}