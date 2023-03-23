using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 1f;
    public Rigidbody playerRb;
    
    public float gravityModifier;

    public bool gameOver = false;
    public bool isOnGround = true;
    private bool doubleJumpUsed = false;
    [SerializeField]
    private float doubleJumpForce;

    
    public bool doubleSpeed = false;



    //Animation and particle variables
    private Animator playerAnim;
    [SerializeField]
    private ParticleSystem playerDirtParticle;
    [SerializeField]
    private ParticleSystem deathExplosion;


    //Audio and sfx variables
    private AudioSource playerAudio;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip crashSound;

    public static PlayerController instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
   

    }

    // Update is called once per frame
    void Update()
    {

        playerControl();
        
    }

    void playerControl()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            doubleSpeed = true;

            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver == false)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerDirtParticle.Stop();

            playerAudio.PlayOneShot(jumpSound, 1f);

            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed && gameOver == false)
        {
            doubleJumpUsed = true;
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            playerDirtParticle.Play();
        }


        if(collision.gameObject.CompareTag("Obsticle"))
        {
            gameOver = true;
            Debug.Log("GameOver");
            GameOver();
            Destroy(collision.gameObject,0.5f);
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ScoreCollider"))
        {
            GameManager.instance.IncrementScore(5);
        }
    }





    void GameOver()
    {
        if(gameOver == true)
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            deathExplosion.Play();

            playerDirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1f);

            GameManager.instance.GameOver();
        }
    }




    public void ResetGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0); 
    }


  
}
