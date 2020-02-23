using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ParticleSystem))]
public class PumpkinController : MonoBehaviour
{

    // Cached references.
    private BoxCollider _collider;
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Eat()
    {
        // Disable the collider so the pumpkin can not be collected twice.
        _collider.enabled = false;
        Destroy(gameObject, _particleSystem.main.duration);
        _particleSystem.Play();
    }
}
