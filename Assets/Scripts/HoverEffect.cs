using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Text _buttonText;
    
    // Start is called before the first frame update
    void Start()
    {
        _buttonText = GetComponentInChildren<Text>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonText.color = new Color(0.2392157f, 0.9019608f, 1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonText.color = new Color(1f, 1f, 1f, 1f);
    }
}
