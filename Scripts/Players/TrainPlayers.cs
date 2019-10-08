using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainPlayers : MonoBehaviour {

	public Button[] atributos;
	public Button[] posicion;
	public Button aplicar;
	public Button volver;

	Manager mg;

	int contador;

	int atr, pos;
	string atrS, posS;
	string[] poss;
	int[,] mejoras;
	Team team;

	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();
		team = GameObject.Find ("Team1").GetComponent<Team> ();

		transform.GetComponent<Image>().sprite = team.GetComponent<Team> ().entrenes;

		poss = new string[5] {"Bases", "Escoltas", "Aleros", "Ala Pivots", "Pivots"};
		mejoras = new int[5, 3];

		atr = 1;
		pos = 1;

		atributos [0].onClick.AddListener (clickAtr1);
		atributos [1].onClick.AddListener (clickAtr2);
		atributos [2].onClick.AddListener (clickAtr3);

		posicion [0].onClick.AddListener (click1);
		posicion [1].onClick.AddListener (click2);
		posicion [2].onClick.AddListener (click3);
		posicion [3].onClick.AddListener (click4);
		posicion [4].onClick.AddListener (click5);

		aplicar.onClick.AddListener (training);
		volver.onClick.AddListener (volverTemporada);
	}
	
	// Update is called once per frame
	void Update () {
		updateText ();
	}

	void clickAtr1() { atr = 1; }
	void clickAtr2() { atr = 2; }
	void clickAtr3() { atr = 3; }

	void click1() { pos = 1; }
	void click2() { pos = 2; }
	void click3() { pos = 3; }
	void click4() { pos = 4; }
	void click5() { pos = 5; }


	void updateText() {
		float ata = 0, def = 0, reb = 0;
		for (int i = 0; i < 5; i++) {
			float total = 0;
			for (int j = 0; j < 10; j++) {
				total = 0;
				if (team.devolverJugadora(j).devolverPosicion() == i+1) {
					total += (team.devolverJugadora (j).devolver3Pt () + team.devolverJugadora (j).devolver2PtExt () + team.devolverJugadora (j).devolver2PtInt ());
					ata = total / 3;

					total = 0;
					total += (team.devolverJugadora (j).devolverDefExt () + team.devolverJugadora (j).devolverDefInt ());
					def = total / 2;

					total = 0;
					total += (team.devolverJugadora (j).devolverRebOfe () + team.devolverJugadora (j).devolverRebDef ());
					reb = total / 2;
				}
			}
			posicion [i].GetComponentInChildren<Text> ().text = poss[i] + "\n" + ata.ToString ("F0") + " " + def.ToString ("F0") +  " " + reb.ToString ("F0");
		}

		for (int i = 0; i < 5; i++) {
			if (pos == i+1) {
				posicion [i].image.color = Color.red;
			} else {
				posicion [i].image.color = Color.white;
			}
		}

		for (int i = 0; i < 3; i++) {
			if (atr == i+1) {
				atributos [i].image.color = Color.red;
			} else {
				atributos [i].image.color = Color.white;
			}
		}
	}

	void training() {
		if (mg.acciones < mg.accMax) {
			int mej = Random.Range (1, 5);
			mg.mejoras [pos-1, atr-1] += mej;
			mg.SaveTrain ();
			team.entrenar (pos, atr, mej);
			mg.acciones++;
		}
	}

	void volverTemporada() {
		team.calcularMedias ();

		SceneManager.LoadScene ("seasonScene");
	}
}
