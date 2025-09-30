using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatController : MonoBehaviour
{

    [SerializeField] GameObject catParent;
    [SerializeField] List<Cat> cats;
    [SerializeField] BalancePole balancePole;
    [SerializeField] PlatformParent platformParent;
    [SerializeField] string nextLevel;
    [SerializeField] int currentLevel = 1;
    public Cat selectedCat = null;
    private int catIndex = 0;

    private void Start()
    {
        cats = catParent.GetComponentsInChildren<Cat>().ToList();
        SetSelectedCat(catIndex);
        UpdatePoleAngle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwapCatForward();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapCatBackward();
        }
    }

    public bool IsDead()
    {
        return balancePole.IsDead();
    }
    public void MoveCatToPlatform(Platform platform)
    {
        if (selectedCat != null)
        {
            print("moving cat to platform from controller");
            selectedCat.MoveTo(platform);

        }
    }

    public void UpdatePoleAngle(){
        balancePole.UpdateAngle(GetFullLoad());

        foreach (Cat cat in cats) {
            cat.SetPanic(GetFullLoad());
        }

        if (!IsDead() && platformParent.GetWinning() && nextLevel != null) {
            UiManager.instance.SetLevelsPassed(currentLevel);
            Transition.instance.TransitionIn(nextLevel);
        }
    }

    public void SetSelectedCat(Cat cat)
    {
        if (selectedCat != cat)
        {
            selectedCat?.SetSelected(false);
        }
        selectedCat = cat;
        selectedCat.SetSelected(true);
        catIndex = cats.IndexOf(cat);
    }

    public void SetSelectedCat(int index)
    {
        SetSelectedCat(cats[index]);
    }

    public void UnselectCat()
    {
        selectedCat?.SetSelected(false);
        selectedCat = null;
    }


    public void SwapCatForward()
    {
        catIndex += 1;
        if (catIndex >= cats.Count)
        {
            catIndex = 0;
        }
        print("Cat Index " + catIndex);
        SetSelectedCat(catIndex);
    }

    public void SwapCatBackward()
    {
        catIndex -= 1;
        if (catIndex < 0)
        {
            catIndex = cats.Count - 1;
        }
        print("Cat Index " + catIndex);
        SetSelectedCat(catIndex);
    }

    public float GetFullLoad()
    {

        float fullLoad = 0;

        foreach (Cat cat in cats)
        {
            float distance = -(cat.transform.localPosition.x + catParent.transform.localPosition.x);
            fullLoad += cat.GetWeight() * distance;
        }

        print("Full Load: " + fullLoad);

        return fullLoad;
    }
}
