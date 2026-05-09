using UnityEngine;

public class PlayerRB : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 8f;    
    public float jumpForce = 7f;    
    private bool isGrounded;        

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

         
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

  
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  
        }
    }

 
    void OnCollisionStay(Collision collision)
    {
 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
  
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}