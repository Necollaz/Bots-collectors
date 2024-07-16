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
        _botPicker.Discovered += SetTargetResource;
    }

    private void Update()
    {
        ReturnBase();
    }

    public void SetTargetResource(Resource resource)
    {
        if (IsBusy || resource.IsCollected) return;

        IsBusy = true;
        _resource = resource;
        _resource.IsCollected = true;
        _botMovement.SetTarget(resource.transform);
    }

    private void ReturnBase()
    {
        if (IsBusy && _resource != null && Vector3.Distance(transform.position, _resource.transform.position) < 0.1f)
        {
            _resource.Collect();

            _botMovement.SetTarget(_base);
            _botMovement.OnReachTarget += OnBaseReached;
        }
    }

    private void OnBaseReached()
    {
        if(_base.TryGetComponent(out Base baseComponent))
        {
            baseComponent.AddResources(_resource.GetResourceType(), _resource.GetAmount());
        }

        IsBusy = false;
        _botMovement.OnReachTarget -= OnBaseReached;
    }
}