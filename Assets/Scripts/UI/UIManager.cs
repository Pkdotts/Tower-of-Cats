using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool active = true;
    public int levelsPassed = 0;
    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     GameObject child = transform.GetChild(i).gameObject;
            //     if (!child.activeSelf)
            //     {
            //         child.SetActive(true);

            //     }
            // }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && (SceneManager.GetActiveScene().name != "Title"))
        {
            if (pauseMenu.activeSelf)
                Resume();
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        Transition.instance.TransitionIn(SceneManager.GetActiveScene().name);
        Resume();
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }



    public void SetLevelsPassed(int levels)
    {
        levelsPassed = Math.Max(levels, levelsPassed);
    }
    public void ReturnToTitle()
    {
        Transition.instance.TransitionIn("Scenes/Title");
        pauseMenu.SetActive(false);
    }
}