using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runMultiplier = 5f;
    private float currentSpeed;
    private PlayerActionControls playerActionControls;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Read the movement value
        float movementInputX = playerActionControls.Land.Move.ReadValue<float>();
        float movementInputZ = playerActionControls.Land.Move2.ReadValue<float>();
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Adjust speed based on whether the player is running
        if (isRunning)
        {
            currentSpeed = speed * runMultiplier; // Running speed when Shift is pressed
        }
        else
        {
            currentSpeed = speed; // Normal walking speed
        }

        //Move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInputX * currentSpeed * Time.deltaTime;
        currentPosition.z += movementInputZ * currentSpeed * Time.deltaTime;
        transform.position = currentPosition;

        // Animation 
        if (movementInputX != 0 || movementInputZ != 0) 
            animator.SetBool("Walk", true);
        else 
            animator.SetBool("Walk", false);

        //Sprite Flip
        if (movementInputX == -1)
            spriteRenderer.flipX = true;
        else if (movementInputX == 1)
            spriteRenderer.flipX = false;

    }
}
