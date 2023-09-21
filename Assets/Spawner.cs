using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] minions;

    [SerializeField] private float timer = 3f;
    [SerializeField] private float timerCounter = 0f;
    [SerializeField] private float spawingFreqeuncy = 0.2f;
    private void Awake()
    {
        timerCounter = 0f;

    }

    private void Update()
    {
        if (GameManager.Instance.gameplayScreen && !GameManager.Instance.isGameover)
        {
            timerCounter += Time.deltaTime;

            if (timerCounter >= timer)
            {
                int random = Random.Range(0, 4);

                if (random == 0)
                {
                    float randx = Random.Range(-18f, -15f);
                    float randy = Random.Range(6f, -6f);
                    int randomMinion = Random.Range(0, 6);
                    GameObject temp = Instantiate(minions[randomMinion], points[0].transform.position + new Vector3(randx, randy, 0f), Quaternion.identity);
                    temp.transform.position += transform.forward * 2f * Time.deltaTime;

                }
                else if (random == 1)
                {
                    float randx = Random.Range(18f, 14f);
                    float randy = Random.Range(6f, -6f);
                    int randomMinion = Random.Range(0, 6);
                    GameObject temp = Instantiate(minions[randomMinion], points[1].transform.position + new Vector3(randx, randy, 0f), Quaternion.identity);
                    temp.transform.position += transform.forward * 2f * Time.deltaTime;
                }
                else if (random == 2)
                {
                    float randx = Random.Range(13f, -13f);
                    float randy = Random.Range(10f, 8f);
                    int randomMinion = Random.Range(0, 6);
                    GameObject temp = Instantiate(minions[randomMinion], points[2].transform.position + new Vector3(randx, randy, 0f), Quaternion.identity);
                    temp.transform.position += transform.forward * 2f * Time.deltaTime;
                }
                else if (random == 3)
                {
                    float randx = Random.Range(13f, -13f);
                    float randy = Random.Range(-10f, -8f);
                    int randomMinion = Random.Range(0, 6);
                    GameObject temp = Instantiate(minions[randomMinion], points[3].transform.position + new Vector3(randx, randy, 0f), Quaternion.identity);
                    temp.transform.position += transform.forward * 2f * Time.deltaTime;
                }

                if (timer > spawingFreqeuncy) // Set your desired minimum timer here (0.2 is just an example)
                {
                    timer -= 0.05f;
                }

                timerCounter = 0f; // Move this line here
            }

    
        }
    }
}
