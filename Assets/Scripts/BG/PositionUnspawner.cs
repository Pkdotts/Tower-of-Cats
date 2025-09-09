using UnityEngine;

[System.Serializable]
public class PositionUnspawner : MonoBehaviour
{
    [SerializeField] private bool _smallerThan;
    [SerializeField] private float _despawnPositionX;

    private void Update()
    {
        if (transform.position.x < _despawnPositionX && _smallerThan)
        {
            Destroy(this);
        }

        if (transform.position.x > _despawnPositionX && !_smallerThan)
        {
            Destroy(this);
        }
    }


}