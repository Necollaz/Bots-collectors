using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _uiElements;
    [SerializeField] private ResourceType[] _resourceTypes;

    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddResource(ResourceType type, int amount)
    {
        if (_resources.ContainsKey(type))
        {
            _resources[type] += amount;
            UpdateUI();
        }
    }

    private void Initialize()
    {
        foreach (ResourceType type in _resourceTypes)
        {
            _resources[type] = 0;
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _resourceTypes.Length; i++)
        {
            ResourceType type = _resourceTypes[i];

            if (i < _uiElements.Length)
            {
                _uiElements[i].text = $"{type.ResourceName}: {_resources[type]}";
            }
        }
    }
}