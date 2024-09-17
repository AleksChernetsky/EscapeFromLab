using UnityEngine.EventSystems;

public class ExitButton : ButtonHandler
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        _buttonType = MainMenuButtonType.Exit;
        base.OnPointerClick(eventData);
    }
}
