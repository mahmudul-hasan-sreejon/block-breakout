using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    
    Vector2 paddleToBallVector;
    bool hasStarted;

    AudioSource myAudioSource;

    void Start() {
        paddleToBallVector = transform.position - paddle.transform.position;

        hasStarted = false;

        myAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(!hasStarted) {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2 (paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMouseClick() {
        if(Input.GetMouseButtonDown(0)) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);

            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            
            myAudioSource.PlayOneShot(clip);
        }
    }

}
