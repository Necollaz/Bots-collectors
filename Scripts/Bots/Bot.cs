using UnityEngine;

[RequireComponent(typeof(BotMovement), typeof(BotPicker))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _base;

    private BotMovement _botMovement;
    private BotPicker _botPicker;
    private Resource _resource;
        
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _botMovement = GetComponent<BotMovement>();
        _botPicker = GetComponent<BotPicker>();
        _botPicker.Discovered += OnResourceDiscovered;
    }

    private void Update()
    {
        if (IsBusy && _resource != null && Vector3.Distance(transform.position, _resource.transform.position) < 0.1f)
        {
            CollectResource();
        }
    }

    public void SetTargetResource(Resource resource, Transform baseTransform)
    {
        _base = baseTransform;
        OnResourceDiscovered(resource);
    }

    private void OnResourceDiscovered(Resource resource)
    {
        if (IsBusy || resource.IsCollected) return;

        IsBusy = true;
        _resource = resource;
        _resource.IsCollected = true;
        _botMovement.SetTarget(resource.transform);
    }

    private void CollectResource()
    {
        _resource.Collect();
        _botMovement.SetTarget(_base);
        _resource = null;
    }
}