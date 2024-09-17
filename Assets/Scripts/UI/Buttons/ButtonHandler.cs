using System;

using UnityEngine;
using UnityEngine.EventSystems;

public enum MainMenuButtonType
{
    Resume,
    Restart,
    Exit
}
public abstract class ButtonHandler : MonoBehaviour, IPointerClickHandler
{
    private string _buttonClickSoundPath = "Sounds/ButtonClickSound";

    protected AudioClip _buttonClick;
    protected MainMenuButtonType _buttonType;

    public static event Action<AudioClip, MainMenuButtonType> OnButtonClick;

    private void Start()
    {
        _buttonClick = Resources.Load<AudioClip>(_buttonClickSoundPath);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClick?.Invoke(_buttonClick, _buttonType);
    }
}
