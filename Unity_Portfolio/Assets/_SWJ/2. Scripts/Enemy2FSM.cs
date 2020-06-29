using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//몬스터 유한상태머신
public class Enemy2FSM : MonoBehaviour
{
    NavMeshAgent nav;
    
    //몬스터 상태 이넘문
    enum EnemyState
    {
        Idle, Move, Attack, Return, Damaged, Die
    }
    EnemyState state;//몬스터 상태 변수
    public Canvas canvas;
    /// 유용한 기능
    #region "Idle 상태에 필요한 변수들"

    #endregion

    #region "Move 상태에 필요한 변수들"

    #endregion

    #region "Attack 상태에 필요한 변수들"
    public GameObject groundEfx;
    float Attacktime;
    #endregion

    #region "Return 상태에 필요한 변수들"
    #endregion

    #region "Damage 상태에 필요한 변수들"

    #endregion

    #region "Die 상태에 필요한 변수들"

    #endregion

    //필요한 변수들
    public float attackRange = 2f;//공격 가능 범위
    public float findRange = 5f;//플레이어를 찾는 범위
    public float moveRange = 10f;//시작지점에서 최대 이동
    Vector3 startPoint;//몬스터 시작위치
    Transform player;   //플레이어를 찾기위해
    Animator anim;
    GameObject hpBar;
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 1.1f, 0);

    private Canvas uiCanvas;
    private Image hpBarImage;

    public GameObject coinFactory;
    public GameObject hudDamageText;
    public Transform hudPos;
    public GameObject deathFactory;

    //몬스터 일반변수
    float hp = 100f;//체력
    float initHp = 100f;
    int att = 5;//공격력
    float speed = 5.0f;//이동속도

    //공격 딜레이
    float attTime = 1f; //2초에 한번 공격
    float timer = 0f; //타이머

    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        //시작지점 저장
        startPoint = transform.position;
        //플레이어 설정
        player = GameObject.Find("Player").transform;
        //애니메이터
        anim = GetComponent<Animator>();
        
        //네비게이션
        nav = GetComponent<NavMeshAgent>();
        setHpBar();
    }
    void setHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpBar.GetComponent<HpBarScript>();
        _hpbar.targetTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }
    void Update()
    {
        
        //상태에 따른 행동처리
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
            default:
                break;
        }

    }//end of void Update()
    //기본상태
    private void Idle()
    {

        //1. 플레이어와 일정범위가 되면 이동상태로 변경(탐지범위)
        //- 플레이어 찾기 (GameObject.Find("Player")
        //- 일정거리 20미터 (거리비교 : Distance, magnitude 아무거나) 
        //- 상태변경
        //- 상태전환 출력
        
        if (Vector3.Distance(transform.position, player.position) < findRange)
        {
            anim.SetBool("Run",true);
            state = EnemyState.Move;
            
        }

    }
    //이동상태
    private void Move()
    {

        //1. 플레이어를 향해 이동 후 공격범위 안에 들어오면 공격상태로 변경
        //2. 플레이어를 추격하더라도 처음위치에서 일정범위를 넘어가면 리턴상태로 변경 
        //- 플레이어 처럼 캐릭터컨트롤러를 이용하기
        //- 공격범위 1미터
        //- 상태변경
        //- 상태전환 출력

        //이동중 이동할 수 있는 최대범위에 들어왔을 때
        if (Vector3.Distance(transform.position, startPoint) > moveRange)
        {
            anim.SetBool("Run", true);
            state = EnemyState.Return;
            

        }
        //리턴상태가 아니면 플레이어를 추격해야 한다
        else if (Vector3.Distance(transform.position, player.position) >= attackRange)
        {
            //플레이어를 추격
            //이동방향 (벡터의 뺄셈)
            Vector3 dir = (player.position - transform.position).normalized;
          
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            if (!nav.pathPending)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                anim.SetBool("Run", true);
                state = EnemyState.Return;
            }

        }
        else //공격범위 안에 들어옴
        {
            nav.ResetPath();
            anim.SetBool("Run", false);
            
            state = EnemyState.Attack;
           
            
        }

    }
    void delay()
    {
        GameObject ground = Instantiate(groundEfx);
        ground.transform.position = transform.position;
        Destroy(ground, 1f);
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
        for (int i = 0; i < cols.Length; i++)
        {

            if (cols[i].tag == "Player")
            {
                player.GetComponent<PlayerDamage>().hitDamage(att);
            }
        }
    }
    //공격상태
    private void Attack()
    {

        //공격범위안에 들어옴
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            //일정 시간마다 플레이어를 공격하기
            timer += Time.deltaTime;
            Attacktime += Time.deltaTime;
            
            if (timer > attTime)
            {
                anim.SetBool("Attack", true);
                
                Invoke("delay", 0.5f);
                


                //타이머 초기화
                timer = 0f;
            }
            else
            {
                anim.SetBool("Attack",false);
            }
        }
        else//현재상태를 무브로 전환하기 (재추격)
        {
            anim.SetBool("Attack",false);
            anim.SetBool("Run", true);
            state = EnemyState.Move;
            
            //타이머 초기화
            timer = 0f;
        }

    }
    //복귀상태
    private void Return()
    {


        //시작위치까지 도달하지 않을때는 이동
        //도착하면 대기상태로 변경
        if (Vector3.Distance(transform.position, startPoint) > 0.4f)
        {
            nav.SetDestination(startPoint);
           
        }
        else if (Vector3.Distance(transform.position, startPoint) <= 0.4f)
        {
            //위치값을 초기값으로 
            nav.ResetPath();
            anim.SetBool("Run", false);
            state = EnemyState.Idle;
           
        }


    }
    void MinusHp()
    {
        
        hp--;
        hpBarImage.fillAmount = hp / initHp;
        
        if (hp <= 0)
        {
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            anim.SetBool("Damage", false);
            anim.SetTrigger("Die");
            state = EnemyState.Die;
           

            Die();
        }


    }
    //플레이어쪽에서 충돌감지를 할 수 있으니 이함수는 퍼블릭으로 만들자
    public void hitDamage(int value)
    {
        //예외처리
        //피격상태이거나, 죽은 상태일때는 데미지 중첩으로 주지 않는다
        if (state == EnemyState.Die) return;

        //체력깍기
        for (float i = 0; i < value; i++)
        {
            Invoke("MinusHp", i/10);
        }
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(hudPos.position);

        hudText.transform.localPosition = ScreenPos; // 표시될 위치
        hudText.transform.SetParent(canvas.transform);
        hudText.GetComponent<DamageText>().damage = value; // 데미지 전달
        
        //hp--;
        //hpBarImage.fillAmount = hp / initHp;

        //몬스터의 체력이 1이상이면 피격상태
        if (hp > 0)
        {
            state = EnemyState.Damaged;
            anim.SetBool("Damage", true);
            
            Damaged();
        }
        //0이하이면 죽음상태
        else
        {
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            anim.SetBool("Damage", false);
            anim.SetTrigger("Die");
            state = EnemyState.Die;
            

            Die();
        }
    }



    //피격상태 (Any State)
    private void Damaged()
    {

        //코루틴을 사용하자
        //1. 몬스터 체력이 1이상
        //2. 다시 이전상태로 변경
        //- 상태변경
        //- 상태전환 출력

        //피격 상태를 처리하기 위한 코루틴을 실행한다
        StartCoroutine(DamageProc());
    }

    //피격상태 처리용 코루틴
    IEnumerator DamageProc()
    {
        //피격모션 시간만큼 기다리기
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Damage", false);
        yield return new WaitForSeconds(0.8f);
        
        //현재상태를 이동으로 전환
        state = EnemyState.Move;
        
    }

    //죽음상태 (Any State)
    private void Die()
    {
        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태변경
        //- 상태전환 출력 (죽었다)

        //진행중인 모든 코루틴은 정지한다
        StopAllCoroutines();
        
        //죽음상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DieProc());
    }

    IEnumerator DieProc()
    {
        // 네비게이션 비활성화
        nav.ResetPath();

        //2초후에 자기자신을 제거한다
        
        yield return new WaitForSeconds(2.0f);
        GameObject coin = Instantiate(coinFactory);
        coin.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        GameObject death = Instantiate(deathFactory);
        death.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        Destroy(hpBar);
        Destroy(gameObject);
        Destroy(death, 2f);
        Destroy(coin, 6f);
        //Destroy(gameObject);
    }

    
    private void OnDrawGizmos()
    {
        //공격 가능 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //플레이어 찾을 수 있는 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, findRange);
        //이동가능한 최대 범위
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPoint, moveRange);
    }
}
