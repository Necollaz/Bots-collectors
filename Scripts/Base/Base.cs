using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceUI), typeof(ResourceScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;

    private ResourceScanner _scanner;
    private ResourceUI _resourceUI;

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();
        _resourceUI = GetComponent<ResourceUI>();
    }

    private void Start()
    {
        _scanner.ResourceFound += OnResourceFound;
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        _resourceUI.AddResource(resourceType, amount);
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