using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    public Sprite visibleImage;
    public Sprite invisibleImage;

    private Image _image;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.IsHiding())
        {
            _image.sprite = invisibleImage;
        }
        else
        {
            _image.sprite = visibleImage;
        }
    }
}
