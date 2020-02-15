using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class PlayerController : MonoBehaviour
{
    
    // Cached references
    
    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    
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
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        
        _materialColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        ChangePlayerStateColor();
    }
    
    // ##################################################
    // ################### Collisions ###################
    // ##################################################

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

    private void EatPumpkin(GameObject pumpkin)
    {
        pumpkin.gameObject.GetComponent<PumpkinController>().Eat();
        ScoreManager.UpdateScore();
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
    
    // ##################################################
    // ################ State Management ################
    // ##################################################
    
    private void ChangePlayerStateColor()
    {
        _meshRenderer.material.color = IsHiding()
            ? new Color(_materialColor.r, _materialColor.g, _materialColor.b, 0.5f)
            : new Color(_materialColor.r, _materialColor.g, _materialColor.b, 1f);
    }

    private void SetCurrentState(PlayerState newState)
    {
        _currState = newState;
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