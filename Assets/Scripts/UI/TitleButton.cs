using UnityEngine;

public class TitleButton : SelectableItem
{
    [SerializeField] UiManager uiManager;

    public override void Select()
    {
        if (active) {
            uiManager.ReturnToTitle();
        }
    }
}