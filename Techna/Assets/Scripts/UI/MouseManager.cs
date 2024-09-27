using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // ���콺 Ŀ�� Ȱ��ȭ ����
    public Sprite[] crossHairs; // ������ �迭
    public GameObject crossHair; // ������

    private PlayerInputActions inputActions; // Input Actions 

    void Awake()
    {
        inputActions = new PlayerInputActions(); // Input Actions �ʱ�ȭ
    }

    private void Start()
    {
        SetCursorState(isCursorVisible); // ���� �� Ŀ�� ���� ����
    }

    void OnEnable()
    {
        inputActions.Enable(); // Input Actions Ȱ��ȭ
        inputActions.UI.Cursor.performed += ctx => ToggleCursor(); // CursorHide �׼ǰ� �޼��� ����
    }

    void OnDisable()
    {
        inputActions.Disable(); // Input Actions ��Ȱ��ȭ
    }

    // Ŀ�� Ȱ��ȭ ���� ��� �Լ�
    private void ToggleCursor()
    {
        SetCursorState(!isCursorVisible);
    }

    // Ŀ�� Ȱ��ȭ ���� ���� �Լ�
    private void SetCursorState(bool isVisible)
    {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;

        crossHair.SetActive(!isVisible);
    }
}
