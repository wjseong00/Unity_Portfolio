using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    Text text;
    //Rigidbody rig;
    Color alpha;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<Text>();
        alpha = text.color;
        text.text = "-" + damage.ToString();
        Invoke("DestroyObject", destroyTime);
        //rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed + Time.deltaTime), transform.position.z);
        //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}