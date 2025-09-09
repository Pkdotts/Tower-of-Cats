using System.Collections;
using UnityEngine;

public class PlatformBreaking : Platform
{
    public bool broken = false;

    private Animator _animator;
    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
    }
    public override void RemoveCat(Cat cat)
    {
        base.RemoveCat(cat);
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        working = false;
        _animator.SetTrigger("Break");

        yield return new WaitForSeconds(0.22f);

        gameObject.SetActive(false);
    }
}
