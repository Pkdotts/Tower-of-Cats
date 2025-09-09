using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _camera;

    private const float TRANSITIONTIME = 1.0f;

    private bool levelSelect = false;
    private bool active = true;

    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !levelSelect)
                StartCoroutine(ShowLevelsCoroutine());
            else if (Input.GetKeyDown(KeyCode.LeftShift) && levelSelect)
                StartCoroutine(ShowTitleCoroutine());

        }
    }

    IEnumerator ShowLevelsCoroutine()
    {
        active = false;
        _animator.SetTrigger("ShowLevels");

        yield return new WaitForSeconds(TRANSITIONTIME);
        active = true;
        GetComponentInChildren<Selector>().active = true;
        levelSelect = true;
    }

    IEnumerator ShowTitleCoroutine()
    {
        active = false;
        GetComponentInChildren<Selector>().active = false;
        _animator.SetTrigger("ShowTitle");
        yield return new WaitForSeconds(TRANSITIONTIME);
        active = true;
        levelSelect = false;
    }
}