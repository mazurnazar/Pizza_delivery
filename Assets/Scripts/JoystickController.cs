using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] float speed;
    public float Speed { get => speed;private set { } }

    [SerializeField] Image joystickContainer, joystick;
    [SerializeField] Canvas canvas;
    [SerializeField] PlayerMoving moving;

    public delegate void speedChanged();
    public event speedChanged ChangeSpeed;

    private Vector3 direction; // direction of movement
    
    private bool CanMove = true;
    
    // when clicked enable joystick and set position of ckick
    public void OnPointerDown(PointerEventData eventData)
    {
        speed = moving.Speed;
    }

    // when dragging -> move middle circle of joystick in boundaries of joystic container
    public void OnDrag(PointerEventData eventData)
    {
        if (!CanMove) { return; }
        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainer.transform as RectTransform,
                                                                Input.mousePosition,
                                                                canvas.worldCamera,
                                                                out pos);
        float x = pos.x / joystickContainer.rectTransform.sizeDelta.x;
        float y = pos.y / joystickContainer.rectTransform.sizeDelta.y;
        
        // set direction
        direction = new Vector3(x, y, 0);
        direction = direction.magnitude > 1 ? direction.normalized : direction;
        joystick.rectTransform.anchoredPosition = new Vector2(direction.x * joystickContainer.rectTransform.sizeDelta.x / 3,   0); 
        
    }

    // on click up deactivate joystick and set direction to zero
    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.rectTransform.anchoredPosition = Vector2.zero;
        direction = Vector3.zero;
    }

    void Start()
    {
        direction = Vector3.zero;
    }
    void Update()
    {
        if (!CanMove) { return; }
        if (direction.x != 0 && speed >= 0) 
        {
            speed += direction.x * Time.deltaTime; ChangeSpeed?.Invoke(); // change speed of player in accordance with direction of joystick
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        direction = Vector3.zero;
        CanMove = false;
    }
}
