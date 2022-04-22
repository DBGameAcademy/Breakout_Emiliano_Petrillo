using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Vector2 Velocity = new Vector2(4, 4);

    CircleCollider2D circleCollider;
    GameObject lastObjectHit;

    GameController gameController;

    public AudioClip OnWallHitAudio;
    public AudioClip OnPaddleHitAudio;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        BallMove();
    }

    public void BallMove()
    {
        transform.Translate(Velocity * Time.deltaTime);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, Velocity, (Velocity * Time.deltaTime).magnitude);

        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider != circleCollider && hit.transform.gameObject != lastObjectHit)
            {
                lastObjectHit = hit.transform.gameObject;

                Velocity = Vector2.Reflect(Velocity, hit.normal);

                if (hit.transform.GetComponent<Paddle>())
                {
                    Velocity.y = Mathf.Abs(Velocity.y);
                    gameController.AudioController.PlayClip(OnPaddleHitAudio);
                }

                if (hit.transform.GetComponent<Block>())
                {
                    hit.transform.GetComponent<Block>().OnHit();
                }

                gameController.AudioController.PlayClip(OnWallHitAudio);
            }
        }

        if(transform.position.y < -Camera.main.orthographicSize)
        {
            gameController.BallLost();
            lastObjectHit = null;
        }
    }

}
