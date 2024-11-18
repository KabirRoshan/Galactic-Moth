// This script contains the move controls by touch 
// It also contains the trigger function so the main body 
// will know what to do when a collision occurs.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class MoveByTouch : MonoBehaviour {

    // Speed at which the main body moves at
    [SerializeField]
    float moveSpeed = 15f;

    // Reference to animator
    public Animator animator;

    Rigidbody2D rb;

    Touch touch;
    Vector3 touchPosition, whereToMove;
    bool isMoving = false;

    // Slow speed timer
    // Slow speed velocity
    // Slow speed detection
    //public float slowBallTime = 5.0f;
    public float slowBallSpeed = 5.0f;
    public bool speedDecreased = true;
    public float currentSlowTime;
    public float speed;
    public GameObject gameOverText, restartButton, returnHomeButton, PointSparkle, SlowSparkle, TheLight, Explosion, HighScoreText;

    float previousDistanceToTouchPos, currentDistanceToTouchPos;

    // ADVERTISEMENT GUWOP BABY
    private string playStoreID = "3560572";
    private string videoAdvert = "video";

    public bool isTargetPlayStore;
    public bool isTestAd;

    // DoubleTap variables
    int TapCount;

    void Start()
    {
        InitializeAdvertisement();

        // Initialise variables here
        rb = GetComponent<Rigidbody2D>();
        

        // Disabling UI so it is only shown when player dies
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        returnHomeButton.SetActive(false);
        HighScoreText.SetActive(false);
    } // Start

    // Update is called once per frame
    void Update() {
        //bool working = false; //flag to avoid another execution
        // Current distance to touch position is calculated
        if (isMoving)
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;

        // Detecting first touch
        // > 0
        if (Input.touchCount > 0)
        {
            // Getting info on first touch
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Reset the previous and current distances to 0
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                // Change from pixels to world units
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                // Z axis value to zero because it will otherwise take cameras z value.
                touchPosition.z = 0;
                // Direction to move the body and the velocity it should move at
                whereToMove = (touchPosition - transform.position).normalized;
                rb.velocity = new Vector2(whereToMove.x * moveSpeed, whereToMove.y * moveSpeed);
                // CREATE THE LIGHT
                Instantiate(TheLight, touchPosition, Quaternion.identity);
            } // if
        } // if

        // Body reaches finish point
        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        } // if

        if (isMoving)
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;

        // DOUBLE TOUCH
        // for each touch in array touches if tap count detected is 2
        // teleport mainBody to that location
        // player can only teleport if score is even not including 0
        foreach (Touch touch in Input.touches)
        {
            if (touch.tapCount == 2 && ScoreScript.scoreValue > 0 && ScoreScript.scoreValue % 2 == 0)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                transform.position = touchPosition;

                // reduce points by 2
                // POINTS NOT SUBTRACTING CORRECTLY?!?!?
                //Debug.Log(ScoreScript.scoreValue -= 2);
                break;
            } // if
        } // for

        animator.SetBool("isItMoving", isMoving); 

    } // Update

    // When main body collides with a ball
    // Using Trigger instead of Collision
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit detected");
        // Collision detectors
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            // turn on UI for when player dies
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            returnHomeButton.SetActive(true);
            HighScoreText.SetActive(true);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            // Sound
            FindObjectOfType<AudioManager>().Play("Explosion");
            //Destroy(gameObject);
            //Explosion.SetActive(true);
            gameObject.SetActive(false);
        } // if
        else if (collision.gameObject.tag.Equals("EnemyCow"))
        {
            // turn on UI for when player dies
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            returnHomeButton.SetActive(true);
            HighScoreText.SetActive(true);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            // Sound
            FindObjectOfType<AudioManager>().Play("Explosion");
            FindObjectOfType<AudioManager>().Play("Cow");
            //Destroy(gameObject);
            //Explosion.SetActive(true);
            gameObject.SetActive(false);
        } // else if
        else if (collision.gameObject.tag.Equals("AdBall"))
        {
            // Ads
            StartCoroutine(PlayVideoAd());
            Debug.Log("Is Test Ad: " + isTestAd);
            Debug.Log("Is Target Play Store: " + isTargetPlayStore);
            AudioListener.pause = true;
            // turn on UI for when player dies
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            returnHomeButton.SetActive(true);
            HighScoreText.SetActive(true);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            // Sound
            FindObjectOfType<AudioManager>().Play("Explosion");
            //Destroy(gameObject);
            //Explosion.SetActive(true);
            gameObject.SetActive(false);
        } // else if
        else if (collision.gameObject.tag.Equals("Points"))
        {
            ScoreScript.scoreValue += 1;
            Instantiate(PointSparkle, transform.position, Quaternion.identity);
            // Sound
            FindObjectOfType<AudioManager>().Play("Bell");
            // Removes point object from game
            Destroy(collision.gameObject);
        } // else if
        else if (collision.gameObject.tag.Equals("SlowBall"))
        {
            speedDecreased = true;
            Instantiate(SlowSparkle, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("SlowDownSound");
            Destroy(collision.gameObject);
            StartCoroutine(EffectBallWearOff(5.0f));
        } // else if
    } // OnCollisionEnter2D

    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID, isTestAd);
            return;
        } // if
    } // InitializeAvertisement

    IEnumerator PlayVideoAd()
    {
        if(!Advertisement.IsReady(videoAdvert))
        {
            yield return new WaitForSeconds(1);
        }
        Advertisement.Show(videoAdvert);       
        
    } // PlayVideoAd

    // Sets time for the slow ball
    IEnumerator EffectBallWearOff (float waitTime)
    {
        moveSpeed = slowBallSpeed; // speed decrease applied
        yield return new WaitForSeconds(waitTime);
        moveSpeed = 15f; // speed decrease disabled
        speedDecreased = false;
    } // EffectBallWearOff
 
} // Class MoveByTouch
