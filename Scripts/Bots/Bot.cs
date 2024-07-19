using UnityEngine;

[RequireComponent(typeof(BotMovement), typeof(BotPicker))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private ResourcePool _resourcePool;

    private BotMovement _botMovement;
    private BotPicker _botPicker;
    private Resource _resource;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _botMovement = GetComponent<BotMovement>();
        _botPicker = GetComponent<BotPicker>();
        _botPicker.Discovered += SetTarget;
    }

    private void Update()
    {
        PickUpResource();
    }

    public void SetTarget(Resource resource)
    {
        if (!IsBusy && resource != null)
        {
            IsBusy = true;
            _resource = resource;
            _botMovement.SetTarget(resource.transform);
            Debug.Log($"Бот {name} установил целью ресурс {resource.name}");
        }
    }

    private void PickUpResource()
    {
        if (IsBusy && _resource != null && Vector3.Distance(transform.position, _resource.transform.position) < 0.1f)
        {
            _resource.transform.SetParent(transform);
            _resource.transform.localPosition = Vector3.zero;
            _botMovement.SetTarget(_base.transform);
            _botMovement.OnReachTarget += ReturnBase;
            Debug.Log($"Бот {name} подобрал ресурс {_resource.name} и направился на базу");

        }
    }

    private void ReturnBase()
    {
        if (_resource != null)
        {
            Debug.Log($"Бот {name} возвращается на базу с ресурсом {_resource.name}");

            _base.Add(_resource.GetResourceType(), _resource.GetAmount());
            IsBusy = false;
            _resourcePool.Return(_resource);
            _resource.gameObject.SetActive(false);
            _botMovement.OnReachTarget -= ReturnBase;
            Debug.Log($"Бот {name} доставил ресурс бесплатно.");

        }
    }
}