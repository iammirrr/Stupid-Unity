using CameraShake;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicPlayer;
    [SerializeField] private AudioSource EffectPlayer;
    [SerializeField] private AudioClip[] tapSound;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip moneyCut;
    [SerializeField] private AudioClip music;    


    [SerializeField] private GameObject startPannel;
    [SerializeField] private GameObject endPannel;

    [SerializeField] private GameObject player;
    [SerializeField] public ParticleSystem particles;


    public bool gameplayScreen;
    public bool startScreen;
    public bool endScreen;

    int c = 0;

    public float totalHealth = 1f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI endhighScoreText;

    public int score = 0;
    public int totalScore = 0;

    public bool isGameover;



    public static GameManager Instance { get; private set; }




    private void Awake()
    {
        MusicPlayer.GetComponent<AudioSource>().Play();
        MusicPlayer.GetComponent<AudioSource>().volume = 0.5f;
        MusicPlayer.GetComponent<AudioSource>().loop = true;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
       
        startPannel.SetActive(true);
        scoreText.gameObject.SetActive(false);
       
        isGameover = false;
        gameplayScreen  = false;
        startScreen = true;
        endScreen = false;


    }


    public void PlayerHit()
    {
        player.GetComponent<Safe>().SafeHit();

    }

    private void StartGame()
    {
        if (!gameplayScreen && Input.GetMouseButtonDown(0) && startScreen)
        {
            c = 0;
            score = 0;
            totalHealth = 1f;
            
            
            startScreen = false;
            endScreen = false;
            startPannel.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }

    }

    private IEnumerator WaitEndGame()
    {
        endPannel.gameObject.SetActive(true);

        c = 1;
        yield return new WaitForSeconds(2f);
        endScreen = true;
        isGameover = false;


    }

    private void Gameplay()
    {
        if (totalHealth < 0.1f && !endScreen && c == 0)
        {
            isGameover = true;

            totalScore = score + totalScore;

            endScoreText.text = score.ToString() ;
            endhighScoreText.text = totalScore.ToString();
            scoreText.gameObject.SetActive(false);

            StartCoroutine(WaitEndGame());





        }
    }



    public void PlaySoundTap()
    {
        int ran = Random.Range(0, 3);
        EffectPlayer.PlayOneShot(tapSound[ran]);
    
    }
    public void PlayEnmyHit()
    {
        EffectPlayer.PlayOneShot(enemyHit);

    }
    public void PlaySafeHit()
    {
        EffectPlayer.PlayOneShot(moneyCut);

    }

    private void EndGame()
    {
        if (!gameplayScreen && Input.GetMouseButtonDown(0) && endScreen &&!isGameover)
        {
            c = 0;

            totalHealth = 1f;
            score = 0;
            endScreen = false;
            endPannel.gameObject.SetActive(false);
            startPannel.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            PlaySoundTap();
            CameraShaker.Presets.ShortShake2D();

            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(particles, clickPosition, Quaternion.identity);
        }

        if (!startScreen && !endScreen)
        {
            gameplayScreen = true;
        }
        else
        {
            gameplayScreen = false;
        }




        StartGame();

        EndGame();

        Gameplay();

        scoreText.text = totalHealth.ToString() + "$";

    }
 

}