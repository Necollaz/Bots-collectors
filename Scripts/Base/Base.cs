using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;

    private ResourceScanner _scanner;
    private Dictionary<ResourceType, int> _resources;

    public event Action OnResourceChanged;

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();
    }

    private void Start()
    {
        _scanner.ResourceFound += Found;
    }

    public Dictionary<ResourceType, int> GetResources() => new Dictionary<ResourceType, int>(_resources);

    public void Add(ResourceType resourceType, int amount)
    {
        if (_resources.ContainsKey(resourceType))
            _resources[resourceType] += amount;
        else
            _resources[resourceType] = amount;

        OnResourceChanged?.Invoke();
        Debug.Log($"Ресурс {resourceType} добавлен. Новая сумма: {_resources[resourceType]}");

    }

    private void Found(Resource resource)
    {
        foreach (var bot in _bots)
        {
            if (!bot.IsBusy)
            {
                bot.SetTarget(resource);
                break;
            }
        }
    }
}