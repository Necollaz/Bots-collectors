using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private float _delay;
    [SerializeField] private float _radius;
    [SerializeField] private ResourcePool _pool;

    private int _minValueResources = 1;
    private int _maxValueResources = 10;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _amount; i++)
        {
            ResourceType resourceType = _pool.GetRandomResourceType();
            Resource resource = _pool.Get(resourceType);
            resource.OnCollected += HandleResourceCollected;
            resource.transform.rotation = GetRandomRotation();
            resource.transform.position = GetRandomPosition();
            resource.Set(resourceType, Random.Range(_minValueResources, _maxValueResources));
            yield return new WaitForSeconds(_delay);
        }
    }

    private void HandleResourceCollected(Resource resource)
    {
        resource.OnCollected -= HandleResourceCollected;
        _pool.Release(resource);
    }

    private Vector3 GetRandomPosition()
    {
        float minYPosition = 0;
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-_radius, _radius), 0, Random.Range(-_radius, _radius));
        randomPosition.y = Mathf.Max(randomPosition.y, minYPosition);
        return randomPosition;
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
