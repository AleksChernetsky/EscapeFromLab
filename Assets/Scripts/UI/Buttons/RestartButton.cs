using UnityEngine.EventSystems;

public class RestartButton : ButtonHandler
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        _buttonType = MainMenuButtonType.Restart;
        base.OnPointerClick(eventData);
    }
}