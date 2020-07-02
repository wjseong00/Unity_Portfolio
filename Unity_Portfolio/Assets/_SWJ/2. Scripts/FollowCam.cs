using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform camPos;
    public Transform target;
    public CameraJoyStick joystick;
    //카메라 회전 속도
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    private float h = 0.0f;
    private float v = 0.0f;

    //y값 제한
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    //앵글의 최소,최대 제한
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    private Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }
    private void LateUpdate()
    {
        //float h = Input.GetAxis("Mouse X");
        //
        //
        //
        //tr.position = Vector3.Slerp(tr.position, camPos.position, Time.deltaTime * rotateDamping);
        ////tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        ////Vector3 dir = new Vector3(h * 10 * Time.deltaTime, 0, 0);
        //Vector3 dir = new Vector3(0, 0, h);
        //
        //tr.RotateAround(tr.position, target.position,h);
        //tr.LookAt(target.position + (target.up * targetOffset));

        //h = Input.GetAxis("Mouse X");
        //v = Input.GetAxis("Mouse Y");
        
        h = joystick.value.x;
        v = joystick.value.y;
        
        Debug.Log(h);
        //카메라 회전속도 계산
        x += h * xSpeed * 0.015f;
        y -= v * ySpeed * 0.015f;


        //앵글값 정하기
        //y값의 Min과 MaX 없애면 y값이 360도 계속 돎
        //x값은 계속 돌고 y값만 제한
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        //카메라 위치 변화 계산
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0,1.0f,-4f) + target.position + new Vector3(0.0f, 0, 0.0f);

        transform.rotation = rotation;
        transform.position = position;
    }
    
}
