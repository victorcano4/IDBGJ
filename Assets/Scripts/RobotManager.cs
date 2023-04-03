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
    private bool somethingInCuttingTable;
    private SpriteRenderer onion;
    private SpriteRenderer tomato;
    private SpriteRenderer robotTimer;
    public Sprite onion_cut;
    public Sprite tomato_cut;
    public Sprite robotTimer0;
    public Sprite robotTimer1;
    public Sprite robotTimer2;
    public Sprite robotTimer3;
    public Sprite robotTimer4;
    public Sprite robotTimer5;
    public Sprite robotTimer6;
    public Sprite robotTimer7;
    private string processedFoodGrabbed;
    private string foodInTable;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        onion = GameObject.Find("onion").GetComponent<SpriteRenderer>();
        tomato = GameObject.Find("tomato").GetComponent<SpriteRenderer>();
        robotTimer = GameObject.Find("timer").GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!blockMovement)
        {
            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(-Velocity, 0));
            }
            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(Velocity, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, Velocity));
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, -Velocity));
            }
        }
       
        if(Input.GetKeyDown(KeyCode.Space))
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
            if (canCut && somethingInCuttingTable)
            {
                processedFoodGrabbed = foodInTable;
                tomato.color = new Color(1f, 1f, 1f, 0f);
                onion.color = new Color(1f, 1f, 1f, 0f);
                somethingInCuttingTable = false;
            }
            if (canCut && servingOnion && !somethingInCuttingTable)
            {
                StartCoroutine(cutOnions());
                somethingInCuttingTable = true;
                foodInTable = "onion";
            }
            if (canCut && servingTomato && !somethingInCuttingTable)
            {
                StartCoroutine(cutTomato());
                somethingInCuttingTable = true;
                foodInTable = "tomato";
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
        robotTimer.enabled = true;
        animator.SetBool("carrying_onion", false);
        servingOnion = false;
        //show onion
        onion.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer1;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer2;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer3;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer4;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer5;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer6;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer7;
        yield return new WaitForSeconds(0.375F);
        robotTimer.enabled = false;
        robotTimer.sprite = robotTimer0;
        //hide onion
        onion.sprite = onion_cut;
        blockMovement = false;
    }
    private IEnumerator cutTomato()
    {
        blockMovement = true;
        robotTimer.enabled = true;
        animator.SetBool("carrying_tomato", false);
        servingTomato = false;
        //show onion
        tomato.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer1;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer2;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer3;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer4;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer5;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer6;
        yield return new WaitForSeconds(0.375F);
        robotTimer.sprite = robotTimer7;
        yield return new WaitForSeconds(0.375F);
        robotTimer.enabled = false;
        robotTimer.sprite = robotTimer0;
        //hide onion
        tomato.sprite = tomato_cut;
        blockMovement = false;
    }
}
