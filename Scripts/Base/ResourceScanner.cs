using System;
using System.Collections;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _delay;
    [SerializeField] private ParticleSystem _particleEffect;

    public event Action<Resource> ResourceFound;

    private void Start()
    {
        StartCoroutine(Scan());
    }

    private IEnumerator Scan()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (Collider hit in hits)
            {
                if(hit.TryGetComponent(out Resource resource))
                {
                    ResourceFound?.Invoke(resource);
                    _particleEffect.Play();
                    break;
                }
            }

            yield return wait;
        }
    }
}