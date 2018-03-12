using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gracz : MonoBehaviour {

    public float moveForce = 30;
    public float jumpForce = 600;
    public float maxSpeed = 2;
    private Animator animator;

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip gameOverSound;
    public AudioClip throwShuriken;

    [SerializeField]
    private GameObject shurikenPrefab;

    float x = 0;
    bool j = false;
    bool a = false;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
	}

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(x));
        if (x != 0) 
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        j = Input.GetKeyDown(KeyCode.Space);
        if (j)
            animator.SetTrigger("Jump");
        a = Input.GetKeyDown(KeyCode.W);
        if (a)
        {
            animator.SetTrigger("Attack");
            ThrowShuriken(0);
            SoundManager.instance.RandomizeSfx(throwShuriken);
        }

    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(x * moveForce, j ? jumpForce : 0));

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }

        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }
    }

    public void ThrowShuriken (int value)
    {
        if (transform.localScale.x > 0)
        {
            GameObject tmp = (GameObject)Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<Shuriken>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<Shuriken>().Initialize(Vector2.left);
        }
    }
}
