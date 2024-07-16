using System;
using System.Collections;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _delay;

    public event Action<Resource> ResourceFound;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (Collider hit in hits)
            {
                if(hit.TryGetComponent(out Resource resource) && !resource.IsCollected)
                {
                    ResourceFound?.Invoke(resource);
                    break;
                }
            }

            yield return new WaitForSeconds(_delay);
        }
    }
}