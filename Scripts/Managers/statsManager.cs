using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class statsManager : MonoBehaviour {

	Manager mg;
	Team[] teams;

	public Text clasificacionN;
	public Text clasificacionR;

	public Text ppgN;
	public Text ppgS;

	public Text rpgN;
	public Text rpgS;

	public Text apgN;
	public Text apgS;

	public Text mvpT;
	public Image[] logos;

	public Button volver;

	int[,] Vteam ;
	int[,] pointsPG;
	int[,] rebPG;
	int[,] astPG;
	int[,] mvpG;

	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();
		teams = new Team[12];
		for (int i = 0; i < 12; i++) {
			teams[i] = GameObject.Find("Team" + (i+1)).GetComponent<Team> ();
		}
		volver.onClick.AddListener (back);

		Vteam = new int[12, 2];
		pointsPG = new int[5, 2];
		rebPG = new int[5, 2];
		astPG = new int[5, 2];
		mvpG = new int[3,2];
		chart ();
		ppg ();
		rpg ();
		apg ();
		mvp ();
	}
	
	// Update is called once per frame
	void Update () {
		updateUI ();
	}

	void updateUI() {
		//Clasificacion
		string clas = "";
		string rec = "";
		for (int i = 0; i < 12; i++) {
			Team t = GameObject.Find ("Team" + (Vteam [i, 1] + 1)).GetComponent<Team> ();
			clas += (i + 1) + "º " + t.nombre + "\n"; 
			rec += t.devolverV () + " : " + t.devolverL () + "\n";
		}
		Team u = GameObject.Find ("Team" + (Vteam [11, 1] + 1)).GetComponent<Team> ();
		clas += "12º " + u.nombre;
		rec += u.devolverV () + " - " + u.devolverL ();

		clasificacionN.text = clas;
		clasificacionR.text = rec;

		//Puntos por partido
		string ppgNom = "";
		string ppgStt = "";
		for (int i = 0; i < 5; i++) {
			ppgNom += GameObject.Find ("Team" + (pointsPG [i, 0] + 1)).GetComponent<Team> ().devolverNombre (pointsPG [i, 1]) + "\n";
			float stat = ((float) GameObject.Find ("Team" + (pointsPG [i, 0] + 1)).GetComponent<Team> ().devolverJugadora (pointsPG [i, 1]).devolverStats () [0] /
			       (GameObject.Find ("Team" + (pointsPG [i, 0] + 1)).GetComponent<Team> ().devolverV () + 
					GameObject.Find ("Team" + (pointsPG [i, 0] + 1)).GetComponent<Team> ().devolverL ()));
			ppgStt += stat.ToString("F1") + "\n";
		}
		ppgN.text = ppgNom;
		ppgS.text = ppgStt;

		//Rebotes por partido
		string rpgNom = "";
		string rpgStt = "";
		for (int i = 0; i < 5; i++) {
			rpgNom += GameObject.Find ("Team" + (rebPG [i, 0] + 1)).GetComponent<Team> ().devolverNombre (rebPG [i, 1]) + "\n";
			float stat = ((float) GameObject.Find ("Team" + (rebPG [i, 0] + 1)).GetComponent<Team> ().devolverJugadora (rebPG [i, 1]).devolverStats () [2] /
				(GameObject.Find ("Team" + (rebPG [i, 0] + 1)).GetComponent<Team> ().devolverV () + 
				 GameObject.Find ("Team" + (rebPG [i, 0] + 1)).GetComponent<Team> ().devolverL ()));
			rpgStt += stat.ToString("F1") + "\n";
		}
		rpgN.text = rpgNom;
		rpgS.text = rpgStt;

		//Asistencias por partido
		string apgNom = "";
		string apgStt = "";
		for (int i = 0; i < 5; i++) {
			apgNom += GameObject.Find ("Team" + (astPG [i, 0] + 1)).GetComponent<Team> ().devolverNombre (astPG [i, 1]) + "\n";
			float stat = ((float) GameObject.Find ("Team" + (astPG [i, 0] + 1)).GetComponent<Team> ().devolverJugadora (astPG [i, 1]).devolverStats () [1] /
				(GameObject.Find ("Team" + (astPG [i, 0] + 1)).GetComponent<Team> ().devolverV () + 
				 GameObject.Find ("Team" + (astPG [i, 0] + 1)).GetComponent<Team> ().devolverL ()));
			apgStt += stat.ToString("F1") + "\n";
		}
		apgN.text = apgNom;
		apgS.text = apgStt;

		//MVP
		string mvpN = "";
		for (int i = 0; i < 3; i++) {
			int total = ((teams [i].devolverJugadora (mvpG [i, 1]).devolverStats () [0] * 6) +
				(teams [i].devolverJugadora (mvpG [i, 1]).devolverStats () [2] * 2) +
				(teams [i].devolverJugadora (mvpG [i, 1]).devolverStats () [1] * 3) +
				teams [i].devolverJugadora (mvpG [i, 1]).devolverStats () [3] +
				teams [i].devolverJugadora (mvpG [i, 1]).devolverStats () [4]) / mg.devolverJornada();
			mvpN += GameObject.Find ("Team" + (mvpG [i, 0] + 1)).GetComponent<Team> ().devolverNombre (mvpG [i, 1]) + "\n"; // + " " + total + "\n";
			logos [i].sprite = GameObject.Find ("Team" + (mvpG [i, 0] + 1)).GetComponent<Image> ().sprite;
		}
		mvpT.text = mvpN;

	}

	void chart() {
		for (int i = 0; i < 12; i++) {
			Vteam [i,0] = teams [i].devolverV ();
			Vteam [i,1] = teams [i].equipo;
		}

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 12; j++) {
				if (Vteam[i,0] > Vteam [j,0]) {
					int aux = Vteam [j,0];
					int aux2 = Vteam [j, 1];
					Vteam [j, 0] = Vteam [i, 0];
					Vteam [j, 1] = Vteam [i, 1];
					Vteam [i, 0] = aux;
					Vteam [i, 1] = aux2;
				}
			}
		}
	}

	void ppg(){
		int max1 = 0, max2 = 0, max3 = 0, max4 = 0, max5 = 0;

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 10; j++) {
				if (teams[i].devolverJugadora(j).devolverStats()[0] > max1) {
					max5 = max4;
					max4 = max3;
					max3 = max2;
					max2 = max1;

					pointsPG [4, 0] = pointsPG [3, 0];
					pointsPG [4, 1] = pointsPG [3, 1];
					pointsPG [3, 0] = pointsPG [2, 0];
					pointsPG [3, 1] = pointsPG [2, 1];
					pointsPG [2, 0] = pointsPG [1, 0];
					pointsPG [2, 1] = pointsPG [1, 1];
					pointsPG [1, 0] = pointsPG [0, 0];
					pointsPG [1, 1] = pointsPG [0, 1];
					pointsPG [0, 0] = i;
					pointsPG [0, 1] = j;

					max1 = teams [i].devolverJugadora (j).devolverStats () [0];
				} else if (teams[i].devolverJugadora(j).devolverStats()[0] > max2) {
					max5 = max4;
					max4 = max3;
					max3 = max2;

					pointsPG [4, 0] = pointsPG [3, 0];
					pointsPG [4, 1] = pointsPG [3, 1];
					pointsPG [3, 0] = pointsPG [2, 0];
					pointsPG [3, 1] = pointsPG [2, 1];
					pointsPG [2, 0] = pointsPG [1, 0];
					pointsPG [2, 1] = pointsPG [1, 1];
					pointsPG [1, 0] = i;
					pointsPG [1, 1] = j;

					max2 = teams [i].devolverJugadora (j).devolverStats () [0];
				} else if (teams[i].devolverJugadora(j).devolverStats()[0] > max3) {
					max5 = max4;
					max4 = max3;

					pointsPG [4, 0] = pointsPG [3, 0];
					pointsPG [4, 1] = pointsPG [3, 1];
					pointsPG [3, 0] = pointsPG [2, 0];
					pointsPG [3, 1] = pointsPG [2, 1];
					pointsPG [2, 0] = i;
					pointsPG [2, 1] = j;

					max3 = teams [i].devolverJugadora (j).devolverStats () [0];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[0] > max4) {
					max5 = max4;
					
					pointsPG [4, 0] = pointsPG [3, 0];
					pointsPG [4, 1] = pointsPG [3, 1];
					pointsPG [3, 0] = i;
					pointsPG [3, 1] = j;

					max4 = teams [i].devolverJugadora (j).devolverStats () [0];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[0] > max5) {
					pointsPG [4, 0] = i;
					pointsPG [4, 1] = j;
					max5 = teams [i].devolverJugadora (j).devolverStats () [0];
				}	
			}
		}
	}

	void rpg(){
		int max1 = 0, max2 = 0, max3 = 0, max4 = 0, max5 = 0;

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 10; j++) {
				if (teams[i].devolverJugadora(j).devolverStats()[2] > max1) {
					max5 = max4;
					max4 = max3;
					max3 = max2;
					max2 = max1;

					rebPG [4, 0] = rebPG [3, 0];
					rebPG [4, 1] = rebPG [3, 1];
					rebPG [3, 0] = rebPG [2, 0];
					rebPG [3, 1] = rebPG [2, 1];
					rebPG [2, 0] = rebPG [1, 0];
					rebPG [2, 1] = rebPG [1, 1];
					rebPG [1, 0] = rebPG [0, 0];
					rebPG [1, 1] = rebPG [0, 1];
					rebPG [0, 0] = i;
					rebPG [0, 1] = j;

					max1 = teams [i].devolverJugadora (j).devolverStats () [2];
				} else if (teams[i].devolverJugadora(j).devolverStats()[2] > max2) {
					max5 = max4;
					max4 = max3;
					max3 = max2;

					rebPG [4, 0] = rebPG [3, 0];
					rebPG [4, 1] = rebPG [3, 1];
					rebPG [3, 0] = rebPG [2, 0];
					rebPG [3, 1] = rebPG [2, 1];
					rebPG [2, 0] = rebPG [1, 0];
					rebPG [2, 1] = rebPG [1, 1];
					rebPG [1, 0] = i;
					rebPG [1, 1] = j;

					max2 = teams [i].devolverJugadora (j).devolverStats () [2];
				} else if (teams[i].devolverJugadora(j).devolverStats()[2] > max3) {
					max5 = max4;
					max4 = max3;

					rebPG [4, 0] = rebPG [3, 0];
					rebPG [4, 1] = rebPG [3, 1];
					rebPG [3, 0] = rebPG [2, 0];
					rebPG [3, 1] = rebPG [2, 1];
					rebPG [2, 0] = i;
					rebPG [2, 1] = j;

					max3 = teams [i].devolverJugadora (j).devolverStats () [2];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[2] > max4) {
					max5 = max4;

					rebPG [4, 0] = rebPG [3, 0];
					rebPG [4, 1] = rebPG [3, 1];
					rebPG [3, 0] = i;
					rebPG [3, 1] = j;

					max4 = teams [i].devolverJugadora (j).devolverStats () [2];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[2] > max5) {
					rebPG [4, 0] = i;
					rebPG [4, 1] = j;
					max5 = teams [i].devolverJugadora (j).devolverStats () [2];
				}	
			}
		}
	}

	void apg(){
		int max1 = 0, max2 = 0, max3 = 0, max4 = 0, max5 = 0;

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 10; j++) {
				if (teams[i].devolverJugadora(j).devolverStats()[1] > max1) {
					max5 = max4;
					max4 = max3;
					max3 = max2;
					max2 = max1;

					astPG [4, 0] = astPG [3, 0];
					astPG [4, 1] = astPG [3, 1];
					astPG [3, 0] = astPG [2, 0];
					astPG [3, 1] = astPG [2, 1];
					astPG [2, 0] = astPG [1, 0];
					astPG [2, 1] = astPG [1, 1];
					astPG [1, 0] = astPG [0, 0];
					astPG [1, 1] = astPG [0, 1];
					astPG [0, 0] = i;
					astPG [0, 1] = j;

					max1 = teams [i].devolverJugadora (j).devolverStats () [1];
				} else if (teams[i].devolverJugadora(j).devolverStats()[1] > max2) {
					max5 = max4;
					max4 = max3;
					max3 = max2;

					astPG [4, 0] = astPG [3, 0];
					astPG [4, 1] = astPG [3, 1];
					astPG [3, 0] = astPG [2, 0];
					astPG [3, 1] = astPG [2, 1];
					astPG [2, 0] = astPG [1, 0];
					astPG [2, 1] = astPG [1, 1];
					astPG [1, 0] = i;
					astPG [1, 1] = j;

					max2 = teams [i].devolverJugadora (j).devolverStats () [1];
				} else if (teams[i].devolverJugadora(j).devolverStats()[1] > max3) {
					max5 = max4;
					max4 = max3;

					astPG [4, 0] = astPG [3, 0];
					astPG [4, 1] = astPG [3, 1];
					astPG [3, 0] = astPG [2, 0];
					astPG [3, 1] = astPG [2, 1];
					astPG [2, 0] = i;
					astPG [2, 1] = j;

					max3 = teams [i].devolverJugadora (j).devolverStats () [1];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[1] > max4) {
					max5 = max4;

					astPG [4, 0] = astPG [3, 0];
					astPG [4, 1] = astPG [3, 1];
					astPG [3, 0] = i;
					astPG [3, 1] = j;

					max4 = teams [i].devolverJugadora (j).devolverStats () [1];
				}	
				else if (teams[i].devolverJugadora(j).devolverStats()[1] > max5) {
					astPG [4, 0] = i;
					rebPG [4, 1] = j;
					max5 = teams [i].devolverJugadora (j).devolverStats () [1];
				}	
			}
		}
	}

	void mvp(){
		int max = 0, max2 = 0, max3 = 0;

		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 10; j++) {
				int total = ((teams [i].devolverJugadora (j).devolverStats () [0] * 6) +
				            (teams [i].devolverJugadora (j).devolverStats () [2] * 2) +
				            (teams [i].devolverJugadora (j).devolverStats () [1] * 3) +
				            teams [i].devolverJugadora (j).devolverStats () [3] +
				            teams [i].devolverJugadora (j).devolverStats () [4]);
				if (total > max) {
					mvpG [0, 0] = i;
					mvpG [0, 1] = j;
					max = total;
				} else if (total > max2) {
					mvpG [1, 0] = i;
					mvpG [1, 1] = j;
					max2 = total;
				}	else if (total > max3) {
					mvpG [2, 0] = i;
					mvpG [2, 1] = j;
					max3 = total;
				}	
			}
		}
		mg.provisionalMVP [0] = mvpG [0, 0];
		mg.provisionalMVP [1] = mvpG [0, 1];
	}

	void back() {
		SceneManager.LoadScene ("seasonScene");
	}
}
