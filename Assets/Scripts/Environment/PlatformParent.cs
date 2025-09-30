using UnityEngine;

public class PlatformParent : MonoBehaviour {

    // Gets the Load of every child platform
    
    // public float getFullLoad(){

    //     float fullLoad = 0;

    //     Platform[] platforms = GetComponentsInChildren<Platform>();
    //     foreach (Platform platform in platforms) {
    //         float distance = transform.localPosition.x - platform.transform.localPosition.x;
    //         fullLoad += platform.getLoad() * distance;
    //     }

    //     print(fullLoad);

    //     return fullLoad;
    // }

    public bool GetWinning(){

        bool winning = true;

        PlatformBed[] beds = GetComponentsInChildren<PlatformBed>();
        foreach (PlatformBed bed in beds) {
            if (!bed.GetHasCat())
            {
                winning = false;
            }
        }

        return winning;
    }

}