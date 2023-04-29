using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D của object player
    public float jumpForce = 10f; // Lực nhảy của object player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy component Rigidbody2D của object player
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // Nếu nhấn phím Space, gọi hàm Jump để object player nhảy
        }
    }
    
    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce; // Đặt vận tốc của object player lên trên với lực nhảy cho trước
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall")) // Nếu object player va chạm với object wall
        {
            float contactPoint = col.contacts[0].point.x - transform.position.x; // Tính khoảng cách giữa vị trí va chạm của object player với object wall
            
            if (Mathf.Abs(contactPoint) > 0.6f) // Nếu khoảng cách quá lớn, đặt vận tốc của object player thành 0 để tránh tình trạng object player bị dính vào object wall
            {
                rb.velocity = Vector2.zero;
            }
            else if (contactPoint < 0) // Nếu vị trí va chạm của object player nằm bên trái của object wall
            {
                rb.velocity = new Vector2(5f, rb.velocity.y); // Đặt vận tốc của object player về phía bên phải để di chuyển về phía bên phải
            }
            else // Nếu vị trí va chạm của object player nằm bên phải của object wall
            {
                rb.velocity = new Vector2(-5f, rb.velocity.y); // Đặt vận tốc của object player về phía bên trái để di chuyển về phía bên trái
            }
        }
    }
}
