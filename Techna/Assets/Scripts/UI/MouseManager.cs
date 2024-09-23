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
        // Sprite�� Texture2D�� ��ȯ �� Ŀ�� ����
        if (customCursorSprite != null)
        {
            customCursorTexture = SpriteToTexture2D(customCursorSprite);
            Cursor.SetCursor(customCursorTexture, Vector2.zero, CursorMode.Auto); // Ŀ�� �̹��� ����
        }

        // Ŀ�� ���� �� ��� ���� ����
        SetCursorState(false);
    }

    void Update()
    {
        // ���콺 Ŀ�� Ȱ��ȭ ���θ� ���
        if (Input.GetButtonDown("CursorHide"))
        {
            SetCursorState(!isCursorVisible);
        }
    }

    // Ŀ�� Ȱ��ȭ ���� ���� �Լ�
    private void SetCursorState(bool isVisible)
    {
        isCursorVisible = isVisible;
        Cursor.visible = isCursorVisible;
        Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
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
        {
            return sprite.texture;
        }
    }
}
