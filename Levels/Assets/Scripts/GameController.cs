using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
    private static GameController instance = null;
    public int score = 0;

    void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject); //Destroy any new instances
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public static GameController Instance()
    {
        return instance;
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score >= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Scene Name: " + SceneManager.GetActiveScene().name);
        }
    }
}
