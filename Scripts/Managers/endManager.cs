using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endManager : MonoBehaviour {

	public Text campeon;
	public Image logoC;
	public Text mostvp;
	public Image logoMVP;
	public Button salir;

	Team[] teams;
	Manager mg;

	int[,] Vteam ;
	int[,] mvpG;

	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();
		teams = new Team[12];
		for (int i = 0; i < 12; i++) {
			teams[i] = GameObject.Find("Team" + (i+1)).GetComponent<Team> ();
		}

		salir.onClick.AddListener (newSeason);

		Vteam = new int[12, 2];
		mvpG = new int[5, 2];

		chart ();
		mvp ();
	}
	
	// Update is called once per frame
	void Update () {

		campeon.text = teams [mg.campeon].nombre;
		logoC.sprite = teams [mg.campeon].GetComponent<Image> ().sprite;

		mostvp.text = GameObject.Find ("Team" + (mvpG [0, 0] + 1)).GetComponent<Team> ().devolverNombre (mvpG [0, 1]) + "\n" +
			((float) (GameObject.Find ("Team" + (mvpG [0, 0] + 1)).GetComponent<Team> ().devolverJugadora (mvpG [0, 1]).devolverStats () [0]) /
		(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n" +
			((float) (GameObject.Find ("Team" + (mvpG [0, 0] + 1)).GetComponent<Team> ().devolverJugadora (mvpG [0, 1]).devolverStats () [2]) /
				(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n" +
			((float) (GameObject.Find ("Team" + (mvpG [0, 0] + 1)).GetComponent<Team> ().devolverJugadora (mvpG [0, 1]).devolverStats () [1]) /
		(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n";
		logoMVP.sprite = GameObject.Find ("Team" + (mvpG [0, 0] + 1)).GetComponent<Team> ().GetComponent<Image> ().sprite;

		/*mostvp.text = GameObject.Find ("Team" + (mg.provisionalMVP [0] + 1)).GetComponent<Team> ().devolverNombre (mg.provisionalMVP [1]) + "\n" +
			(GameObject.Find ("Team" + (mg.provisionalMVP [0] + 1)).GetComponent<Team> ().devolverJugadora (mg.provisionalMVP [1]).devolverStats () [0] /
				(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n" +
			(GameObject.Find ("Team" + (mg.provisionalMVP [0] + 1)).GetComponent<Team> ().devolverJugadora (mg.provisionalMVP [1]).devolverStats () [2] /
				(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n" +
			(GameObject.Find ("Team" + (mg.provisionalMVP [0] + 1)).GetComponent<Team> ().devolverJugadora (mg.provisionalMVP [1]).devolverStats () [1] /
				(teams [Vteam [0, 1]].devolverV () + teams [Vteam [0, 1]].devolverL ())).ToString ("F1") + "\n";
		logoMVP.sprite = GameObject.Find ("Team" + (mg.provisionalMVP [0] + 1)).GetComponent<Team> ().GetComponent<Image> ().sprite;*/
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

		mg.campeon = Vteam [0, 1];
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

	void newSeason() {
		mg.reset ();

		SceneManager.LoadScene ("seasonScene");
	}
}
