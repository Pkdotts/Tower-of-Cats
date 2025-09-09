using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private string[] levels;

    public bool isFinished = false;
    private void Awake()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            print("level: " + i.ToString());
            LevelBox newBox = Instantiate(_box).GetComponent<LevelBox>();
            newBox.transform.parent = transform;
            newBox.transform.localScale = Vector3.one;
            if (i > UiManager.instance.levelsPassed)
            {
                newBox.active = false;
                newBox.gameObject.SetActive(false);
                print("set active false");
            }

            newBox.SetText((i + 1).ToString());
            newBox.level = levels[i];
        }
        isFinished = true;
    }
}
