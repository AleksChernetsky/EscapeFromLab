using UnityEngine;
using UnityEngine.SceneManagement;

public class VerySimpleUIHandler : MonoBehaviour
{
    private readonly string MainScene = "MainScene";

    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _looseCanvas;

    [SerializeField] private Texture2D _cursourSprite;

    private bool _isGameOver;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        CursorStartState();

        PlayerInputService.OnEscapeInput += Pause;
        QuestHandler.instance.OnEndGameEvent += EndGameState;
        ButtonHandler.OnButtonClick += OnButtonClickAction;
    }
    private void OnDisable()
    {
        PlayerInputService.OnEscapeInput -= Pause;
        QuestHandler.instance.OnEndGameEvent -= EndGameState;
        ButtonHandler.OnButtonClick -= OnButtonClickAction;
    }
    private void CursorStartState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.SetCursor(_cursourSprite, Vector2.zero, CursorMode.Auto);
    }
    private void PlayButtonClickSound(AudioClip audioClip) => _audioSource.PlayOneShot(audioClip);
    private void OnButtonClickAction(AudioClip audioClip, MainMenuButtonType buttonType)
    {
        switch (buttonType)
        {
            case MainMenuButtonType.Resume:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _mainMenuCanvas.SetActive(false);
                Time.timeScale = 1f;
                break;
            case MainMenuButtonType.Restart:
                _isGameOver = false;
                SceneManager.LoadScene(MainScene);
                break;
            case MainMenuButtonType.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
        PlayButtonClickSound(audioClip);
    }
    public void EndGameState(bool isWin)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isGameOver = true;

        if (isWin)
        {
            _winCanvas.SetActive(true);
        }
        else
        {
            _looseCanvas.SetActive(true);
        }
    }
    public void Pause()
    {
        if (_isGameOver)
            return;

        _mainMenuCanvas.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
