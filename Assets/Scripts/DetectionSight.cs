using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class DetectionSight : MonoBehaviour
{
    [SerializeField] float fieldOfViewAngle = 110f;
    [SerializeField] float rotationSpeed = 30f;
    [SerializeField] private GameObject exclamationSign;
    
    // Cached references.
    private GameObject _player;
    private SphereCollider _collider;
    private AudioSource _audioSource;

    private Vector3 _currPosition;
    private float _howFarCanISee;
    private int _onlyLayerToRaycast = 1 << 10; // Player layer (10)
    private bool _alreadySpawnedExclamation;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _collider = GetComponent<SphereCollider>();
        _audioSource = GetComponent<AudioSource>();
        
        _currPosition = GetComponent<Transform>().position;
        
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
            Vector3 direction = other.transform.position - _currPosition;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle / 2)
            {
                if (Physics.Raycast(_currPosition + (transform.up / 4), direction.normalized, out RaycastHit hit,
                    _howFarCanISee, _onlyLayerToRaycast))
                {
                    if (hit.collider.gameObject == _player && _player.GetComponent<PlayerController>().IsWandering())
                    {
                        OnPlayerDetection();
                    }
                }
            }
        }
    }

    private void OnPlayerDetection()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        // We only need one exclamation...
        if (!_alreadySpawnedExclamation)
        {
            Instantiate(exclamationSign,
                new Vector3(_currPosition.x, _currPosition.y * 5, _currPosition.z),
                Quaternion.identity, transform);    
            _alreadySpawnedExclamation = true;
        }
        
        Invoke(nameof(LoadDeathScreen), _audioSource.clip.length);
    }

    private void LoadDeathScreen()
    {
        UIManager.GoToDeathScreen();
    }
}
