using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luigi : MonoBehaviour {


    public float speed;
    public float boundarys;
    public float jumpchance;
    bool jumping;
    bool climbing;
    bool dead = false;
    Animator LuigiAnimator;
    Vector2 movement;
    Rigidbody2D body;


    // Use this for initialization
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        LuigiAnimator = GetComponent<Animator>();
        Spawn();
        StartCoroutine(JumpCheck());
    }

    // Update is called once per frame
    void Update()
    {
        Boundary();
    }

    public IEnumerator JumpCheck()
    {
        while (true)
        {
            int rand = Random.Range(0, 100);

            if (rand <= jumpchance && jumping == false && climbing == false && dead == false)
            {
                StartCoroutine(Jump(0.5f, 3));
            }

            yield return new WaitForSeconds(1);

        }
    }

    public IEnumerator Jump(float height, float jumpspeed)
    {
        jumping = true;
        float jumppower = 1;
        GetComponent<BoxCollider2D>().enabled = false;

        while (jumppower > 0)
        {
            body.velocity = new Vector2(movement.x, jumppower) * jumpspeed;

            yield return new WaitForSeconds(height / 100);
            jumppower = jumppower - 0.1f;

        }

        jumppower = -1;

        while (jumppower < 0)
        {
            body.velocity = new Vector2(movement.x, jumppower) * jumpspeed;

            yield return new WaitForSeconds(height / 100);
            jumppower = jumppower + 0.1f;

        }
        jumping = false;
        Move(movement);
        GetComponent<BoxCollider2D>().enabled = true;

    }

    public void Rotate(Vector2 movement)
    {


        if (movement == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        if (movement == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }


    }

    public void Boundary()
    {
        if (transform.position.x > boundarys || transform.position.x < -boundarys)
        {
            Destroy(gameObject);
        }

    }

    

    public void Spawn()
    {
        if (transform.position.x > 0)
        {
            Move(Vector2.left);
        }

        if (transform.position.x < 0)
        {
            Move(Vector2.right);
        }

    }

    public void Move(Vector2 movement)
    {
        this.movement = movement;
        body.velocity = movement * speed;
        Rotate(movement);


    }

}
