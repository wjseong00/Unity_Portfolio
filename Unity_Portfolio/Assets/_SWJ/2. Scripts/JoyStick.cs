using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Jonstick;

    private float radius;

    [SerializeField] private GameObject go_Player;
    [SerializeField] public float moveSpeed;

    public Vector2 value;
    public Animator anim;
    public float aniSpeed = 1.5f;
    private bool isTouch = false;
    private Vector3 movePosition;
    public bool stun = false;

    // Start is called before the first frame update
    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouch)
        {
            if(!stun)
            {
                anim.SetBool("Run", true);
                go_Player.transform.position += movePosition;
                go_Player.transform.rotation = Quaternion.LookRotation(movePosition);
            }
            
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Jonstick.localPosition = value;

        float distance = Vector2.Distance(rect_Background.position, rect_Jonstick.position) / radius;
        value = value.normalized;
        movePosition = new Vector3(value.x *distance* moveSpeed * Time.deltaTime, 0.0f, value.y * distance * moveSpeed * Time.deltaTime);
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
