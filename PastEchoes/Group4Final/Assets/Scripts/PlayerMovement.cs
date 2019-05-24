using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public GameObject Camera;
    private ThrowBall throwBallScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        throwBallScript = Camera.GetComponent<ThrowBall>();

        Cursor.visible = false;
    }

    void Update()
    {
        if (GameHandler.Instance.gameState == GameHandler.gameStates.navigating)
        {
            Cursor.visible = false;

            float horiMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horiMovement, 0.0f, vertMovement);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            // Apply gravity
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

            // Move the controller
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            
        }
        
        if(GameHandler.Instance.gameState == GameHandler.gameStates.selection)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            throwBallScript.resetBall(collision.gameObject);
        }
    }
}