using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    public float Velocity;
    private Animator animator;
    private bool canGetFood;
    private bool serving;
    private bool canCook;
    private bool blockMovement;
    private SpriteRenderer onion;
    public Sprite onion_cut;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        onion = GameObject.Find("onion").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!blockMovement)
        {
            if (Input.GetKey("left"))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(-Velocity, 0));
            }
            if (Input.GetKey("right"))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(Velocity, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, Velocity));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, -Velocity));
            }
        }
       
        if(Input.GetKey(KeyCode.Space))
        {
            if (canGetFood)
            {
                animator.SetBool("serving", true);
                serving = true;
            }
            if(canCook)
            {
                StartCoroutine(cutOnions());// cutOnions();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "fridge")
            canGetFood = true;
        if (collision.name == "cooking_machines" && serving)
            canCook = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "fridge")
            canGetFood = false;
        if (collision.name == "cooking_machines")
            canCook = false;
    }
    private IEnumerator cutOnions()
    {
        blockMovement = true;
        animator.SetBool("serving", false);
        //show onion
        onion.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(3);
        //hide onion
        onion.sprite = onion_cut;
        blockMovement = false;
    }
}
