using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float _timeInterval = 3f;
    [SerializeField] private float _lifespan = 10f;
    [SerializeField] private Vector2 _spawnRange = new Vector2();
    [SerializeField] private GameObject _object;
    
    private float _timer;

    private void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        if (_timer > _timeInterval)
        {
            SpawnObject();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _spawnRange);
    }
    private void SpawnObject()
    {
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-_spawnRange.x/2, _spawnRange.x/2), Random.Range(-_spawnRange.y/2, _spawnRange.y/2));
        GameObject obj = Instantiate(_object, spawnPos, Quaternion.identity);

        Destroy(obj, _lifespan);
    }

}
