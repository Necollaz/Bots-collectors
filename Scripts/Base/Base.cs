using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;

    private ResourceScanner _scanner;
    private Dictionary<ResourceType, int> _resources;
    private HashSet<Resource> _reservedResources;

    public event Action OnResourceChanged;

    private void Awake()
    {
        _resources = new Dictionary<ResourceType, int>();
        _reservedResources = new HashSet<Resource>();
        _scanner = GetComponent<ResourceScanner>();
        _scanner.ResourceFound += Found;
    }

    public Dictionary<ResourceType, int> GetResources() => new Dictionary<ResourceType, int>(_resources);

    public void ReleaseResource(Resource resource)
    {
        _reservedResources.Remove(resource);
    }

    public void TryAssign(Bot bot)
    {
        foreach (var resource in _scanner.GetAvailableResources())
        {
            if (!_reservedResources.Contains(resource))
            {
                Assign(bot, resource);
                break;
            }
        }
    }

    public void Add(ResourceType resourceType, int amount)
    {
        if (_resources.ContainsKey(resourceType))
            _resources[resourceType] += amount;
        else
            _resources[resourceType] = amount;

        OnResourceChanged?.Invoke();
    }

    private void Found(Resource resource)
    {
        if (_reservedResources.Contains(resource)) return;

        foreach (var bot in _bots)
        {
            if (!bot.IsBusy)
            {
                bot.SetTarget(resource);
                _reservedResources.Add(resource);
                break;
            }
        }
    }

    private void Assign(Bot bot, Resource resource)
    {
        bot.SetTarget(resource);
        _reservedResources.Add(resource);
    }
}