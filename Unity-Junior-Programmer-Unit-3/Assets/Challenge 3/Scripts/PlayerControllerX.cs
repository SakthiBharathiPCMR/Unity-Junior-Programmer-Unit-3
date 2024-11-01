using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce;

    private float gravityModifier = 1.5f;
    private float yMax = 16f;
    public bool isLower;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip groundBounceSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && isLower&& playerRb.velocity.y < 5f)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
      
        }

        isLower = transform.position.y < yMax;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameOver) return;

        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground"))
        {
            playerAudio.PlayOneShot(groundBounceSound, 1.0f);
            playerRb.AddForce(Vector3.up * floatForce/2, ForceMode.Impulse);

        }


    }

}
