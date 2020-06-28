using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheck : MonoBehaviour
{
    public float range =20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        //공격 가능 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
        
    }
}
