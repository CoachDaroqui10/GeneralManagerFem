using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startGameLoad : MonoBehaviour {

	public Button btn;

	// Use this for initialization
	void Start () {
		btn.onClick.AddListener (changeLevel);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void changeLevel(){
		SceneManager.LoadScene ("gameScene");
	}
}
