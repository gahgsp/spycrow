using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StatusManager : MonoBehaviour
{

    [SerializeField] Sprite visibleImage;
    [SerializeField] Sprite invisibleImage;

    private Image _image;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // We do this because the Canvas is created before the Player is instantiated.
        // Is there a better way to do this?
        if (!_playerController)
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        _image.sprite = _playerController.IsHiding() ? invisibleImage : visibleImage;
    }
}
