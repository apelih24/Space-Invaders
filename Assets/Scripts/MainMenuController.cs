using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton;

    private void Awake()
    {
        m_PlayButton.onClick.AddListener(OnPlayButton);
    }

    private void OnPlayButton()
    {
        m_PlayButton.interactable = false;
        SceneManager.LoadSceneAsync("Game");
    }
}