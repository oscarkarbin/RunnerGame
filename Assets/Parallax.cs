using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public float depth = 1;

    Player player;

    private void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }
    //-18.69  42.5
    // Update is called once per frame
    void Update()
    {
        
        float realvelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realvelocity * Time.fixedDeltaTime;

        if(pos.x <= -30)
        {
            pos.x = 88;
        }


        transform.position = pos;
    }
}
