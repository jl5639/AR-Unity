using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float velocityFactor = 0.001f;
    public float angleCorrection = 30;

    public Rigidbody ballPrefab;
    public Transform cameraTransform;
    public AudioSource throwAudioSource;

    public CapturedMonsterUI capturedMonsterUIPrefab;
    public RectTransform capturedMonstersHolder;

    private Vector2 startSwipePosition;
    private float startSwipeTime;
    private bool ballThrowingDisabled;

    public void MonsterCaptured(Monster monster)
    {
        MonsterSpawner.MonsterCaptured();

        var newUIEntry = Instantiate(capturedMonsterUIPrefab, capturedMonstersHolder);
        newUIEntry.Setup(monster.portraitSprite);
    }

    public void BallDestroyed()
    {
        ballThrowingDisabled = false;
    }

    private void ThrowBall(float velocity)
    {
        var position = cameraTransform.position + (cameraTransform.forward * .5f);
        var ballRigidbody = Instantiate(ballPrefab, position, cameraTransform.rotation);

        ballRigidbody.angularVelocity = Random.insideUnitSphere * Random.Range(.5f, 2);

        var direction = Vector3.RotateTowards(cameraTransform.forward, Vector3.up, Mathf.Deg2Rad * angleCorrection, 0);
        ballRigidbody.velocity = direction * velocity * velocityFactor;

        ballThrowingDisabled = true;
        throwAudioSource.Play();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !ballThrowingDisabled)
            ThrowBall(7000);
        #endif

        if (Input.touchCount != 1 || ballThrowingDisabled) return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            startSwipePosition = touch.position;
            startSwipeTime = Time.time;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            var magnitude = (touch.position - startSwipePosition).magnitude;
            var deltaTime = Time.time - startSwipeTime;
            var velocity = magnitude / deltaTime;

            if (velocity < 2500) return;

            ThrowBall(Mathf.Min(velocity, 10000));
        }
    }

    private void Awake()
    {
        foreach (Transform child in capturedMonstersHolder.transform) {
            Destroy(child.gameObject);
        }
    }
}
