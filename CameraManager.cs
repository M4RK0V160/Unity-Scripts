
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset =  new Vector3(0, 40, -100);


    private Vector2 _delta;

    private bool _isRotating;
    
    private float _xRotation;
    
    [SerializeField] private float horizontalRotationSpeed = 0.05f;
    [SerializeField] private float verticalRotationSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void LateUpdate()
    {

        transform.position = player.transform.position + offset;
       

        if (_isRotating)
        {
   
            transform.RotateAround(player.transform.position, Vector3.down,_delta.x * horizontalRotationSpeed);
            transform.RotateAround(player.transform.position, Vector3.right, -_delta.y * verticalRotationSpeed);
            offset =  transform.position - player.transform.position;
        }
        transform.LookAt(player.transform.position);


    }
    private void Awake()
    {
        transform.position = player.transform.position + offset;
        _xRotation = transform.rotation.eulerAngles.x;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _delta = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

}
