using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    public float Velocity;
    private Animator animator;
    private bool canGetOnions;
    private bool canGetTomato;
    private bool servingOnion;
    private bool servingTomato;
    private bool canCut;
    private bool blockMovement;
    private SpriteRenderer onion;
    private SpriteRenderer tomato;
    public Sprite onion_cut;
    public Sprite tomato_cut;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        onion = GameObject.Find("onion").GetComponent<SpriteRenderer>();
        tomato = GameObject.Find("tomato").GetComponent<SpriteRenderer>();
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
            if (canGetOnions)
            {
                animator.SetBool("carrying_onion", true);
                servingOnion = true;
            }
            if (canGetTomato)
            {
                animator.SetBool("carrying_tomato", true);
                servingTomato = true;
            }
            if(canCut && servingOnion)
            {
                StartCoroutine(cutOnions());
            }
            if (canCut && servingTomato)
            {
                StartCoroutine(cutTomato());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "onion_dispenser")
            canGetOnions = true;
        if (collision.name == "tomato_dispenser")
            canGetTomato = true;
        if (collision.name == "knife_table" && (servingOnion || servingTomato))
            canCut = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "onion_dispenser")
            canGetOnions = false;
        if (collision.name == "tomato_dispenser")
            canGetTomato = false;
        if (collision.name == "knife_table")
            canCut = false;
    }
    private IEnumerator cutOnions()
    {
        blockMovement = true;
        animator.SetBool("carrying_onion", false);
        servingOnion = false;
        //show onion
        onion.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(3);
        //hide onion
        onion.sprite = onion_cut;
        blockMovement = false;
    }
    private IEnumerator cutTomato()
    {
        blockMovement = true;
        animator.SetBool("carrying_tomato", false);
        servingTomato = false;
        //show onion
        tomato.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(3);
        //hide onion
        tomato.sprite = tomato_cut;
        blockMovement = false;
    }
}
