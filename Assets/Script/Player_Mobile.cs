using UnityEngine;
using UnityEngine.UI;  // ใช้สำหรับ UI Buttons

public class Player_Mobile : MonoBehaviour
{
    public float playerSpeed = 5f;  // ความเร็วในการเคลื่อนที่ของตัวละคร
    private Rigidbody2D rb;  // ตัวแปร Rigidbody2D สำหรับการเคลื่อนที่
    private Vector2 playerDirection = Vector2.zero;  // ทิศทางการเคลื่อนที่, เริ่มต้นเป็น Vector2.zero

    // Referencing UI Buttons
    public Button upButton;
    public Button downButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // รับค่า Rigidbody2D

        // ตั้งค่าปุ่มขึ้นและลง
        upButton.onClick.AddListener(MoveUp);
        downButton.onClick.AddListener(MoveDown);
    }

    // ฟังก์ชันที่ใช้ในการเคลื่อนที่ขึ้น
    public void MoveUp()
    {
        playerDirection = Vector2.up;  // ตั้งทิศทางให้เคลื่อนที่ขึ้น
    }

    // ฟังก์ชันที่ใช้ในการเคลื่อนที่ลง
    public void MoveDown()
    {
        playerDirection = Vector2.down;  // ตั้งทิศทางให้เคลื่อนที่ลง
    }

    void Update()
    {
        // เช็คว่า playerDirection มีค่าหรือไม่ (มีค่าก็เคลื่อนที่)
        if (playerDirection != Vector2.zero)
        {
            rb.velocity = playerDirection * playerSpeed;  // เคลื่อนที่ตามทิศทางที่กำหนด
        }
        else
        {
            rb.velocity = Vector2.zero;  // หยุดการเคลื่อนที่เมื่อไม่มีการกดปุ่ม
        }
    }
}
