using UnityEngine;

public class ResumeButton : SelectableItem
{
    [SerializeField] UiManager uiManager;

    public override void Select()
    {
        uiManager.Resume();
    }
}