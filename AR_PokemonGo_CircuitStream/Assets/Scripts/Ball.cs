using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public AudioSource hitAudioSource;

    private float ballLifetime = 1.5f;

    public void OnCaptureAnimationFinished()
    {
        DestroyBall();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Monster")) return;

        hitAudioSource.Play();

        animator.SetTrigger("Capturing");
        enabled = false;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.LookAt(other.transform);
    }

    private void DestroyBall()
    {
        FindObjectOfType<GameManager>().BallDestroyed();
        Destroy(gameObject);
    }

    private void Update()
    {
        ballLifetime -= Time.deltaTime;

        if (ballLifetime <= 0)
        {
            DestroyBall();
        }
    }
}