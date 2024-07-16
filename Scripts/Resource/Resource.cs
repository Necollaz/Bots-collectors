using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _amount = 1;

    public bool IsCollected = false;

    public event Action<Resource> OnCollected;

    public void Set(ResourceType resourceMaterial, int amount)
    {
        _resourceType = resourceMaterial;
        _amount = amount;
    }

    public ResourceType GetResourceType()
    {
        return _resourceType;
    }

    public void Collect()
    {
        IsCollected = true;
        OnCollected?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        _amount = 0;
        IsCollected = false;
        gameObject.SetActive(true);
    }

    public int GetAmount()
    {
        return _amount;
    }
}