using UnityEngine;

public class DetectionSight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public float rotationSpeed = 30f;
    
    // Cached references
    private GameObject _player;
    private SphereCollider _collider;
    private AudioSource _audioSource;

    private float _howFarCanISee;
    private int _onlyLayerToRaycast = 1 << 10; // Player layer (10)
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _collider = GetComponent<SphereCollider>();
        _audioSource = GetComponent<AudioSource>();

        _howFarCanISee = _collider.radius;
    }

    // Update is called once per frame
    void Update()
    {
       Rotate();
    }

    private void OnTriggerStay(Collider other)
    {
        CheckIfDetectedPlayer(other);
    }

    private void Rotate()
    {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }

    private void CheckIfDetectedPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle / 2)
            {
                if (Physics.Raycast(transform.position + (transform.up / 4), direction.normalized, out RaycastHit hit,
                    _howFarCanISee, _onlyLayerToRaycast))
                {
                    if (hit.collider.gameObject == _player && _player.GetComponent<PlayerController>().IsWandering())
                    {
                        PlayDetectedSound();
                    }
                }
            }
        }
    }

    private void PlayDetectedSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
