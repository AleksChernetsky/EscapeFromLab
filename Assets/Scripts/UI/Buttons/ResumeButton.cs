using UnityEngine.EventSystems;

public class ResumeButton : ButtonHandler
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        _buttonType = MainMenuButtonType.Resume;
        base.OnPointerClick(eventData);
    }
}
