using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionConversion : MonoBehaviour
{
    public GemCombination gem; // 현재 보석 상태 확인용
    public GameObject[] state; // 상태 오브젝트
    public bool plus; // 현재 상태

    void Start()
    {
        plus = true;
        state[0].gameObject.SetActive(true);
        state[1].gameObject.SetActive(false);
    }

    void Update()
    {
        if (gem.currentGemNum == 1.3f) // 확장 보석일때 v(현재)키 입력시 상태 변화
        {
            if (Input.GetButtonDown("Expansion"))
            {
                if (plus)
                {
                    plus = false;
                    state[0].gameObject.SetActive(false);
                    state[1].gameObject.SetActive(true);
                }
                else
                {
                    plus = true;
                    state[0].gameObject.SetActive(true);
                    state[1].gameObject.SetActive(false);
                }
            }
        }
    }
}
