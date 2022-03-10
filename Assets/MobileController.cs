using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _backgroung;
    [SerializeField] private Image _joystick;
    private Vector2 _inputVector;
    
    private void Start() 
    {
        _backgroung.gameObject.SetActive(false);

    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        _backgroung.gameObject.SetActive(true);
        _backgroung.transform.position = ped.position;
        OnDrag(ped);

    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        _backgroung.gameObject.SetActive(false);
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
        _inputVector = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroung.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _backgroung.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _backgroung.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(pos.x * 2, pos.y * 2);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * _backgroung.rectTransform.sizeDelta.x / 2, _inputVector.y * _backgroung.rectTransform.sizeDelta.y / 2);


        }
    }
    public float Horizontal()
    {
        return _inputVector.x;
    }
    public float Vertical()
    {
        return _inputVector.y;
    }

}
