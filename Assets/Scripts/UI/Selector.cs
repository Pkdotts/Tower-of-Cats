using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    private SelectableItem[] items;

    [SerializeField] private Vector2 padding = new Vector2(6, 6);
    [SerializeField] private Vector2 animSize;
    public bool active = false;
    private int index = 0;

    private float time;
    private const float INTERVAL = 0.2f;

    InputAction moveAction;
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        items = grid.GetComponentsInChildren<SelectableItem>();
    }

    private void Update()
    {
        if (active)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if (moveValue != Vector2.zero)
            {

                if (time >= INTERVAL)
                {
                    time = 0;

                    index += moveValue.x.ConvertTo<int>();
                    index -= moveValue.y.ConvertTo<int>() * grid.constraintCount;

                    // Loop Back
                    if (index >= items.Length)
                    {
                        index = 0;
                    }
                    if (index < 0)
                    {
                        index = items.Length - 1;
                    }
                    print("Selector Index : " + index);
                }
                time += Time.deltaTime;
                
            }
            else if (time != INTERVAL)
            {
                time = INTERVAL;
            }

            // Select box
            if (Input.GetKeyDown(KeyCode.Space) && items[index].active)
            {
                print("select");
                items[index].Select();
            }
        }
        transform.position = items[index].transform.position;

        RectTransform itemRect = items[index].GetComponent<RectTransform>();
        Vector2 newSize = new Vector2(itemRect.rect.width, itemRect.rect.height);

        newSize += animSize + padding;

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);


    }


}