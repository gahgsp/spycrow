using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    
    // Cached references.
    private Rigidbody _rigidBody;
    private GridManager _gridManager;
    
    private Vector3 _destPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        
        _gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();
        
        _destPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }
    
    private void Move()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && CanMove(Vector3.forward))
        {
            _destPos += Vector3.forward;
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && CanMove(Vector3.right))
        {
            _destPos += Vector3.right;
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && CanMove(Vector3.back))
        {
            _destPos += Vector3.back;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && CanMove(Vector3.left))
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
}
