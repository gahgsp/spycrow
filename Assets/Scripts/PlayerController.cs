using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Cached references
    private Rigidbody _rigidBody;
    private GridManager _gridManager;
    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;

    private Vector3 _destPos;
    private Color _materialColor;
    private PlayerState _currState;

    private enum PlayerState
    {
        HIDING,
        WANDERING
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();

        _materialColor = _meshRenderer.material.color;
        _destPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        _meshRenderer.material.color = IsHiding()
            ? new Color(_materialColor.r, _materialColor.g, _materialColor.b, 0.5f)
            : new Color(_materialColor.r, _materialColor.g, _materialColor.b, 1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pumpkin"))
        {
            EatPumpkin(other.gameObject);
            if (ScoreManager.CollectedAllPumpkins())
            {
                UIManager.GoToWinScreen();
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetCurrentState(other.GetComponent<Tile>().IsGrass() ? PlayerState.HIDING : PlayerState.WANDERING);
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) && CanMove(Vector3.forward))
        {
            _destPos += Vector3.forward;
        }

        if (Input.GetKeyDown(KeyCode.D) && CanMove(Vector3.right))
        {
            _destPos += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.S) && CanMove(Vector3.back))
        {
            _destPos += Vector3.back;
        }

        if (Input.GetKeyDown(KeyCode.A) && CanMove(Vector3.left))
        {
            _destPos += Vector3.left;
        }

        _rigidBody.MovePosition(_destPos);
    }

    private bool CanMove(Vector3 nextDesiredMove)
    {
        return IsPlayerInsideGridBoundaries(nextDesiredMove) && HasPlayerReachedDestination();
    }

    private bool HasPlayerReachedDestination()
    {
        return transform.position == _destPos;
    }

    private bool IsPlayerInsideGridBoundaries(Vector3 nextMove)
    {
        Vector3 expectedMove = _destPos + nextMove;
        return (expectedMove.x >= _gridManager.MinPositionX() && expectedMove.x <= _gridManager.MaxPositionX()) &&
               (expectedMove.z >= _gridManager.MinPositionZ() && expectedMove.z <= _gridManager.MaxPositionZ());
    }

    private void SetCurrentState(PlayerState newState)
    {
        _currState = newState;
    }

    private void EatPumpkin(GameObject pumpkin)
    {
        Destroy(pumpkin);
        ScoreManager.UpdateScore();
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    public bool IsWandering()
    {
        return _currState == PlayerState.WANDERING;
    }

    public bool IsHiding()
    {
        return _currState == PlayerState.HIDING;
    }
}