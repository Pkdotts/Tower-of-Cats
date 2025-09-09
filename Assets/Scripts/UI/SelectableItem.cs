using TMPro;
using UnityEngine;

public abstract class SelectableItem : MonoBehaviour
{
    public bool active = true;
    abstract public void Select();
}