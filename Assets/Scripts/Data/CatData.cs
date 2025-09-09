using UnityEngine;


// Universal Cat Data
[CreateAssetMenu(menuName = "Cat Data")]
public class CatData : ScriptableObject 
{
    public enum Type {REGULAR, FAT}
    public Type type = Type.REGULAR;
    public float weight = 3;
    public float jumpTime = 0.25f;
    
    public int spaceOccupied = 3;
    [HideInInspector] public float moveSize = 1f *2;
    [HideInInspector] public float startCheckSize = 0.25f * 2;
}