using CameraShake;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Minions : MonoBehaviour
{


    [SerializeField] private Transform playerPosition;
    [SerializeField] private Rigidbody2D rb;


    [SerializeField] private float timer = 1f;
    [SerializeField] private float timerCounter = 0f;
    [SerializeField] public float moveSpeed = 4f;


    
 
    [SerializeField] public ParticleSystem particles;




    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Players").transform;

        rb = GetComponent<Rigidbody2D>();   
        timerCounter = 0f;
    }

    private void Update()
    {

        if (timerCounter >= timer)
        {
            moveSpeed += 2f;
            timerCounter = 0f;
        }
        timerCounter = timerCounter + Time.deltaTime;

        if (playerPosition != null)
        {
            Vector2 direction = playerPosition.position - transform.position;


            direction.Normalize();


           rb.velocity = direction * moveSpeed;

        }


        if (Input.GetMouseButtonDown(0))
        {

    
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(clickPosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {

                GameManager.Instance.PlayEnmyHit();
                Instantiate(particles, clickPosition,Quaternion.identity);
                CameraShaker.Presets.ShortShake2D();
                CameraShaker.Presets.ShortShake2D();
                CameraShaker.Presets.ShortShake2D();

                GameManager.Instance.score++;
                Destroy(this.gameObject);

            }
        }


        if (GameManager.Instance.isGameover)
        {
            Destroy(this.gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Players"))
        {
            GameManager.Instance.PlaySafeHit();

            TakeMoney();
        }
    }
 
    public void TakeMoney()
    {

        GameManager.Instance.PlayerHit();
        CameraShaker.Presets.ShortShake2D();
        CameraShaker.Presets.ShortShake2D();
        CameraShaker.Presets.ShortShake2D();

        GameManager.Instance.totalHealth = GameManager.Instance.totalHealth - 0.20f;

        Destroy(this.gameObject);
    }


}
