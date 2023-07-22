
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update


    InputActions inputActions;
    private Rigidbody rb;

    //movement
    [SerializeField] public float moveSpeed;
    private Vector2 moveVector = new Vector2(0,0);

    //Jumping
    public float jumpForce;
    public float jumpingDriftSpeed;
    public float gravityModifier;
    public float JumpingMoveSpeedTranslationConstant;
    public int maxJumps = 2;


    
    private int canJump;
    private bool isOnGround = false;

    //dashing
    public float dashForce;
    public int maxDashes = 1;
    public float dashLength = 4;

    private int canDash;
    private bool dashing = false;
    private float dashTime = -1;


    //wallCollisiion
    void Start()
    {
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!dashing)
        {
            Move();
                }
        else
        {
            if(Time.time > dashTime)
            {
                dashing = false;
                rb.useGravity = true;
            }
        }
    }

    private void Jump()
    {
        if (canJump > 0)
        {
               
            rb.velocity = Vector3.zero;                 
            rb.AddForce(Vector2.up * jumpForce + moveVector * JumpingMoveSpeedTranslationConstant * moveSpeed, ForceMode.Impulse);
            canJump--;
            isOnGround = false;

        }

    }

    private void Dash()
    {
        if (canDash > 0)
        {
            dashTime = Time.time + dashLength;
            rb.useGravity = false;
            dashing = true;
            Debug.Log("Dashing");
            rb.velocity = Vector3.zero;
            rb.AddForce(moveVector * dashForce, ForceMode.Impulse);       
            canDash--;

        }
    }

    private void Move()
    {
        switch(isOnGround)
        {
           case false:
                transform.Translate(new Vector2(moveVector.normalized.x, 0).normalized * Time.deltaTime * jumpingDriftSpeed);
                break;
            case true:
                transform.Translate(new Vector2(moveVector.normalized.x, 0).normalized * Time.deltaTime * moveSpeed);
                break;

            default:
                break;

        }
    }

    
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.PlayerInput.Jump.performed += ctx => Jump();
        inputActions.PlayerInput.Move.performed += ctx => moveVector = ctx.ReadValue<Vector2>();
        inputActions.PlayerInput.Move.canceled += ctx => moveVector = Vector2.zero;
        inputActions.PlayerInput.Dash.performed += ctx => Dash();
    }
    private void OnEnable()
    {
        inputActions.PlayerInput.Enable();

    }

    private void OnDisable()
    {
        inputActions.PlayerInput.Disable(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canJump = maxJumps;
            canDash = maxDashes;
        }
    }
}
