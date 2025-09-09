using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class RestartButton : SelectableItem
{
    [SerializeField] UiManager uiManager;

    public override void Select()
    {
        uiManager.Restart();
    }
}