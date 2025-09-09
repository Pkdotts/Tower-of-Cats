using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private Vector3 direction = Vector3.left;
    
    void Update()
    {
        transform.position += direction * _speed * Time.deltaTime;
    }
}
