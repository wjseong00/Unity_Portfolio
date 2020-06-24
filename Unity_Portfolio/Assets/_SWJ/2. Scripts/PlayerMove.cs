using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    
    int jumpCount = 0;      //점프카운트

    CharacterController cc;
    public float aniSpeed = 1.5f;
    public float frontSpeed = 4.0f;
    public float backSpeed = 2.0f;
    public float rotaSpeed = 2.0f;
    public float jumpSpeed = 3.0f;

    public float gravity = -10;
    float velocityY;        //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    private Vector3 velocity;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    private GameObject cameraObject;


    static int idle_c = Animator.StringToHash("Base Layer.Idle_C");
    static int idle_a = Animator.StringToHash("Base Layer.Idle_A");
    static int runState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int cuteState = Animator.StringToHash("Base Layer.Cute1");

    bool stun = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        cameraObject = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(!stun)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

            //dir = transform.TransformDirection(dir);
            transform.Rotate(Vector3.up *rotaSpeed * Time.deltaTime*h);
            cc.Move(dir.normalized * frontSpeed * Time.deltaTime);
            velocityY += gravity * Time.deltaTime;
            dir.y = velocityY;
            
            
            //if (cc.collisionFlags == CollisionFlags.Below) //땅에 닿았냐?
            if (cc.isGrounded) //땅에 닿았냐?
            {

                anim.SetBool("Jump", false);
                velocityY = 0;
                jumpCount = 0;
            }
            if (Input.GetButtonDown("Jump") && jumpCount < 2)
            {

                //cameraObject.SendMessage("setCameraPositionJumpView");
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Jump", true);
                    jumpCount++;
                    velocityY = jumpSpeed;
                }
            }
        }
        
    }
    
    //private void FixedUpdate()
    //{
    //    if(!stun)
    //    {
    //        float h = Input.GetAxis("Horizontal");
    //        float v = Input.GetAxis("Vertical");
    //        anim.SetFloat("Speed", v);
    //        anim.SetFloat("Direction", h);
    //        anim.speed = aniSpeed;
    //        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
    //        if (v > 0.1)
    //        {
    //            velocity *= frontSpeed;
    //        }
    //        else if (v < -0.1)
    //        {
    //            velocity *= backSpeed;
    //        }
    //
    //
    //
    //
    //        velocity = new Vector3(h, 0, v);
    //        transform.rotation = Quaternion.LookRotation(velocity);
    //        
    //    }
    //
    //
    //}
    public void setStun(bool _stun)
    {
        stun = _stun;
    }
}
