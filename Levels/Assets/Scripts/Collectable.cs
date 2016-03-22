using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    public float frequency = 45f;
    public AudioSource collectSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, frequency * Time.deltaTime, 0f), Space.World);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameController.Instance().AddScore(1);
            print("Score: " + GameController.Instance().score);
            //collectSound.Play();
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
            Destroy(gameObject);
        }
    }
}
