using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public ActivateGem gem;

    public float rotateDuration; // 회전 시간
    public float rotateNum; // 회전 각도
    public bool x; // x축 회전 여부
    public bool y; // y축 회전 여부
    public bool z; // z축 회전 여부

    public float waitTime; // 회전 후 대기 시간

    private bool rotating; // 회전 중 여부
    public bool activate; // 일반 회전시 회전 시작 확인

    public bool gamRotate; // 보석 관련
    public bool objRotate; // 무한 회전
    public bool objRotateSetting; // 정해진 위치로 회전

    void Update()
    {
        // 보석 관련 회전
        if (gamRotate && gem != null && gem.activate && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateAndWait());
        }
        // 무한 회전
        if (objRotate && !rotating && activate)
        {
            rotating = true;
            StartCoroutine(Rotate());
        }
        // 정해진 위치로 회전
        if (objRotateSetting && !rotating && activate)
        {
            rotating = true;
            StartCoroutine(RotateValueSetting());
        }
    }

    IEnumerator Rotate() // 무한 회전
    {
        while (activate)
        {
            if (x) // x방향으로 회전
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime);
            }
            if (y) // y방향으로 회전
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime);
            }
            if (z) // z방향으로 회전
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime);
            }

            yield return null; // 다음 프레임까지 대기
        }

        rotating = false;
    }

    IEnumerator RotateAndWait() // 일정 값만큼 회전후 대기후 다시 회전
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            if (x) // x방향으로 회전
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (y) // y방향으로 회전
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (z) // z방향으로 회전
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime / rotateDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 회전 후 대기 시간
        yield return new WaitForSeconds(waitTime);

        rotating = false;
    }

    IEnumerator RotateValueSetting()
    {
        // 목표 회전값을 현재 회전값에 상대적으로 더하거나 빼는 방식으로 설정
        Vector3 targetRotation = transform.eulerAngles;

        if (x) // x축 회전을 설정한 값으로 상대적으로 변경
        {
            targetRotation.x = rotateNum;
        }
        if (y) // y축 회전을 설정한 값으로 상대적으로 변경
        {
            targetRotation.y = rotateNum;
        }
        if (z) // z축 회전을 설정한 값으로 상대적으로 변경
        {
            targetRotation.z = rotateNum;
        }

        // 현재 회전값과 목표 회전값 사이의 차이 계산
        Vector3 startRotation = transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            // 선형 보간을 통해 서서히 목표 회전값으로 회전
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 목표 회전값으로 정확히 설정
        transform.eulerAngles = targetRotation;

        // 회전이 완료되었으므로 회전 중 상태를 해제
        rotating = false;
    }
}
