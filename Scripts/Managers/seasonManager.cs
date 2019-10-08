using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class seasonManager : MonoBehaviour {

	public Button game;
	public Button teamPref;
	public Button train;
	public Button stats;
	public Text texto;
	public Text oponente;
	public Image logo;
	public Image logo1;
	public Image logo2;

	Team team;
	Team rival;
	Team[] teams;

	Manager man;
	statsManager stm;

	int jornada;
	int[,] calendario;
	string opInfo;
	int[,] mvpG;
	int[,] Vteam;


	// Use this for initialization
	void Start () {

		game.onClick.AddListener (startGame);
		teamPref.onClick.AddListener (teamPreferences);
		train.onClick.AddListener (trainment);
		stats.onClick.AddListener (statScene);

		man = GameObject.Find ("TeamManager").GetComponent<Manager> ();

		teams = new Team[12];
		for (int i = 0; i < 12; i++) {
			teams[i] = GameObject.Find("Team" + (i+1)).GetComponent<Team> ();
		}

		mvpG = new int[3,2];
		Vteam = new int[12, 2];

		team = GameObject.Find ("Team1").GetComponent<Team> ();
		rival = GameObject.Find("Team" + man.devolverCalendario()[jornada, 1]).GetComponent<Team>();
		logo.sprite = team.GetComponent<Image>().sprite;

		jornada = man.devolverJornada ();
		calendario = man.devolverCalendario ();

		if (man.cargada) {
			for (int i = 0; i < 10; i++) {
				for (int j = 0; j < 12; j++) {
					team.setStats (i, j, man.devolverStats() [i, j]);
				}
			}
			for (int i = 1; i < 12; i++) {
				Team t = GameObject.Find ("Team" + (i + 1)).GetComponent<Team> (); 
				for (int j = 0; j < 10; j++) {
					for (int k = 0; k < 12; k++) {
						t.setStats (j, k, man.devolverStats () [j + (10 * t.equipo), k]);
					}
				}
			}
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 3; j++) {
					for (int k= 0; k < man.mejoras [i,j]; k++) {
						GameObject.Find ("Team1").GetComponent<Team> ().entrenar (i+1, j+1, 1);
						GameObject.Find ("Team1").GetComponent<Team> ().entrenar (i+1 + 5, j+1, 1);
					}
				}
			}
			man.cargada = false;
		}

		logo1.sprite = team.GetComponent<Image>().sprite;

		mvp ();
		chart ();
	}
	
	// Update is called once per frame
	void Update () {
		rival = GameObject.Find("Team" + man.devolverCalendario()[jornada, 1]).GetComponent<Team>();
		texto.text = team.devolverV() + " : " + team.devolverL();

		rival.calcularMedias ();
		int at = rival.devolverAta ();
		if (at > 99) {
			at = 99;
		}
		int df = rival.devolverDef ();
		if (df > 99) {
			df = 99;
		}
		int rb = rival.devolverReb ();
		if (rb > 99) {
			rb = 99;
		}

		opInfo = "PROXIMO RIVAL" + "\n" + rival.GetComponent<Team> ().nombre + "\n" +
			rival.GetComponent<Team> ().devolverV ().ToString() + " - " + rival.GetComponent<Team> ().devolverL ().ToString() + "\n" +
			"A: " + at.ToString() + " D: " + rb.ToString() + " R: " + rb.ToString();
		oponente.text = opInfo;
		logo2.sprite = rival.GetComponent<Image>().sprite;
	}

	float ppg() {
		float ppg;
		float total = 0;
		for (int i = 0; i < 10; i++) {
			total += team.devolverJugadora (i).devolverStats () [0];
		}
		if (team.devolverL () + team.devolverV () != 0) {
			ppg = total / (team.devolverL () + team.devolverV ());
			return ppg;
		}
		return 0;
	}

	float ppgRival() {
		float ppg;
		float total = 0;
		for (int i = 0; i < 10; i++) {
			total += rival.devolverJugadora (i).devolverStats () [0];
		}
		if (rival.devolverL () + rival.devolverV () != 0) {
			ppg = total / (rival.devolverL () + rival.devolverV ());
			return ppg;
		}
		return 0;
	}

	public void chart() {
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
		man.campeon = Vteam [0, 0];
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
					max3 = max2;
					max2 = max;

					mvpG [2, 0] = mvpG [1, 0];
					mvpG [2, 1] = mvpG [1, 1];
					mvpG [1, 0] = mvpG [0, 0];
					mvpG [1, 1] = mvpG [0, 1];
					mvpG [0, 0] = i;
					mvpG [0, 1] = j;
					max = total;
				} else if (total > max2) {
					max3 = max2;

					mvpG [2, 0] = mvpG [1, 0];
					mvpG [2, 1] = mvpG [1, 1];
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
		man.provisionalMVP [0] = mvpG [0, 0];
		man.provisionalMVP [1] = mvpG [0, 1];
	}

	void startGame() {
		man.setJornada (jornada + 1);
		man.SaveSeason ();
		man.acciones = 0;

		SceneManager.LoadScene ("gameScene");
	}

	void teamPreferences() {
		SceneManager.LoadScene ("teamPrefScene");
	}

	void trainment() {
		SceneManager.LoadScene ("trainScene");
	}

	void statScene() {
		SceneManager.LoadScene ("statsScene");
	}
}
