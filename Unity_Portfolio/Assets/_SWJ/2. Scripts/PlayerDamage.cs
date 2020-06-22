using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    float hp=100f;
    float initHp=100f;
    
    void minusHp()
    {
        hp--;
        print("플레이어 피 : " + hp);
    }


    public void hitDamage(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Invoke("minusHp", i / 10);
        }
    }
}
