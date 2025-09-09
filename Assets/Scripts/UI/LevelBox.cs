using TMPro;
using UnityEngine;

public class LevelBox : SelectableItem
{
    public string level;

    [SerializeField] private TextMeshProUGUI label;

    // void Start()
    // {
    //     label = GetComponentInChildren<TextMeshProUGUI>();
    // }

    public void SetText(string text)
    {
        label.text = text;
    }

    public override void Select()
    {
        if (active) {
            Transition.instance.TransitionIn(level);
        }
    }

}