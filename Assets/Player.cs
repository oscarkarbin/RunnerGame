using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float distance = 0;
    public float gravity;
    public float x_acc = 10;
    public float max_x_acc = 10;
    public float max_velocity = 60;
    public Vector2 velocity;
    public float ground  = 10;
    public float jumpVelocity = 20;
    public bool isOnGround = false;
    public bool holdJump = false;
    public float maxJumpTime = 0.4f;
    private float holdJumpTimer;
    private float groundThreshold = 3;
    public bool Attack = false;
    Animator anim1;


    // Start is called before the first frame update
    void Start()
    {
        anim1= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if(isOnGround || Mathf.Abs(pos.y - ground) <= groundThreshold )
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isOnGround = false;
                velocity.y = jumpVelocity;
                holdJump = true;
                holdJumpTimer = 0;
            }

        
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            holdJump = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack= true;
            anim1.SetTrigger("Attack");
            Invoke("SetBoolBack", 3);
        }

    }

    private void SetBoolBack()
    {
        Attack = false;
    }

    private void FixedUpdate(){

        Vector2 pos = transform.position;

        distance += velocity.x * Time.fixedDeltaTime;

        if(!isOnGround)
        {

            if(holdJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;

                if (maxJumpTime <= holdJumpTimer){
                    holdJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!holdJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
              
            if(pos.y <= ground){
                pos.y = ground;
                isOnGround = true;
            
            }
        }

        if(isOnGround){

            float velocityRatio = velocity.x/max_velocity;
            x_acc = max_x_acc * (1-velocityRatio);

            velocity.x += x_acc * Time.fixedDeltaTime;
            if(velocity.x >= max_velocity){
                velocity.x = max_velocity;
            }
        }

        transform.position = pos;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && Attack == false)
        {
            velocity.x = 0;  
            anim1.SetTrigger("Blood");
            Invoke("GameOver", 2);
           
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
        //Other stuff
    }
}
