using UnityEngine;


//  Uses the collider to check directions to see if the object is currently on the ground, touching the wall, or touching the ceiling
public class TouchingDirections : MonoBehaviour
{

    Rigidbody2D rb;
    CapsuleCollider2D bodyCollider;
    Animator animator;

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnCeiling;


    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        IsGrounded = bodyCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = bodyCollider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = bodyCollider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }

    public bool IsGrounded
    {
        get { return _isGrounded; }
        set 
        { 
            _isGrounded = value; 
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    public bool IsOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }
}
