using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

    private void Start()
    {
        UpdateResourceText();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if (_resources.ContainsKey(resourceType))
            _resources[resourceType] += amount;
        else
            _resources[resourceType] = amount;

        UpdateResourceText();
    }

    private void UpdateResourceText()
    {
        _text.text = "Ресурсы:\n";
        foreach (var resource in _resources)
        {
            _text.text += $"{resource.Key.ResourceName}: {resource.Value}\n";
        }
    }
}