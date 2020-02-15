using UnityEngine;

public class PumpkinController : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Eat()
    {
        Destroy(gameObject, _particleSystem.main.duration);
        _particleSystem.Play();
    }
}
