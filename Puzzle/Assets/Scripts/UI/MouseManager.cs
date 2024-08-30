using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // ���콺 Ŀ�� Ȱ��ȭ ����
    public Sprite customCursorSprite; // Ŀ�� �̹����� ����� ��������Ʈ
    private Texture2D customCursorTexture; // Texture2D�� ��ȯ�� Ŀ�� �̹���

    void Start()
    {
        // Sprite�� Texture2D�� ��ȯ
        if (customCursorSprite != null)
        {
            customCursorTexture = SpriteToTexture2D(customCursorSprite);
            Cursor.SetCursor(customCursorTexture, Vector2.zero, CursorMode.Auto); // Ŀ�� �̹��� ����
        }

        isCursorVisible = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        OffCursorVisibility(); // ���콺 Ŀ�� ��/Ȱ��ȭ
    }

    private void OffCursorVisibility() // Ŀ�� ��Ȱ��ȭ
    {
        if (Input.GetButtonDown("CursorHide")) // Ű �Է� ����
        {
            isCursorVisible = !isCursorVisible; // ���콺 ������ Ȱ��ȭ ����
            Cursor.visible = isCursorVisible; // ���콺 ������ Ȱ��ȭ ���� ����
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // ���콺 ������ ��� ���� ����
        }
    }

    // Sprite�� Texture2D�� ��ȯ�ϴ� �Լ�
    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
