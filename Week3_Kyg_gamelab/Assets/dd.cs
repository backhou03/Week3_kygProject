using UnityEngine;

public class dd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 마우스 커서 잠금 해제 및 화면에 표시
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
