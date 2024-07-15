using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private ResourceScanner _scanner;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _scanner.ResourceFound += OnResourceFound;
    }

    private void OnResourceFound(Resource resource)
    {
        foreach (var bot in _bots)
        {
            if (!bot.IsBusy)
            {
                bot.SetTargetResource(resource, _transform);
                break;
            }
        }
    }
}