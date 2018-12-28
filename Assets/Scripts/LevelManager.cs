using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public void LoadLevel (string name) {
		Debug.Log("Scene load requested for: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest () {
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
}
