using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    public float captureChance = .8f;
    public Sprite portraitSprite;
    public Animator animator;
    public AudioSource capturedAudioSource;

    private Transform cameraTransform;

    public void OnCapturedAnimationFinished()
    {
        CaptureMonster();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;

        if (Random.value > captureChance) return;

        animator.SetTrigger("Captured");
        capturedAudioSource.Play();
    }

    private void CaptureMonster()
    {
        FindObjectOfType<GameManager>().MonsterCaptured(this);

        Destroy(gameObject);
    }

    private void Update()
    {
        var position = cameraTransform.position;
        position.y = transform.position.y;
        transform.LookAt(position);
    }

    private void Awake()
    {
        cameraTransform = FindObjectOfType<Camera>().transform;
    }
}
