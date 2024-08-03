﻿using System.Collections.Generic;
using UnityEngine;

public class ProjectilesScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;
    public Camera camera;
    public GameObject firePoint; // 발사 위치

    public float gemIndex;
    public bool changeBullet;
    public GameObject[] B_Bullets; // 총알 투사체 배열
    public GameObject[] B_A_Bullets; // 총알 + 속성 투사체 배열
    public GameObject[] B_F_Bullets; // 총알 + 기능 투사체 배열
    public GameObject[] B_A_F_Bullets; // 총알 + 속성 + 기능 투사체 배열
    public bool b_Bullet;
    public bool b_A_Bullet;
    public bool b_F_Bullet;
    public bool b_A_F_Bullet;

    private float timeToFire = 0f; // 발사 시간 추적
    public GameObject effectToSpawn; // 생성할 투사체

    private void Awake()
    {
        if (!playerMovement)
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (B_Bullets.Length > 0) // 총알 프리팹 확인
            effectToSpawn = B_Bullets[0]; // 첫 번째 효과 선택
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire && !playerMovement.isCursorVisible) // 마우스 클릭 & 발사시간o & 커서 비활성화 중
        {
            timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate; // 발사 시간 업데이트
            ShotBullet(); // 효과 생성
        }
    }

    public void ShotBullet() // 효과 생성
    {
        if (firePoint != null && camera != null) // 발사 위치, 카메라 확인
        {
            // 카메라 중앙에서 레이 발사
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point; // 레이가 맞은 지점
            }
            else
            {
                targetPoint = ray.GetPoint(1000); // 아무것도 맞지 않을시 먼 거리의 점을 목표로 설정
            }

            Vector3 direction = (targetPoint - firePoint.transform.position).normalized;

            // 총구에서 카메라 중앙 방향으로 총알 생성
            GameObject shotBullet = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.LookRotation(direction));
            shotBullet.name = effectToSpawn.name;

            // 총알 초기 속도 설정
            Rigidbody rb = shotBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * shotBullet.GetComponent<ProjectileMoveScript>().speed; // 총알 속도 설정
            }
        }
    }
}
