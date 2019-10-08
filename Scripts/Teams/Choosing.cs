using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choosing : MonoBehaviour {

	public Button[] equipos;
	public Sprite[] imagenes;
	public Button empezar;
	int seleccion = 0;

	Manager mg;

	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();

		equipos [0].onClick.AddListener (click0);
		equipos [1].onClick.AddListener (click1);
		equipos [2].onClick.AddListener (click2);
		equipos [3].onClick.AddListener (click3);
		equipos [4].onClick.AddListener (click4);
		equipos [5].onClick.AddListener (click5);
		equipos [6].onClick.AddListener (click6);
		equipos [7].onClick.AddListener (click7);
		equipos [8].onClick.AddListener (click8);
		equipos [9].onClick.AddListener (click9);
		equipos [10].onClick.AddListener (click10);
		equipos [11].onClick.AddListener (click11);

		empezar.onClick.AddListener (go);
	}
	
	// Update is called once per frame
	void Update () {
		empezar.image.sprite = imagenes [seleccion];
	}

	void go() {
		switch (seleccion) {
		case (0):

			break;
		case (1):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (2):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (3):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (4):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (5):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (6):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (7):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (8):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (9):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (10):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		case (11):
			GameObject.Find ("Team" + (seleccion+1)).name = "Cambio";
			GameObject.Find ("Team1").name = "Team" + (seleccion+1).ToString();
			GameObject.Find ("Cambio").name = "Team1";
			GameObject.Find ("Team1").GetComponent<Team> ().equipo = 0;
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team> ().equipo = seleccion;
			break;
		}

		mg.balance [12, 0] = seleccion;
		mg.seleccion = seleccion;
		mg.SaveSeason ();

		SceneManager.LoadScene ("seasonScene");
	}

	void click0() { 
		seleccion = 0;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click1() { 
		seleccion = 1;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click2() { 
		seleccion = 2;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click3() { 
		seleccion = 3;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click4() { 
		seleccion = 4;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click5() { 
		seleccion = 5;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click6() { 
		seleccion = 6;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click7() { 
		seleccion = 7;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click8() { 
		seleccion = 8;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click9() { 
		seleccion = 9;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click10() { 
		seleccion = 10;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().nombre + "\n\n\n\n" +
			GameObject.Find ("Team" + (seleccion+1)).GetComponent<Team>().devolverMed();
		empezar.image.sprite = imagenes [seleccion];
	}
	void click11() { 
		seleccion = 11;
		empezar.GetComponentInChildren<Text> ().text = GameObject.Find ("Team" + (seleccion + 1)).GetComponent<Team> ().nombre + "\n\n\n\n" +
		GameObject.Find ("Team" + (seleccion + 1)).GetComponent<Team> ().devolverMed ();
		empezar.image.sprite = imagenes [seleccion];
	}
}
