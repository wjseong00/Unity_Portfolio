using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CameraJoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Jonstick;

    private float radius;

   
    [SerializeField] private float moveSpeed;

    public Vector2 value;

    private bool isTouch = false;
    private Vector3 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Jonstick.localPosition = value;

        float distance = Vector2.Distance(rect_Background.position, rect_Jonstick.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(value.x * distance * moveSpeed * Time.deltaTime, 0.0f, value.y * distance * moveSpeed * Time.deltaTime);
        movePosition = Camera.main.transform.TransformDirection(movePosition);
        movePosition.y = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }
    public void Reset()
    {
        isTouch = false;
        rect_Jonstick.localPosition = Vector3.zero;
        value = Vector2.zero;
        movePosition = Vector3.zero;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_Jonstick.localPosition = Vector3.zero;
        value = Vector2.zero;
        movePosition = Vector3.zero;
    }
}
