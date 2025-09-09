using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public static Transition instance;
    private Animator _animator;
    private float _transitionInTime = 1/3f;
    private float _transitionOutTime = 5/12f;


    private bool transitioning = false; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            _animator = GetComponent<Animator>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    
    

    public void TransitionIn(string sceneName)
    {
        if (!transitioning)
        {
            print("Transitioning to: " + sceneName);
            transitioning = true;
            StartCoroutine(TransitionInCoroutine(sceneName));
        }
    }

    IEnumerator TransitionInCoroutine(string sceneName)
    {
        _animator.SetTrigger("TransitionIn");

        yield return new WaitForSeconds(_transitionInTime);

        SceneManager.LoadScene(sceneName);

        _animator.SetTrigger("TransitionOut");

        yield return new WaitForSeconds(_transitionOutTime);

        transitioning = false;

    }
}