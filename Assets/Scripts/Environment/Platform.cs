
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    // Load is 0 when there is no cat on it
    // Increases when a cat goes on it


    private List<Cat> cats;
    private bool occupied = false;
    
    private bool active = false;

    protected bool working = true;

    private CatController controller;

    protected virtual void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<CatController>();
    }

    // public float getLoad()
    // {
    //     return load;
    // }

    public void OnMouseDown()
    {
        //check if the platform has enough space for the cat
        if (IsEmpty() ){
            print("Platform Clicked");
            Cat cat = controller.selectedCat;
            controller.MoveCatToPlatform(GetComponent<Platform>());
        }
    }

    public bool IsEmpty(){
        return (!occupied && active && working);
    }



    public virtual void AddCat(Cat cat)
    {
        // load += cat.getWeight();
        occupied = true;
    }

    public virtual void RemoveCat(Cat cat)
    {
        if (cat != null){
            
            // load -= cat.getWeight();
            occupied = false;
        }
    }

    public void SetActive(bool enabled)
    {
        active = enabled;
    }

}