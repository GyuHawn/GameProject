using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteDevice : MonoBehaviour
{
    public LEDNode activateNode; // 연결된 선
    
    public Material[] materials; // 재질
    public GameObject changeMaterialObj;  // 재직변경 할 오브젝트

    public GameObject[] objs; // 제어할 오브젝트 

    public bool activated; // 활성화 시킬지
    public bool destroy; // 제거할지

    private Renderer render;

    void Start()
    {
        render = changeMaterialObj.GetComponent<Renderer>();

        if (activateNode != null) // 이벤트 구독, 활성화 상태 감지
        {
            activateNode.activationChanged += ActivatedCheck;
        }
    }

    void Update()
    {
        ActivatedCheck(false); // 활성화 체크
    }

    void ActivatedCheck(bool dummy)
    {
        bool activated = true;

        if (!activateNode.activate) // 비활성화시 상태 false, 재질 변경
        {
            activated = false;
            UpdateColor();
        }

        if (activated) // 활성화시 재질변경
        {
            UpdateColor();

            if (destroy) // 제어할 오브젝트 제거
            {
                Destroy();
            }
        }
    }

    void UpdateColor() // 재질 변경
    {
        render.material = activateNode.activate ? materials[1] : materials[0];
    }

    void Destroy() // 제거
    {
        if(activateNode.activate && objs != null)
        {
            foreach(GameObject obj in objs)
            {
                Destroy(obj);
            }   
        }
    }

}
