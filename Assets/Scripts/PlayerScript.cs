using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2D;
    public float speed;
    public Text score;
    public Text win;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;


    // Start is called before the first frame update
    void Start()
    {
       rd2D = GetComponent<Rigidbody2D>();
       score.text = "Count: " + scoreValue.ToString();
       win.text = "";
       SetLivesText();
       musicSource.clip = musicClipOne;
       musicSource.Play();
       anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2D.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

         if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (isOnGround == false && verMovement > 0)
        {
            anim.SetInteger("State", 2);
        }
        
        else if (isOnGround == true && hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 4)
            {
            transform.position = new Vector2(70.0f, 0.0f);
            livesValue = 3;
            SetLivesText();
            }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }

        if(livesValue == 0)
        {
            win.text = "You Lose!";
            Destroy(this);
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }

        if(scoreValue == 8)
        {
            win.text = "You Win! Created by Austin Michaud";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(this);
        }
        
    }

    void SetLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
        {

        rd2D.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
    }
}

}


    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
