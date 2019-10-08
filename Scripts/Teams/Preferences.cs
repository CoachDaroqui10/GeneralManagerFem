using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Preferences : MonoBehaviour {

	public Button[] preferencias;
	public Button[] minutos;
	public Button jugar;
	public Image logo;

	public int reparto;

	Team team;

	// Use this for initialization
	void Start () {
		team = GameObject.Find ("Team1").GetComponent<Team> ();

		reparto = 0;

		jugar.onClick.AddListener (changeLevel);

		transform.GetComponent<Image>().sprite = team.GetComponent<Team> ().campo;

		minutos [0].onClick.AddListener (min1);
		minutos [1].onClick.AddListener (min2);
		minutos [2].onClick.AddListener (min3);

		preferencias [0].onClick.AddListener (click1);
		preferencias [1].onClick.AddListener (click2);
		preferencias [2].onClick.AddListener (click3);
		preferencias [3].onClick.AddListener (click4);
		preferencias [4].onClick.AddListener (click5);

		logo.sprite = team.GetComponent<Image>().sprite;
		jugar.image.sprite = team.btn;

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < 5; i++) {
			if (team.eleccion1 == i+1 || team.eleccion2 == i+1) {
				preferencias [i].image.color = Color.white;
			} else {
				preferencias [i].image.color = Color.black;
			}
		}

		for (int i = 0; i < 3; i++) {
			if (team.reparto == i+1) {
				minutos [i].image.color = Color.white;
			} else {
				minutos [i].image.color = Color.grey;
			}
		}
	}

	void min1() { team.setReparto (1); }
	void min2() { team.setReparto (2); }
	void min3() { team.setReparto (3); }

	void click1() { team.setEleccion (1); }
	void click2() { team.setEleccion (2); }
	void click3() { team.setEleccion (3); }
	void click4() { team.setEleccion (4); }
	void click5() { team.setEleccion (5); }

	void changeLevel(){
		team.mg.setPrefs (team.devolverE1 (), team.devolverE2(), team.devolverReparto ());
		SceneManager.LoadScene ("seasonScene");
	}
}
