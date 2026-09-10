using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    private new Camera camera;
    private Rigidbody rigid;
    [SerializeField] private PlayerAttack attackController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 c_forward;
    private Vector3 c_right;
    private Vector3 direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        camera = Camera.main;
        rigid = GetComponent<Rigidbody>();

        c_forward = camera.transform.forward;
        c_right = camera.transform.right;

        c_forward.y = 0;
        c_right.y = 0;

        c_forward.Normalize();
        c_right.Normalize();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDirection = c_right * input.x + c_forward * input.y;
    }

    public void OnLook(InputValue value)
    {
        Vector2 mousePosition = value.Get<Vector2>();

        Ray ray = camera.ScreenPointToRay(mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 targetPos = hit.point;

            Vector3 lookDirection = targetPos - transform.position;
            lookDirection.y = 0;

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

    }

    public void OnAttack(InputValue value)
    {
        Debug.Log("attack");
        attackController.Attack();
    }

    void FixedUpdate()
    {
        direction = moveDirection * speed;
        rigid.linearVelocity = new Vector3(direction.x, rigid.linearVelocity.y, direction.z);
    }
}
