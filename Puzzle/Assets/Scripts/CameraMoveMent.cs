using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����մϴ�.
    public Vector3 cameraOffset; // ī�޶��� ��ġ �������� �����մϴ�.

    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� �÷��̾��� �ڽ����� �����Ͽ� �÷��̾�� �Բ� �̵��ϵ��� �մϴ�.
        transform.SetParent(player);
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� �����¸�ŭ �̵���ŵ�ϴ�.
        transform.localPosition = cameraOffset;
        // ī�޶��� ȸ���� �÷��̾��� ȸ���� ��ġ��ŵ�ϴ�.
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾��� ȸ���� ���� ī�޶� ȸ���ϵ��� �մϴ�.
        transform.localPosition = cameraOffset;
    }
}
