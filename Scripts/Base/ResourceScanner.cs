using System;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;

    public event Action<Resource> ResourceFound;

    private void Update()
    {
        Resource resource = ScanForResources();

        if (resource != null)
            ResourceFound?.Invoke(resource);
    }

    public Resource ScanForResources()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource) && !resource.IsCollected)
            {
                return resource;
            }
        }
        return null;
    }
}