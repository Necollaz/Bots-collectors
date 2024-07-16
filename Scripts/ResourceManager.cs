using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Dictionary<ResourceType, int> _resourceCounts = new Dictionary<ResourceType, int>();

    private void Start()
    {
        UpdateResourceText();
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if (_resourceCounts.ContainsKey(resourceType))
        {
            _resourceCounts[resourceType] += amount;
        }
        else
        {
            _resourceCounts[resourceType] = amount;
        }
        UpdateResourceText();
    }

    private void UpdateResourceText()
    {
        _text.text = "Resources:\n";
        foreach (var resource in _resourceCounts)
        {
            _text.text += $"{resource.Key}: {resource.Value}\n";
        }
    }
}