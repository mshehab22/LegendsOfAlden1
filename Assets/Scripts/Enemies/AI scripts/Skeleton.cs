using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    Rigidbody2D rb;
    Touching_Directions touchingDirections; 

    public enum WalkableDirection { Left, Right }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);  
                if(value == WalkableDirection.Right)
                { 
                    walkDirectionVector = Vector2.right;
                }
                
               
                    else if (value == WalkableDirection.Left)
                    {
                        walkDirectionVector = Vector2.left;
                    }
                
            }
            _walkDirection = value; }
        
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<Touching_Directions>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(walkSpeed * Vector2.right.x, rb.linearVelocity.y);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
