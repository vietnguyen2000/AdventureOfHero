using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{

    public float speedX;
    public float speedY;
    public float distance;
    public float size;
    private Transform backgroundtransform;
    private Transform maincamera;
    private Rigidbody2D player;
    private Rigidbody2D backgroudrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        backgroundtransform = gameObject.transform;
        maincamera = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        backgroudrigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move();
        jump();
    }
    private void move()
    {
        Vector2 v = player.velocity;
        backgroudrigidbody.velocity = new Vector2(v.x * speedX, v.y*speedY);
    }
    private void jump()
    {
        if (backgroundtransform.position.x - maincamera.position.x <= -distance)
        {
            backgroundtransform.position = new Vector3(backgroundtransform.position.x + size, backgroundtransform.position.y, backgroundtransform.position.z);
        }
        else if (backgroundtransform.position.x - maincamera.position.x >= distance)
        {
            backgroundtransform.position = new Vector3(backgroundtransform.position.x - size, backgroundtransform.position.y, backgroundtransform.position.z);
        }
    }
}
