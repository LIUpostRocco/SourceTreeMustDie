using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {
	private string beginScene;
	public GameObject fade;
	public EventSystem es;

	public void DoBegin() {
		SceneManager.LoadScene(beginScene);
	}

	public void Begin(string which) {
		beginScene = which;
		fade.SetActive(true);
		es.enabled = false;
		Invoke("DoBegin", 1f);
	}

	public void Quit() {
		Invoke("DoQuit", 1f);
	}

	private void DoQuit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}

// ~ Rocco Russo