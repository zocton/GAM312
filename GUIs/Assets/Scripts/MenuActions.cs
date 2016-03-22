using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene(1);
        foreach(Transform child in GameController.Instance().transform)
        {
            if(child.name == "Canvas")
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
