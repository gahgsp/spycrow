using UnityEngine;

public class AISight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public float rotationSpeed = 30f;

    // Cached references
    private GameObject _player;
    private SphereCollider _collider;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       Rotate();
    }

    private void OnTriggerStay(Collider other)
    {
        CheckIfCaughtPlayer(other);
    }

    private void Rotate()
    {
        transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);
    }

    private void CheckIfCaughtPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle / 2)
            {
                if (Physics.Raycast(transform.position + (transform.up / 3), direction.normalized, out RaycastHit hit,
                    _collider.radius))
                {
                    if (hit.collider.gameObject == _player && _player.GetComponent<PlayerController>().IsWandering())
                    {
                        Debug.Log("You got caught!");
                    }
                }
            }
        }
    }
}
