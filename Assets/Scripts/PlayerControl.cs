using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public GameObject hitPoint;
    public GameObject plantPrefab;
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 10f;
    public float groundCheckDistance = 1.1f;
    public float raycastDistance = 10f;

    private InputSystem_Actions inputActions;
    private Inventory inventory;
    private Rigidbody rb;
    private RaycastHit groundHit;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        CheckGround();
        Look();
        SelectSlot();
    }
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 targetVelocity = direction * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0f, velocity.z);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Look()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        inputActions.Player.Jump.performed += ctx => Jump();

        // raycast
        if(inputActions.Player.Attack.triggered && Physics.Raycast(cameraTransform.position, cameraTransform.forward, out groundHit, raycastDistance))
        {
            GameObject hitObject = groundHit.collider.gameObject;

            CheckObject(hitObject);

            hitPoint.transform.position = groundHit.point;
        }
    }

    private void SelectSlot()
    {
        if (inputActions.UI.Slot1.triggered) inventory.selectedSlot = 0;
        else if (inputActions.UI.Slot2.triggered) inventory.selectedSlot = 1;
        else if (inputActions.UI.Slot3.triggered) inventory.selectedSlot = 2;
        else if (inputActions.UI.Slot4.triggered) inventory.selectedSlot = 3;
        else if (inputActions.UI.Slot5.triggered) inventory.selectedSlot = 4;
        else if (inputActions.UI.Slot6.triggered) inventory.selectedSlot = 5;
        else if (inputActions.UI.Slot7.triggered) inventory.selectedSlot = 6;
        else if (inputActions.UI.Slot8.triggered) inventory.selectedSlot = 7;
        else if (inputActions.UI.Slot9.triggered) inventory.selectedSlot = 8;
        else if (inputActions.UI.Slot10.triggered) inventory.selectedSlot = 9;
    }

    private void CheckObject(GameObject hitObject)
    {
        switch (hitObject.tag)
        {
            case "Soil":
                if (hitObject.transform.childCount <= 1)
                {
                    Instantiate(plantPrefab, hitObject.transform.position, Quaternion.identity, hitObject.transform);
                }

                break;
            case "Plant":
                PlantControl plantScript = hitObject.transform.parent.gameObject.GetComponent<PlantControl>();

                if (plantScript != null && plantScript.growTimer == plantScript.growTime)
                {
                    plantScript.PickupPlant();
                }

                break;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
