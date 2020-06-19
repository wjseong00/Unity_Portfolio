using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject iceWall;
    public GameObject normalAttack;
    public Transform attackPos;
    public GameObject fireAttackMotion;
    public GameObject iceAttackMotion;
    public GameObject normalAttackMotion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Normal();
    }

    private void Normal()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject missile = Instantiate(normalAttack);
            GameObject motion = Instantiate(normalAttackMotion,GameObject.Find("Player").transform);
            missile.transform.position = attackPos.position;
            missile.transform.rotation = attackPos.rotation;
            motion.transform.position = transform.position + new Vector3(0,0.05f,0);
            Destroy(missile, 3f);
            Destroy(motion, 1f);
            
        }
    }
}
