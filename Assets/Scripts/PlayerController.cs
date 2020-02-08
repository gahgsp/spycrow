using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 _destPos;

    private enum PlayerState
    {
        HIDING,
        WANDERING
    }

    private PlayerState _currState;
    
    // Start is called before the first frame update
    void Start()
    {
        _destPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    public bool IsWandering()
    {
        return _currState == PlayerState.WANDERING;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pumpkin"))
        {
            GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            Destroy(other.gameObject);
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetCurrentState(other.GetComponent<Tile>().IsGrass() ? PlayerState.HIDING : PlayerState.WANDERING);
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) && HasPlayerReachedDestination())
        {
            _destPos += Vector3.forward;
        }

        if (Input.GetKeyDown(KeyCode.D) && HasPlayerReachedDestination())
        {
            _destPos += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.S) && HasPlayerReachedDestination())
        {
            _destPos += Vector3.back;
        }

        if (Input.GetKeyDown(KeyCode.A) && HasPlayerReachedDestination())
        {
            _destPos += Vector3.left;
        }

        transform.position = Vector3.MoveTowards(transform.position, _destPos, Time.deltaTime * 3f);
    }

    private bool HasPlayerReachedDestination()
    {
        return transform.position == _destPos;
    }

    private void SetCurrentState(PlayerState newState)
    {
        _currState = newState;
    }
}
