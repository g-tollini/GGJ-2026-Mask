// ChatGPT

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class IsoTPSController : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Glisse ici ta Camera (Transform). Elle suivra le joueur en gardant l'offset initial.")]
    public Transform followCamera;

    [Tooltip("Lissage du suivi caméra. 0 = instant, >0 = plus smooth.")]
    public float cameraFollowSmooth = 12f;

    private Vector3 cameraOffset;
    private Quaternion cameraRotation;

    private CharacterController cc;

    [Header("Movement")]
    public float moveSpeed = 5.5f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    [Header("Punch")]
    public float punchRange = 1f;
    public float punchSphereRadius = 1f;
    public float punchForce = 500f;

    [Header("Facing")]
    [Tooltip("Si ON, le perso s'oriente vers la souris (raycast).")]
    public bool faceMouse = true;

    [Tooltip("Vitesse de rotation vers la souris.")]
    public float rotationSpeed = 18f;

    [Tooltip("Layers utilisés pour viser (ton sol).")]
    public LayerMask aimMask = ~0;

    [Tooltip("Distance max du raycast souris.")]
    public float aimMaxDistance = 500f;

    [Header("Gravity")]
    [Tooltip("Gravité négative. Ex: -9.81 à -25")]
    public float gravity = -18f;
    [Tooltip("Force légère vers le bas pour rester collé au sol.")]
    public float groundedStickForce = -2f;
    public float groundedTolerance = 0.25f;

    [Header("Jump (optional)")]
    public bool enableJump = true;
    public float jumpHeight = 1.2f;

    [Header("Ground Mask")]
    [Tooltip("Layers considérés comme sol pour le check grounded.")]
    public LayerMask groundMask = ~0;

    [Header("Pushing")]
    public bool enablePush = true;
    public float pushPower = 2.0f;

    // Input state (New Input System)
    private Vector2 moveInput;
    private bool jumpPressed;

    // Motion state
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        if (followCamera == null && Camera.main != null)
            followCamera = Camera.main.transform;
    }

    void Start()
    {
        if (followCamera != null)
        {
            cameraOffset = followCamera.position - transform.position;
            cameraRotation = followCamera.rotation;
        }

        InGameUI.enabled = false;
    }

    void Update()
    {
        // 1) Mouvement relatif à la caméra (isométrique)
        Vector3 camForward = Vector3.forward;
        Vector3 camRight = Vector3.right;

        if (followCamera != null)
        {
            camForward = followCamera.forward;
            camRight = followCamera.right;
        }

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredMoveDir = (camRight * moveInput.x + camForward * moveInput.y);
        if (desiredMoveDir.sqrMagnitude > 0.0001f)
            desiredMoveDir.Normalize();

        // 2) Sol
        bool grounded = IsGrounded();

        // 3) Gravité + saut
        if (grounded && verticalVelocity < 0f)
            verticalVelocity = groundedStickForce;
        else
            verticalVelocity += gravity * Time.deltaTime;

        if (enableJump && grounded && jumpPressed)
        {
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravity);
        }
        jumpPressed = false;

        // 4) Accélération / décélération horizontale
        Vector3 targetHorizontal = desiredMoveDir * moveSpeed;
        float accel = (targetHorizontal.sqrMagnitude > 0.001f) ? acceleration : deceleration;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetHorizontal,
            accel * Time.deltaTime
        );

        // 5) Rotation: vers la souris (si activé), sinon vers la direction de mouvement
        if (faceMouse)
        {
            RotateTowardMouse();
        }
        else
        {
            RotateTowardMovement();
        }

        // 6) Déplacement
        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;
        cc.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        if (followCamera == null) return;

        Vector3 targetPos = transform.position + cameraOffset;

        if (cameraFollowSmooth <= 0f)
        {
            followCamera.position = targetPos;
        }
        else
        {
            followCamera.position = Vector3.Lerp(
                followCamera.position,
                targetPos,
                1f - Mathf.Exp(-cameraFollowSmooth * Time.deltaTime)
            );
        }

        // Garde l'orientation isométrique fixe
        followCamera.rotation = cameraRotation;
    }

    private void RotateTowardMouse()
    {
        if (followCamera == null) return;

        // Position souris (New Input System)
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main != null
            ? Camera.main.ScreenPointToRay(mousePos)
            : new Ray(followCamera.position, followCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, aimMaxDistance, aimMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 lookPoint = hit.point;
            Vector3 dir = lookPoint - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Fallback: si pas de hit (aimMask mal réglé), ne change pas la rotation
            // (ou tu peux viser un plan à hauteur du joueur si tu préfères)
        }
    }

    private void RotateTowardMovement()
    {
        Vector3 faceDir = horizontalVelocity;
        faceDir.y = 0f;

        if (faceDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(faceDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + cc.center;
        float radius = cc.radius * 0.95f;

        float bottom = origin.y - (cc.height * 0.5f) + radius;
        Vector3 spherePos = new Vector3(origin.x, bottom + 0.02f, origin.z);

        return Physics.SphereCast(
            spherePos,
            radius,
            Vector3.down,
            out _,
            groundedTolerance,
            groundMask,
            QueryTriggerInteraction.Ignore
        );
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!enablePush) return;

        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb == null || rb.isKinematic) return;

        // Ne pousse pas le sol
        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

        // Version compatible partout (velocity)
        rb.linearVelocity = new Vector3(
            pushDir.x * pushPower,
            rb.linearVelocity.y,
            pushDir.z * pushPower
        );
    }

    // ---- PlayerInput (Send Messages) ----
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);
    }

    public void OnJump(InputValue value)
    {
        if (!enableJump) return;
        if (value.isPressed) jumpPressed = true;
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log($"punch {value}");
            var center = transform.position + transform.forward * punchRange ;
            var hitColliders = Physics.OverlapSphere(center, punchSphereRadius);
            foreach (var collider in hitColliders)
            {
                var body = collider.attachedRigidbody;
                if (body != null)
                {
                    Debug.Log($"body touched {body.name}");
                    body.AddForce(transform.forward * punchForce);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var center = transform.position + transform.forward * punchRange;
        Gizmos.DrawWireSphere(center, punchSphereRadius);
    }

    public Canvas InGameUI;

    public void OnEscape(InputValue value)
    {
        InGameUI.enabled = !InGameUI.enabled;
        GetComponent<PlayerInput>().SwitchCurrentActionMap(InGameUI.enabled ? "UI" : "Player");
    }

    public void MainMenu()
    {
        var mainMenu = FindFirstObjectByType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.enabled = true;
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Game");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
