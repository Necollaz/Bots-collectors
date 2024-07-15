using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private ResourceType[] _resourceTypes;
    [SerializeField] private int _poolSize;

    private Dictionary<ResourceType, Queue<Resource>> _pools;

    private void Awake()
    {
        InitializePools();
    }

    public Resource Get(ResourceType resourceType)
    {
        if (_pools.TryGetValue(resourceType, out Queue<Resource> pool) && pool.Count > 0)
        {
            Resource resource = pool.Dequeue();
            resource.gameObject.SetActive(true);
            return resource;
        }
        else
        {
            Resource resource = CreateInstance(resourceType, transform);
            return resource;
        }
    }

    public ResourceType GetRandomResourceType()
    {
        return _resourceTypes[Random.Range(0, _resourceTypes.Length)];
    }

    public void Release(Resource resource)
    {
        resource.Reset();
        _pools[resource.GetResourceType()].Enqueue(resource);
    }

    private void InitializePools()
    {
        _pools = new Dictionary<ResourceType, Queue<Resource>>();

        foreach (var resourceType in _resourceTypes)
        {
            Queue<Resource> pool = new Queue<Resource>();

            for (int i = 0; i < _poolSize; i++)
            {
                Resource resource = CreateInstance(resourceType, transform);
                resource.gameObject.SetActive(false);
                pool.Enqueue(resource);
            }
            _pools.Add(resourceType, pool);
        }
    }

    private Resource CreateInstance(ResourceType resourceMaterial, Transform parent)
    {
        GameObject instance = Instantiate(resourceMaterial.Prefab, parent);
        Resource resource = instance.GetComponent<Resource>();
        resource.Set(resourceMaterial, 0);
        resource.transform.SetParent(parent);
        return resource;
    }
}