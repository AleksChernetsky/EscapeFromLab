using System;

using TMPro;

using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keysCountText;
    private int _keysLeft;
    private int _keysOnColumn;
    private int _columnsCount;

    private bool _gameOver;

    public static QuestHandler instance;

    public event Action OnKeysReceived;
    public event Action<bool> OnEndGameEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // register and check keys collect
    public void RegisterNewKey()
    {
        _keysLeft++;
        _columnsCount = _keysLeft;
        SetKeysCountText();
    }
    public void OnKeyCollect()
    {
        _keysLeft--;
        SetKeysCountText();
    }

    // check keys on column
    public void OnColumnReceiveKey()
    {
        _keysOnColumn++;
        if (_keysOnColumn == _columnsCount)
        {
            OnKeysReceived?.Invoke();
        }
    }
    private void SetKeysCountText()
    {
        if (_keysLeft != 0 && _keysLeft % 10 != 1)
        {
            _keysCountText.text = $"{_keysLeft} keys left";
        }
        else if (_keysLeft % 10 == 1)
        {
            _keysCountText.text = $"{_keysLeft} key left";
        }
        else
        {
            _keysCountText.text = "All keys collected";
        }
    }

    // end game event
    public void OnEndGame(bool win)
    {
        if (!_gameOver)
        {
            _gameOver = true;
            OnEndGameEvent?.Invoke(win);
            GlobalSoundHandler.instance.OnWinSound();
        }
    }
}
