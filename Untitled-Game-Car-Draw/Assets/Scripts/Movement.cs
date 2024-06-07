using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float fallThreshold = -10f;  // Adjust this value as needed
    public TextMeshProUGUI clickToStartText;  // Reference to the TextMeshPro Text element

    private Rigidbody rb;
    private bool isGrounded;
    private bool isMoving;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;  // Save the starting position
        startRotation = transform.rotation;  // Save the starting rotation
        clickToStartText.enabled = true;  // Show the text at the start
    }

    void Update()
    {
        // Check for left mouse button click to start moving
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            clickToStartText.enabled = false;  // Hide the text when player starts moving
        }

        if (isMoving)
        {
            // Automatically move the player to the left
            rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
        }

        // Allow the player to jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Check if the player has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            ResetPosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ResetPosition()
    {
        transform.position = startPosition;  // Reset to the starting position
        transform.rotation = startRotation;  // Reset to the starting rotation
        rb.velocity = Vector3.zero;  // Reset velocity
        rb.angularVelocity = Vector3.zero;  // Reset angular velocity to stop any rotation
        isMoving = false;  // Stop movement until the player clicks again
        clickToStartText.enabled = true;  // Show the text again when player resets
    }
}
