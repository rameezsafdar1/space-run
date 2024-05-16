using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public SwipeControl swip;
    private Transform Player;
    public Transform[] PlayerActive;
    public float movSpeed;
    private Rigidbody rb;

    private Vector3 velocityVariable;
    [Header("Lane Properties")]
    public float laneWidth;
    private int laneIndex = 0;
    public float changeLaneSpeed;
    public bool IsGrounded;
    public float JumpForce;
    private Vector3 moveAmount;
    private ConstantForce CF;
    public AudioClip[] MixSounds;
    private AudioSource AS;
    //Animation State
    const string Idle = "Idle_Wait_A";
    const string Run = "Mvm_Boost";
    const string Jump = "Jump_Down_A_Loop";
    const string JumpDown = "Land_Base_Move";
    const string Die = "Dam_StandDie_Root";
    const string SlideLeft = "Esc_Slide_Left";
    const string SlideRight = "Esc_Slide_Right";
    const string SlideStraight = "Esc_Slide_Loop_Mirror";
    private string currentState;
    public int _CurrentCoins, _TotalCoins,_BestScore;
    public TMP_Text CurrentCointText, TotalCoinsText, BestScoreText, BestScoreText2;
    [SerializeField]private GameObject MissionEndPanel,NewRecordObj;
    [SerializeField] private UnityEvent onDead;
    private float moveSpeedTimer;
    public void changeanimationstate(string newState)
    {
        //stop the same animation from intrupting itself
        if (currentState == newState) return;

        //play the animation
        PlayerActive[MainMenu.selectedPlayer].GetComponent<Animator>().Play(newState);

        //reassign the current state
        currentState = newState;
    }
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    // Start is called before the first frame update
    void Start()
    {
        _TotalCoins = PlayerPrefs.GetInt("coins", 0);
        _BestScore = PlayerPrefs.GetInt("best", 0);
        _CurrentCoins = 0;
        BestScoreText.text = _BestScore.ToString();
        BestScoreText2.text = _BestScore.ToString();
        TotalCoinsText.text = _TotalCoins.ToString();
        CurrentCointText.text = _CurrentCoins.ToString();
        rb = GetComponent<Rigidbody>();
        CF = GetComponent<ConstantForce>();
        AS = GetComponent<AudioSource>();
        Player = PlayerActive[MainMenu.selectedPlayer];
        Player.gameObject.SetActive(true);
        NewRecordObj.gameObject.SetActive(false);
        moveSpeedTimer = 0;
    }
    public void TapToStart()
    {
        StartCoroutine(StartRun());
    }
    IEnumerator StartRun()
    {
        PlayerHitFront.MissionState = 1;
        yield return new WaitForSeconds(2f);
        PlayerHitFront.MissionState = 2;
        IsGrounded = true;
        changeanimationstate(Run);
        rb.useGravity = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerHitFront.MissionState == 2)
        {
            if (movSpeed <= 15)
            {
                moveSpeedTimer += Time.deltaTime;
                if (moveSpeedTimer >= 30)
                {
                    movSpeed++;
                    moveSpeedTimer = 0;    
                }
            }
            Move();
            transform.Translate(Vector3.forward * movSpeed * Time.deltaTime);
            if (swip.SwipeLeft)
            {
                AS.PlayOneShot(MixSounds[1], 0.5f); //Slide Sound
                if (laneIndex == 0 || laneIndex == 1)
                {
                    laneIndex--;
                }
                if (IsGrounded)
                {
                    changeanimationstate(SlideRight);
                    StartCoroutine(slideanim());
                }
                
            }
            if (swip.SwipeRight)
            {
                AS.PlayOneShot(MixSounds[1], 0.5f); //Slide Sound
                if (laneIndex == 0 || laneIndex == -1)
                {
                    laneIndex++;
                }
                if (IsGrounded)
                {
                    changeanimationstate(SlideLeft);  
                    StartCoroutine(slideanim());
                }
                
            }
            if (swip.SwipeUp)
            {

                if (gameObject.transform.position.y <= 0.10f || IsGrounded)
                {
                    AS.PlayOneShot(MixSounds[0], 1f); //Slide Sound
                    rb.useGravity = false;
                    IsGrounded = false;
                    rb.velocity = Vector3.up * JumpForce;
                    changeanimationstate(Jump);
                    StartCoroutine(AfterJumpanim());
                }
            }
            if (transform.position.y > -2.26f && transform.position.y <= 1.4f)
            {
                rb.useGravity = false;
            }
            else if (IsGrounded)
            {
                rb.useGravity = true;
            }
            if (swip.SwipeDown)
            {
                if (gameObject.transform.position.y <= 0.038)
                {
                   // movSpeed = 15;
                    AS.PlayOneShot(MixSounds[1], 1f); //Slide Sound
                    changeanimationstate(SlideStraight);
                    StartCoroutine(StraighSlide());

                }
            }
            if (swip.SwipeDown && !IsGrounded)
            {
                CF.force = Vector3.down * 100f;
            }
        }
        else if (PlayerHitFront.MissionState == 3)//Die State
        {
            changeanimationstate(Die);
            rb.velocity = new Vector3(0, 4, 2);
            StartCoroutine(StopAnim());
            _TotalCoins += _CurrentCoins;
            if(_CurrentCoins>= _BestScore)
            {
                _BestScore = _CurrentCoins;
                NewRecordObj.gameObject.SetActive(true);
            }
            PlayerPrefs.SetInt("best", _BestScore);
            PlayerPrefs.SetInt("coins", _TotalCoins);
            BestScoreText.text = _BestScore.ToString();
            BestScoreText2.text = _BestScore.ToString();
            TotalCoinsText.text = _TotalCoins.ToString();
            MissionEndPanel.gameObject.SetActive(true);
            PlayerHitFront.MissionState = 4;
            onDead?.Invoke();
        }
    }

    private void Move()
    {

        moveAmount = velocityVariable * Time.deltaTime;
        float targetX = laneIndex * laneWidth;
        float dirX = Mathf.Sign(targetX - transform.position.x);
        float deltaX = changeLaneSpeed * dirX * Time.deltaTime;
        // Correct for overshoot
        if (Mathf.Sign(targetX - (transform.position.x + deltaX)) != dirX)
        {
            float overshoot = targetX - (transform.position.x + deltaX);
            deltaX += overshoot;
        }
        moveAmount.x = deltaX;
        transform.Translate(new Vector3(moveAmount.x * Time.deltaTime * 15, 0, 0));

    }
    IEnumerator StraighSlide()
    {
        yield return new WaitForSeconds(0.5f);
        if (PlayerHitFront.MissionState == 2)
        {
           // movSpeed = 8f;
            changeanimationstate(Run);
        }
    }
    IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(1f);
        PlayerActive[MainMenu.selectedPlayer].GetComponent<Animator>().enabled = false;
    }
    IEnumerator AfterJumpanim()
    {
        yield return new WaitForSeconds(0.8f);
        if (PlayerHitFront.MissionState == 2)
        {
            changeanimationstate(Run);
        }
    }
    IEnumerator slideanim()
    {
        yield return new WaitForSeconds(0.25f);
        if (PlayerHitFront.MissionState == 2)
        {
            changeanimationstate(Run);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && PlayerHitFront.MissionState == 2)
        {
            IsGrounded = true;
            CF.force = Vector3.down * 30f;
            rb.useGravity = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin" && PlayerHitFront.MissionState == 2)
        {
            _CurrentCoins++;
            other.GetComponent<MeshRenderer>().enabled = false;
            TotalCoinsText.text = _TotalCoins.ToString();
            CurrentCointText.text = _CurrentCoins.ToString();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
        if (!IsGrounded)
        {
            StopCoroutine(StraighSlide());
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
