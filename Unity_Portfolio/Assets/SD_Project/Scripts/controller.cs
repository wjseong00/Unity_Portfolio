
using UnityEngine;
using System.Collections;


[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class controller : MonoBehaviour
{
	
	public float animSpeed = 1.5f;				
	public float lookSmoother = 3.0f;			
	public bool useCurves = true;				
	public float useCurvesHeight = 0.5f;		
	public float forwardSpeed = 7.0f;
	public float backwardSpeed = 2.0f;
	public float rotateSpeed = 2.0f;
	public float jumpPower = 3.0f; 
	private CapsuleCollider col;
	private Rigidbody rb;
	private Vector3 velocity;
	private float orgColHight;
	private Vector3 orgVectColCenter;
	private Animator anim;						
	private AnimatorStateInfo currentBaseState;			
	private GameObject cameraObject;

    float jumpCount = 0; //점프카운터

	static int idle_cState = Animator.StringToHash("Base Layer.Idle_C");
	static int idle_aState = Animator.StringToHash("Base Layer.Idle_A");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int cute1State = Animator.StringToHash("Base Layer.Cute1");
	
	
	
	
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		cameraObject = GameObject.FindWithTag("MainCamera");
		orgColHight = col.height;
		orgVectColCenter = col.center;
	}

    private void OnCollisionEnter(Collision collision)
    {
        int layer = 1 << 8;
        if(collision.gameObject.layer == layer)
        {
            jumpCount = 0;
        }
    }
    private void Update()
    {
        Debug.Log(jumpCount);
        //Ray jumpRay = new Ray(transform.position, Vector3.down);
        //RaycastHit hit;
        //if(Physics.Raycast(jumpRay,out hit,1f,1<<8))
        //{
        //    jumpCount = 0;
        //}
    }
    void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");				
		float v = Input.GetAxis("Vertical");				
		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h); 						
		anim.speed = animSpeed;								
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	
		rb.useGravity = true;
		
		
		
		velocity = new Vector3(0, 0, v);
		velocity = transform.TransformDirection(velocity);
		if (v > 0.1) {
			velocity *= forwardSpeed;	
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}

		if (Input.GetButtonDown("Jump") ) {
            if (jumpCount < 2)
            {
                
				if(!anim.IsInTransition(0))
				{
                    jumpCount++;
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}
        
		
		transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate(0, h * rotateSpeed, 0);	
		if (currentBaseState.fullPathHash == locoState){
			if(useCurves){
				resetCollider();
			}
            
        }
		if(currentBaseState.fullPathHash == jumpState)
		{
			cameraObject.SendMessage("setCameraPositionJumpView");	
			if(!anim.IsInTransition(0))
			{
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{
                            
                            resetCollider();
						}
                        
					}
                    
                }
                
				anim.SetBool("Jump", false);
			}
		}

		
		else if (currentBaseState.fullPathHash == idle_cState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}

			
			
		}
		else if (currentBaseState.fullPathHash == idle_aState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}
		}
		else if (currentBaseState.fullPathHash == cute1State)
		{
			
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Cute1", false);
			}
		}

	}
	
	
	void resetCollider()
	{
		
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}
}
