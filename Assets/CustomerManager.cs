using System.Threading;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Sprite customerAngry0;
    // Start is called before the first frame update
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private int frameCount = 0;
    private int secondsPassed = 0;
    private bool customerActive = false;
    private bool startTimer = false;
    private bool automaticReleased = false;
    private bool changeSprite = false;
    private bool isCustomerAngry0 = false;
    private Semaphore sem;
    Thread timerThread;
    Thread releaseThread;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        sr.enabled = false;
        bc.enabled = false;
        //StartCoroutine(showCustomer());
        sem = new Semaphore(initialCount: 0, maximumCount: 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frameCount++;
        if(frameCount == 60)
        {
            secondsPassed++;
            frameCount = 0;
        }
        if(secondsPassed >= 5)
        {
            if(!customerActive)
            {
                sr.enabled = true;
                bc.enabled = true;
                customerActive = true;
                startTimer = true;
            }
            /*
            if(startTimer)
            {
                //StartCoroutine(waitTimer());
                timerThread = new Thread(waitTimer);
                releaseThread = new Thread(automaticRelease);
                timerThread.Start();
                releaseThread.Start();
                

                startTimer = false;
            }
            if (changeSprite)
            {
                sr.color = new Color(1, 1, 1, 0);
                sr.transform.position = new Vector3(sr.transform.position.x, sr.transform.position.y + 0.43F,0);
                sr.sprite = customerAngry0;
                sr.color = new Color(1, 1, 1, 1);
                changeSprite = false;
            }
            */
        }
        if(secondsPassed >= 10)
        {
            if(!isCustomerAngry0)
            {
                sr.color = new Color(1, 1, 1, 0);
                sr.transform.position = new Vector3(sr.transform.position.x, sr.transform.position.y + 0.43F, 0);
                sr.sprite = customerAngry0;
                sr.color = new Color(1, 1, 1, 1);
                isCustomerAngry0 = true;
            }
        }
    }

    private void waitTimer()
    {
        sem.WaitOne();
        if(automaticReleased)
        {
            //customer not served
            changeSprite = true;
        }
        else
        {
            //customer served
        }
    }

    private void automaticRelease()
    {
        Thread.Sleep(1000 * 5);
        automaticReleased = true;
        sem.Release(1);
    }
}

