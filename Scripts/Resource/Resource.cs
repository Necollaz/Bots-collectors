using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceMaterial;
    [SerializeField] private int _amount = 1;

    public bool IsCollected = false;
    public event Action<Resource> OnCollected;

    public void Set(ResourceType resourceMaterial, int amount)
    {
        _resourceMaterial = resourceMaterial;
        _amount = amount;
    }

    public ResourceType GetResourceType()
    {
        return _resourceMaterial;
    }

    public void Collect()
    {
        IsCollected = true;
        OnCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        IsCollected = false;
        gameObject.SetActive(true);
    }
}