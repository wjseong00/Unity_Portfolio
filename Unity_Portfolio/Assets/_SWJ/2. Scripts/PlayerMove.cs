using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    
    
    int jumpCount = 0;      //점프카운트

    CharacterController cc;
    public float aniSpeed = 1.5f;
    public float frontSpeed = 2.0f;
    public float backSpeed = 2.0f;
    public float rotaSpeed = 2.0f;
    public float jumpSpeed = 5.0f;
    public GameObject startPoint;
    public float gravity = -10;
    float velocityY;        //낙하속도(벨로시티는 방향과 힘을 들고 있다)
    private Vector3 velocity;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    private GameObject cameraObject;
    public AudioSource sound;
    public bool isJumpBar = false;
    float h = 0f;
   float v = 0f;
    bool isJump = false;
    bool stun = false;
    public float slowTime =10f;

    public float rotationSpeed = 360f;

   


    void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        cameraObject = GameObject.FindWithTag("MainCamera");
    }

    
    void Update()
    {
        if(transform.position.y<-5)
        {
            transform.position = startPoint.transform.position;
        }
        if(!stun)
        {
            if(isJumpBar)
            {
                jumpSpeed = 10f;
            }
            else
            {
                jumpSpeed = 4f;
            }
            if (cc.isGrounded) //땅에 닿았냐?
            {
                anim.SetBool("Jump", false);
                velocityY = 0;
                jumpCount = 0;
                isJump = false;
            }
            var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
            var maxDistance = 0.25f;
            Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo , maxDistance,1<<8))
            {
                if(hitInfo.collider.CompareTag("Ground"))
                {
                    anim.SetBool("Jump", false);
                    velocityY = 0;
                    jumpCount = 0;
                    isJump = false;
                }
                
            }
            
            if(Imotal.instance.isKeyBorad==true)
            {
                if (Input.GetButtonDown("Jump") && jumpCount < 2)
                {
                    sound.Play();
                    anim.SetBool("Jump", true);
                    jumpCount++;
                    velocityY = jumpSpeed;
                    

                }

            }



        }
        
    }
    public void Jump()
    {
        if (jumpCount < 2)
        {
            sound.Play();
            isJump = true;
            anim.SetBool("Jump", true);
            jumpCount++;
            velocityY = jumpSpeed;
            velocity.y = velocityY;
            cc.Move(velocity  * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        if(!stun)
        {
            if(Imotal.instance.isKeyBorad == true)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
                //anim.SetFloat("Speed", v);
                //anim.SetFloat("Speed", h);

            }
            else
            {

            }


            anim.speed = aniSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            //rig.useGravity = true;
            
            velocity = new Vector3(h, 0, v);

            velocity = Camera.main.transform.TransformDirection(velocity);
            velocity.y = 0;

            if (h == 0 && v == 0)
            {
                anim.SetBool("Run", false);

            }
            else
            {
                anim.SetBool("Run", true);
                transform.rotation = Quaternion.LookRotation(velocity);
            }
            
                velocityY += gravity * Time.deltaTime;
                velocity.y = velocityY;

                cc.Move(velocity * frontSpeed * Time.deltaTime);
            
            

        }
    
    
    }
    public void setStun(bool _stun)
    {
        stun = _stun;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ground")
        {
            anim.SetBool("Jump", false);
            velocityY = 0;
            jumpCount = 0;
            isJump = false;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            anim.SetBool("Jump", false);
            velocityY = 0;
            jumpCount = 0;
            isJump = false;
        }
    }
}
