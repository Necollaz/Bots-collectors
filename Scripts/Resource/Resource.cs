using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _minAmount = 1;
    [SerializeField] private int _maxAmount = 10;

    private int _amount;
    public bool IsCollected = false;

    public event Action<Resource> OnCollected;

    public void Initialize()
    {
        _amount = Random.Range(_minAmount, _maxAmount);
        IsCollected = false;
        gameObject.SetActive(true);
    }

    public void Set(ResourceType resourceType)
    {
        _resourceType = resourceType;
        _amount = Random.Range(_minAmount, _maxAmount);
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