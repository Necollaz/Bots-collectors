using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.OnResourceChanged += UpdateResourceText;
    }

    private void OnDisable()
    {
        _base.OnResourceChanged -= UpdateResourceText;
    }

    private void Start()
    {
        UpdateResourceText();
    }

    private void UpdateResourceText()
    {
        var resources = _base.GetResources();

        _text.text = "Ресурсы:\n";

        foreach (var resource in resources)
        {
            _text.text += $"{resource.Key.ResourceName}: {resource.Value}\n";
        }
    }
}