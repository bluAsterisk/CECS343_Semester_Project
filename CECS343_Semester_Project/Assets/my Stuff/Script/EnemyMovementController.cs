using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovementController : MonoBehaviour
{
    // Movement Variables
    [SerializeField]
    private float enemySpeed;
    private float move;

    // facing
    public GameObject enemyGraphic;
    bool canFlip = true;
    bool facingRight = false;
    float flipTime = 5f;
    float nextFlipChance = 0f;

    //attacking
    public float chargeTime;
    float startChargeTime;
    bool charging;
    // Body
    public
    Transform enemyT;
    public
    Rigidbody2D enemyRB;
    public
    Animator enemyAnim;
    

    public float detectRad = 5f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        enemyT = transform.parent;
        enemyAnim = GetComponentInParent<Animator>();
        enemyRB = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            print("Target not null");
            float distance = Vector2.Distance(target.position, transform.position);
            if (distance <= detectRad)
            {
                print("Set destination");
                SetDestination(target.position);
            }
            if (enemyRB.velocity.x > 0 && !facingRight)
            {
                flip();
            }
            else if (enemyRB.velocity.x < 0 && facingRight)
            {
                flip();
            }
            enemyAnim.SetFloat("speed", Mathf.Abs(enemyRB.velocity.x)); // "speed" is variable for Animator
    }

    void SetDestination(Vector2 target)
    {
        float x = target.x - transform.position.x;
        //enemyRB.AddForce(new Vector2(x, 0) * enemySpeed);
        
        if (x < 0)
        {
            enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
            enemyRB.velocity = Vector2.ClampMagnitude(enemyRB.velocity, enemySpeed);
        }
        else if (x > 0)
        {
            enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
            enemyRB.velocity = Vector2.ClampMagnitude(enemyRB.velocity, enemySpeed);
        }
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRad);

    }
    
    void flip()
    {
        facingRight = !facingRight; // Set reverse boolean
        Vector2 eScale = enemyT.localScale;
        eScale.x *= -1;
        enemyT.localScale = eScale;
    }
}
