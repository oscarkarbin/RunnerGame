using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    

    Player player;
    public float ratio = 10;
    // Start is called before the first frame update
    private void Awake()
    {

        player = GameObject.Find("Player").GetComponent<Player>();
     

    }

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = player.velocity.x / ratio;
        Vector2 pos = transform.position;

        pos.x -= movementSpeed * Time.fixedDeltaTime;

        if(pos.x <= -50) 
        {
            Destroy(gameObject);
        }

        transform.position = pos;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && player.Attack)
        {
            Destroy(gameObject);
        }
    }
}
