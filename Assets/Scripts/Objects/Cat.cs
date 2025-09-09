using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : WeightyObject
{
    [SerializeField] public CatData Data;
    [SerializeField] private LayerMask _platformLayer;

    private List<Collider> TriggerList;
    private Platform currentPlatform;
    private CatController controller;

    private Cat cat;
    public Animator animator;
    private bool selected;
    private bool jumping = false;

    InputAction moveAction;

    private AudioSource _sound;


    private void Start()
    {
        cat = GetComponent<Cat>();
        _sound = GetComponent<AudioSource>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<CatController>();
        SetCurrentPlatform(GetPlatformInDirection(Vector2.zero));
        moveAction = InputSystem.actions.FindAction("Move");
        print(moveAction);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && selected && !controller.IsDead()) {
            
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if (moveValue != Vector2.zero){
                Platform nextPlatform = GetPlatformInDirection(moveValue);
                
                if (nextPlatform != null && nextPlatform.IsEmpty()){
                    MoveTo(nextPlatform);
                }
            }   
        }

    }

    public float GetWeight(){
        return Data.weight;
    }

    public float GetSpaceOccupied(){
        return Data.spaceOccupied;
    }

    public void SetSelected(bool enabled){
        selected = enabled;
        animator.SetBool("selected", selected);
        if (selected)
        {
            ActivateSurroundingPlatforms();
        } else 
        {
            DeactivateSurroundingPlatforms();
        }
    }

    public void MoveTo(Platform platform)
    {
        DeactivateSurroundingPlatforms();
        if (!jumping)
            StartCoroutine(JumpCoroutine(platform, Data.jumpTime));
    }
    public IEnumerator JumpCoroutine(Platform platform, float time){
        // Remove self from the current platform
        print("move");
        _sound.Play();
        float jumpHeight = Math.Max(platform.transform.localPosition.y, transform.localPosition.y);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(jumpHeight + 1f , time/2).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOLocalMoveY(platform.transform.localPosition.y, time/2).SetEase(Ease.InQuad));
        sequence.Insert(0, transform.DOLocalMoveX(platform.transform.localPosition.x, time).SetEase(Ease.Linear));

        
        SetJumping(true);
        yield return sequence.WaitForCompletion();
        currentPlatform?.RemoveCat(cat);
        print("finished movement");

        SetCurrentPlatform(platform);
        SetJumping(false);
        controller.UpdatePoleAngle();
        ActivateSurroundingPlatforms();
        // controller.unselectCat();
    }

    private void SetCurrentPlatform(Platform platform)
    {
        currentPlatform = platform;
        currentPlatform.AddCat(cat);
    }
    

    void OnDrawGizmos()
    {
        Vector3 moveSize = new Vector3(Data.moveSize, Data.moveSize, Data.moveSize);
        Gizmos.DrawWireCube(transform.position, moveSize);

        Vector3 startCheckSize = new Vector3(Data.startCheckSize, Data.startCheckSize, Data.startCheckSize);
        Gizmos.DrawWireCube(transform.position, startCheckSize);
    }


    // public void OnMouseDown()
    // {
    //     if (!selected){
    //         print("Clicked");
    //         SetSelected(true);
    //     } else {
    //         controller.UnselectCat();
    //     }
    // }

    private List<Platform> surroundingPlatforms = new List<Platform>();
    // Activate all surrounding platforms


    private Platform GetPlatformInDirection(Vector2 direction)
    {
        RaycastHit2D hit;
        Collider2D collider;
        Vector2 size = new Vector3(0.1f, 0.1f);
        Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
        collider = Physics2D.OverlapBox(transform.position + direction3, size, 0, _platformLayer);

        if (collider == null && direction.y == 0 && direction.x != 0)
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(direction.x, 1, 0), transform.TransformDirection(Vector3.down), 2);
            collider = hit.collider;
        }
        
        return collider.gameObject.GetComponent<Platform>();
    }

    private void ActivateSurroundingPlatforms()
    {
        print("Activating platforms");
        Vector2 size = new Vector3(Data.moveSize, Data.moveSize);
        foreach (Collider2D collider in Physics2D.OverlapBoxAll(transform.position, size, 0, _platformLayer))
        {
            collider.gameObject.GetComponent<Platform>()?.SetActive(true);
            surroundingPlatforms.Add(collider.gameObject.GetComponent<Platform>());
        }
    }

    private void DeactivateSurroundingPlatforms()
    {
        if (surroundingPlatforms.Count() != 0)
        {
            foreach (Platform platform in surroundingPlatforms)
            {
                platform.SetActive(false);
            }
            surroundingPlatforms.Clear();
        }
    }

    public void SetJumping(bool enabled){
        jumping = enabled;
        animator.SetBool("jumping", jumping);
    }

    public void SetPanic(float level){
        animator.SetFloat("panic", Math.Abs(level));
    }
}