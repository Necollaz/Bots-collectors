using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceManager), typeof(ResourceScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;

    private ResourceScanner _scanner;
    private ResourceManager _resourceManager;

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();
        _resourceManager = GetComponent<ResourceManager>();
    }

    private void Start()
    {
        _scanner.ResourceFound += OnResourceFound;
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        _resourceManager.AddResource(resourceType, amount);
    }

    private void OnResourceFound(Resource resource)
    {
        foreach (var bot in _bots)
        {
            if (!bot.IsBusy)
            {
                bot.SetTargetResource(resource);
                break;
            }
        }
    }
}