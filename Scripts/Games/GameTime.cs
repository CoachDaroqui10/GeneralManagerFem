using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour {
	//Canvas
	public Text[] result1;
	public Text[] result2;
	public Text marca1;
	public Text marca2;
	public Text timeClock;
	public Text timeOutClock;

	public Text[] nombres1;
	public Text[] nombres2;
	public Text[] tstats1;
	public Text[] tstats2;
	public Text teamStatsT1;
	public Text teamStatsT2;

	public Text nom1;
	public Text nom2;
	public Image logo1;
	public Image logo2;

	public Button tiempoMuerto;

	Manager mg;

	//Desarrollo del juego
	bool corriendo;

	//Score info
	int score1;
	int score2;

	int partial1Score1;
	int partial1Score2;
	int partial2Score1;
	int partial2Score2;
	int partial3Score1;
	int partial3Score2;
	int partial4Score1;
	int partial4Score2;
	int partial5Score1;
	int partial5Score2;

	int possession;
	int timeSeconds;
	float timeOutS;
	const int duracionQ = 600;
	const int duracionTO = 5;
	int quarter;
	int cambio;

	float timer = 0;
	float timeMax = 0.2f;

	float timerTO = 0;

	//Teams info

	int t1;
	int t2;
	Team team1;
	Team team2;
	int minutos1;
	int minutos2;
	const int maxMinutos = 5;
	bool minuto;

	int base1, escolta1, ala1, alapivot1, pivot1;
	int base2, escolta2, ala2, alapivot2, pivot2;

	int tendencia1;
	int tendencia2;
	int[,] tendenciasJ1;
	int[,] tendenciasJ2;
	const int consec = 0;
	const int fire = 1;
	const int energia = 2;

	int statDefensiva;

	//Stats info
	int[,] t1Stats;
	int[,] t2Stats;
	const int pts = 0;
	const int asi = 1;
	const int reb = 2;
	const int rob = 3;
	const int tap = 4;
	const int tov = 5;
	const int p2m = 6;
	const int p2a = 7;
	const int p3m = 8;
	const int p3a = 9;
	const int rbd = 10;
	const int rbo = 11;


	const string pt3 = "3p";
	const string p2e = "p2e";
	const string p2i = "p2i";

	const string defe = "defe";
	const string defi = "defi";

	const string rebd = "rebd";
	const string rebo = "rebo";

	float fg100one = 0;
	float fg100two = 0;
	float p3100one = 0;
	float p3100two = 0;

	//Acciones de jugador


	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();

		corriendo = true;
		tiempoMuerto.onClick.AddListener (timeOut);

		possession = (int) Random.Range (1, 2);
		timeSeconds = duracionQ;
		quarter = 1;
		cambio = 1;
		minuto = false;

		t1 = mg.devolverCalendario()[mg.devolverJornada() - 1, 0];
		t2 = mg.devolverCalendario()[mg.devolverJornada() - 1, 1];
		team1 = GameObject.Find ("Team" + t1).GetComponent<Team> ();
		team2 = GameObject.Find ("Team" + t2).GetComponent<Team> ();
		t1Stats = new int[10,12];
		t2Stats = new int[10,12];

		base1 = 0;
		escolta1 = 2;
		ala1 = 1;
		alapivot1 = 3;
		pivot1 = 4;

		base2 = 0;
		escolta2 = 2;
		ala2 = 1;
		alapivot2 = 3;
		pivot2 = 4;

		tendencia1 = 0;
		tendencia2 = 0;
		tendenciasJ1 = new int[10, 3];
		tendenciasJ2 = new int[10, 3];

		for (int i = 0; i < t1Stats.GetLength(0); i++) {
			for (int j = 0; j < t1Stats.GetLength(1); j++) {
				t1Stats [i,j] = 0;
				t2Stats [i,j] = 0;
			}
		}

		for (int i = 0; i < tendenciasJ1.GetLength(0); i++) {
			for (int j = 0; j < tendenciasJ1.GetLength(1); j++) {
				tendenciasJ1 [i,j] = 0;
				tendenciasJ2 [i,j] = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (corriendo) {
			timer += Time.deltaTime;
			if (timer >= timeMax) {

				if (timeSeconds > 0) {
					makePlay (possession);
				}
				timer = 0;
			}
			actualizarBoton ();
			decisionesIA ();
		}
		calcularPorcentajes ();
		updateUI();
		cronoTO ();
	}

	void makePlay (int pos) {
		int[] tira = new int[2];
		tira = selectShooter (pos);
		int rango, eleccion1, eleccion2;

		if (pos == 1) {
			//Modificar valores segun atributos de equipo
			rango = Random.Range(0,100);
			if (rango < 85) {
				eleccion1 = Random.Range (0,100);
				if (eleccion1 < team1.devolverStat(tira[0], pt3) * 0.33f) {
					shoot3p (tira[0], tira[1], possession);
				} else {
					eleccion2 = Random.Range(0, team1.devolverStat(tira[0], p2e) + team1.devolverStat(tira[0], p2i));
					if (eleccion2 < team1.devolverStat(tira[0], p2e)) {
						shoot2pe (tira[0], tira[1], possession);
					} else {
						shoot2pi (tira[0], tira[1], possession);
					}
				}
				updateFire (tira [0], possession);
			} else {
				turnoverStat (possession);
			}
		} 

		else {
			rango = Random.Range(0,100);
			if (rango < 85) {
				eleccion1 = Random.Range (0,100);
				if (eleccion1 < team2.devolverStat(tira[0], pt3) * 0.4f) {
					shoot3p (tira[0], tira[1], possession);
				} else {
					eleccion2 = Random.Range(0, team2.devolverStat(tira[0], p2e) + team2.devolverStat(tira[0], p2i));
					if (eleccion2 < team2.devolverStat(tira[0], p2e)) {
						shoot2pe (tira[0], tira[1], possession);
					} else {
						shoot2pi (tira[0], tira[1], possession);
					}
				}
				updateFire (tira [0], possession);
			} else {
				turnoverStat (possession);
			}
		}
		changePosession (cambio);
	}

	void shoot2pe (int t, int d, int p) {
		int exito1, exito2;
		if (p == 1) {
			exito1 = Random.Range (0, team1.devolverStat (t, p2e) + team2.devolverStat (d, defe));
			if (exito1 < team1.devolverStat (t, p2e) + 10) {
				exito2 = Random.Range (0, 100);
				if (exito2 < 80 + (tendenciasJ1[t, fire] * 5) - ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
					score2p (p, quarter, t);
					assist (p, t);
					t1Stats [t, p2m]++;
					tendenciasJ1 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ1 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ1 [t, consec] -= 2;
			}
			t1Stats [t, p2a]++;
		} 

		else {
			exito1 = Random.Range (0, team2.devolverStat (t, p2e) + team1.devolverStat (d, defe));
			if (exito1 < team2.devolverStat (t, p2e) + 10) {
				exito2 = Random.Range (0, 100);
				if (exito2 < 80 + (tendenciasJ2[t, fire] * 5) - ((int) (tendenciasJ2[t, energia]) * 0.5f)) {
					score2p (p, quarter, t);
					assist (p, t);
					t2Stats [t, p2m]++;
					tendenciasJ2 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ2 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ2 [t, consec] -= 2;
			}
			t2Stats [t, p2a]++;
		}
	}

	void shoot2pi (int t, int d, int p) {
		int exito1, exito2;
		if (p == 1) {
			exito1 = Random.Range (0, team1.devolverStat (t, p2i) + team2.devolverStat (d, defi));
			if (exito1 < team1.devolverStat (t, p2i) + 15) {
				exito2 = Random.Range (0, 100);
				if (exito2 < 90  + (tendenciasJ1[t, fire] * 5)  - ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
					score2p (p, quarter, t);
					assist (p, t);
					t1Stats [t, p2m]++;
					tendenciasJ1 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ1 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ1 [t, consec] -= 2;
			}
			t1Stats [t, p2a]++;
		} else {
			exito1 = Random.Range (0, team2.devolverStat (t, p2i) + team1.devolverStat (d, defi));
			if (exito1 < team2.devolverStat (t, p2i) + 15) {
				exito2 = Random.Range (0, 100);
				if (exito2 < 90  + (tendenciasJ2[t, fire] * 5)  + ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
					score2p (p, quarter, t);
					assist (p, t);
					t2Stats [t, p2m]++;
					tendenciasJ2 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ2 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ2 [t, consec] -= 2;
			}
			t2Stats [t, p2a]++;
		}
	}

	void shoot3p (int t, int d, int p) {
		int exito1, exito2;
		if (p == 1) {
			exito1 = Random.Range (0, team1.devolverStat (t, pt3) + team2.devolverStat (d, defe));
			if (exito1 < team1.devolverStat (t, pt3)) {
				exito2 = (int) Random.Range (0, (int) 150 - (tendenciasJ1[t, fire] * 10)  + ((int) (tendenciasJ1[t, energia]) * 0.5f));
				if (exito2 < team1.devolverStat (t, pt3)) {
					score3p (p, quarter, t);
					assist (p, t);
					t1Stats [t, p3m]++;
					tendenciasJ1 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ1 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ1 [t, consec] -= 2;
			}
			t1Stats [t, p3a]++;
		} else {
			exito1 = Random.Range (0, team2.devolverStat (t, pt3) + team1.devolverStat (d, defe));
			if (exito1 < team2.devolverStat (t, pt3)) {
				exito2 = (int) Random.Range (0, (int)170  - (tendenciasJ2[t, fire] * 10)  - ((int) (tendenciasJ2[t, energia]) * 0.5f));
				if (exito2 < team2.devolverStat (t, pt3)) {
					score3p (p, quarter, t);
					assist (p, t);
					t2Stats [t, p3m]++;
					tendenciasJ2 [t, consec]++;
				} else {
					defenseStat ();
					tendenciasJ2 [t, consec] -= 2;
				}
			} else {
				defenseStat ();
				tendenciasJ2 [t, consec] -= 2;
			}	
			t2Stats [t, p3a]++;
		}
	}

	int[] selectShooter (int t) {
		int shooter;
		int defender;
		int[] porcentajes = new int[5];
		if (t == 1) {
			if (team1.devolverE1() == 0) {
				for (int i = 0; i < 5; i++) {
					porcentajes [i] = 20;
				}
			} else if (team1.devolverE2() == 0){
				for (int i = 0; i < 5; i++) {
					if (team1.devolverE1() == i+1) {
						porcentajes [i] = 28;
					} else { 
						porcentajes[i] = 18; 
					}
				}
			} else {
				for (int i = 0; i < 5; i++) {
					if (team1.devolverE1() == i+1 || team1.devolverE1() == i+1) {
						porcentajes [i] = 26;
					} else { 
						porcentajes[i] = 16; 
					}
				}
			}
		} 

		else {
			if (team2.devolverE1() == 0) {
				for (int i = 0; i < 5; i++) {
					porcentajes [i] = 20;
				}
			} else if (team2.devolverE2() == 0){
				for (int i = 0; i < 5; i++) {
					if (team2.devolverE1() == i+1) {
						porcentajes [i] = 28;
					} else { 
						porcentajes[i] = 18; 
					}
				}
			} else {
				for (int i = 0; i < 5; i++) {
					if (team2.devolverE1() == i+1 || team2.devolverE1() == i+1) {
						porcentajes [i] = 26;
					} else { 
						porcentajes[i] = 16; 
					}
				}
			}
		}

		int rango = Random.Range (0, 100);
		int p1, p2, p3, p4, p5;
		p1 = porcentajes [0];
		p2 = porcentajes [0] + porcentajes [1];
		p3 = porcentajes [0] + porcentajes [1] + porcentajes [2];
		p4 = porcentajes [0] + porcentajes [1] + porcentajes [2] + porcentajes [3];
		p5 = porcentajes [0] + porcentajes [1] + porcentajes [2] + porcentajes [3] + porcentajes [4];

		if (t == 1) {
			if (rango < p1) {
				shooter = base1;
				defender = base2;
			} else if (rango < p2) {
				shooter = escolta1;
				defender = escolta2;
			} else if (rango < p3) {
				shooter = ala1;
				defender = ala2;
			} else if (rango < p4) {
				shooter = alapivot1;
				defender = alapivot2;
			} else {
				shooter = pivot1;
				defender = pivot2;
			}
		} 

		else {
			if (rango < p1) {
				shooter = base2;
				defender = base1;
			} else if (rango < p2) {
				shooter = escolta2;
				defender = escolta1;
			} else if (rango < p3) {
				shooter = ala2;
				defender = ala1;
			} else if (rango < p4) {
				shooter = alapivot2;
				defender = alapivot1;
			} else {
				shooter = pivot2;
				defender = pivot1;
			}
		}

		int[] dupla = new int[2];
		dupla [0] = shooter;
		dupla [1] = defender;
		return dupla;

	}

	void defenseStat() {
		cambio = 1;
		statDefensiva = Random.Range (0, 100);
		if (statDefensiva < 75) {
			cambio = rebound (possession);
		} else {
			block (possession);
			cambio = rebound (possession);
		}
	}

	void turnoverStat(int p) {
		int	rango = Random.Range (0, 100);
		if (rango < 35) {
			steal (p);
			turnover (p);
		} else {
			turnover (p);
		}
	}

	void changePosession(int c){

		if (c == 1) {
			if (possession == 1) {
				possession = 2;
			} else {
				possession = 1;
			}
		}


		if (timeSeconds > 24) {
			timeSeconds -= (int)Random.Range (4, 24);
		} else {
			timeSeconds -= timeSeconds;

			if (quarter < 4) {
				quarter++;
				timeSeconds = duracionQ;
			} else if (quarter >= 4){
				if (score1 == score2) {
					timeSeconds = duracionQ / 2;
					quarter++;
				} else {
					closeGame ();
				}
			} 
		}

		cansar ();
		cambiar ();
	}

	void cambiar() {
		for (int i = 0; i < tendenciasJ1.GetLength(0); i++) {
			if ((i == base1 || i == escolta1 || i == ala1 || i == alapivot1 || i == pivot1) && tendenciasJ1[i, energia] > Random.Range (15, 25)) {
				switch (i) {
				case (0):
					if (tendenciasJ1[i, energia] > (Random.Range (30, 50) * team1.devolverReparto()) && (quarter != 4 && timeSeconds < 400)) {
						base1 = 5;
					}
					break;
				case (1):
					if (tendenciasJ1 [i, energia] > (Random.Range (30, 50) * team1.devolverReparto ()) && (quarter != 4 && timeSeconds < 400)) {
						ala1 = 6;
					}
					break;
				case (2):
					if (tendenciasJ1 [i, energia] > (Random.Range (30, 50) * team1.devolverReparto ()) && (quarter != 4 && timeSeconds < 400)) {
						escolta1 = 7;
					}
					break;
				case (3):
					if (tendenciasJ1 [i, energia] > (Random.Range (30, 50) * team1.devolverReparto ()) && (quarter != 4 && timeSeconds < 400)) {
						alapivot1 = 8;
					}
					break;
				case (4):
					if (tendenciasJ1 [i, energia] > (Random.Range (30, 50) * team1.devolverReparto ()) && (quarter != 4 && timeSeconds < 400)) {
						pivot1 = 9;
					}
					break;
				case (5):
					base1 = 0;
					break;
				case (6):
					ala1 = 1;
					break;
				case (7):
					escolta1 = 2;
					break;
				case (8):
					alapivot1 = 3;
					break;
				case (9):
					pivot1 = 4;
					break;
				}
			}
		}

		for (int i = 0; i < tendenciasJ2.GetLength(0); i++) {
			if ((i == base2 || i == escolta2 || i == ala2 || i == alapivot2 || i == pivot2) && tendenciasJ2[i, energia] > Random.Range (20, 25)) {
				switch (i) {
				case (0):
					if (tendenciasJ2 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 300)) {
						base2 = 5;
					}
					break;
				case (1):
					if (tendenciasJ2 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 300)) {
						escolta2 = 6;
					}
					break;
				case (2):
					if (tendenciasJ2 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 300)) {
						ala2 = 7; 
					}
					break;
				case (3):
					if (tendenciasJ2 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 300)) {
						alapivot2 = 8; 
					}
					break;
				case (4):
					if (tendenciasJ2 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 300)) {
						pivot2 = 9;
					}
					break;
				case (5):
					base2 = 0;
					break;
				case (6):
					escolta2 = 1;
					break;
				case (7):
					ala2 = 2;
					break;
				case (8):
					alapivot2 = 3;
					break;
				case (9):
					pivot2 = 4;
					break;
				}
			}
		}
	}

	void updateFire (int t, int p) {
		if (p == 1) {
			if (tendenciasJ1 [t, consec] >= 3) {
				tendenciasJ1 [t, consec] = 3;
				tendenciasJ1 [t, fire] = 1;
			}
			if (tendenciasJ1 [t, consec] <= 0) {
				tendenciasJ1 [t, consec] = 0;
				tendenciasJ1 [t, fire] = 0;
			}
		} else {
			if (tendenciasJ2 [t, consec] >= 3) {
				tendenciasJ1 [t, consec] = 3;
				tendenciasJ2 [t, fire] = 1;
			}
			if (tendenciasJ2 [t, consec] <= 0) {
				tendenciasJ2 [t, consec] = 0;
				tendenciasJ2 [t, fire] = 0;
			}
		}
	}

	void cansar(){
		//Cansancio
		for (int i = 0; i < tendenciasJ1.GetLength(0); i++) {
			if (i == base1 || i == escolta1 || i == ala1 || i == alapivot1 || i == pivot1 ) {
				tendenciasJ1 [i, energia] += Random.Range (1, 2);
			} else {
				if ((tendenciasJ1 [i, energia] - 10) > 0) {
					tendenciasJ1 [i, energia] -= 10;
				}
			}
		}

		for (int i = 0; i < tendenciasJ2.GetLength(0); i++) {
			if (i == base2 || i == escolta2 || i == ala2 || i == alapivot2 || i == pivot2 ) {
				tendenciasJ2 [i, energia] += Random.Range (1, 2);
			} else {
				if (tendenciasJ2 [i, energia] - 10 > 0) {
					tendenciasJ2 [i, energia] -= 10;
				}
			}
		}
	}

	void score2p(int pos, int q, int tira){

		if (pos == 1) {
			score1 += 2;
			if (q == 1) {
				partial1Score1 += 2;
			}
			if (q == 2) {
				partial2Score1 += 2;
			}
			if (q == 3) {
				partial3Score1 += 2;
			}
			if (q == 4) {
				partial4Score1 += 2;
			}
			if (q == 5) {
				partial5Score1 += 2;
			}

			//Anotacion Jugadora
			t1Stats[tira,pts] += 2;
		} 
		else {
			score2 += 2;
			if (q == 1) {
				partial1Score2 += 2;
			}
			if (q == 2) {
				partial2Score2 += 2;
			}
			if (q == 3) {
				partial3Score2 += 2;
			}
			if (q == 4) {
				partial4Score2 += 2;
			}
			if (q == 5) {
				partial5Score2 += 2;
			}

			//Anotacion Jugadora
			t2Stats[tira,pts] += 2;
		}

		if (pos == 1) {
			tendencia1 += 5;
			tendencia2 -= 5;
		} else {
			tendencia2 += 5;
			if (tendencia1 > -35) {
				tendencia1 -= 5;
			}
		}
	}

	void score3p(int pos, int q, int tira){

		if (pos == 1) {
			score1 += 3;
			if (q == 1) {
				partial1Score1 += 3;
			}
			if (q == 2) {
				partial2Score1 += 3;
			}
			if (q == 3) {
				partial3Score1 += 3;
			}
			if (q == 4) {
				partial4Score1 += 3;
			}
			if (q == 5) {
				partial5Score1 += 3;
			}

			//Anotacion Jugadora
			t1Stats[tira,pts] += 3;
		} 
		else {
			score2 += 3;
			if (q == 1) {
				partial1Score2 += 3;
			}
			if (q == 2) {
				partial2Score2 += 3;
			}
			if (q == 3) {
				partial3Score2 += 3;
			}
			if (q == 4) {
				partial4Score2 += 3;
			}
			if (q == 5) {
				partial5Score2 += 3;
			}

			//Anotacion Jugadora
			t2Stats[tira,pts] += 3;
		}

		if (pos == 1) {
			tendencia1 += 5;
			tendencia2 -= 5;
		} else {
			tendencia2 += 5;
			if (tendencia1 > -35) {
				tendencia1 -= 5;
			}
		}
	}

	void assist (int p, int t) {
		int exito = Random.Range (0, 100);
		int asistente = 0;

		if (exito < 85) {
			asistente = Random.Range (0, 100);
			if (p == 1) {
				if (asistente < 40) {
					asistente = base1;
				} else if (asistente < 60) {
					asistente = escolta1;
				} else if (asistente < 80) {
					asistente = ala1;
				} else if (asistente < 90) {
					asistente = alapivot1;
				} else {
					asistente = pivot1;
				}
			} else {
				if (asistente < 40) {
					asistente = base2;
				} else if (asistente < 60) {
					asistente = escolta2;
				} else if (asistente < 80) {
					asistente = ala2;
				} else if (asistente < 90) {
					asistente = alapivot2;
				} else {
					asistente = pivot2;
				}
			}
			if (p == 1 && asistente != t) {
				t1Stats [asistente, asi] += 1; 
			} 
			if (p == 2 && asistente != t) {
				t2Stats [asistente, asi] += 1; 
			}
		}
	}

	int rebound (int p) {
		int rango;
		int reboteador = Random.Range (0, 100);

		if (p == 1) {
			rango = Random.Range (0, team1.devolverReb() + (team2.devolverReb() * 3));
			if (rango < team2.devolverReb() * 3)  { //Rebote defensivo

				if (reboteador < 10)
					t2Stats [base2, reb]++;
				else if (reboteador < 20)
					t2Stats [escolta2, reb]++;
				else if (reboteador < 40)
					t2Stats [ala2, reb]++;
				else if (reboteador < 70)
					t2Stats [alapivot2, reb]++;
				else t2Stats [pivot2, reb]++;

				return 1;

			} else { //Rebote ofensivo
				if (reboteador < 10)
					t1Stats [base1, reb]++;
				else if (reboteador < 20)
					t1Stats [escolta1, reb]++;
				else if (reboteador < 40)
					t1Stats [ala1, reb]++;
				else if (reboteador < 70)
					t1Stats [alapivot1, reb]++;
				else t1Stats [pivot1, reb]++;

				return 2;
			}
		} else {
			rango = Random.Range (0, team2.devolverReb() + (team1.devolverReb() * 3));
			if (rango < team1.devolverReb() * 3)  { //Rebote defensivo

				if (reboteador < 10)
					t1Stats [base1, reb]++;
				else if (reboteador < 20)
					t1Stats [escolta1, reb]++;
				else if (reboteador < 40)
					t1Stats [ala1, reb]++;
				else if (reboteador < 70)
					t1Stats [alapivot1, reb]++;
				else t1Stats [pivot1, reb]++;

				return 1;

			} else { //Rebote ofensivo
				if (reboteador < 10)
					t2Stats [base2, reb]++;
				else if (reboteador < 20)
					t2Stats [escolta2, reb]++;
				else if (reboteador < 40)
					t2Stats [ala2, reb]++;
				else if (reboteador < 70)
					t2Stats [alapivot2, reb]++;
				else t2Stats [pivot2, reb]++;

				return 2;
			}
		}
	}

	void steal(int p) {
		int ladron = Random.Range (0, 100);

		if (p == 2) {
			if (ladron < 25)
				t1Stats [base1, rob]++;
			else if (ladron < 45)
				t1Stats [escolta1, rob]++;
			else if (ladron < 65)
				t1Stats [ala1, rob]++;
			else if (ladron < 90)
				t1Stats [alapivot1, rob]++;
			else t1Stats [pivot1, rob]++;
		} else {
			if (ladron < 25)
				t2Stats [base2, rob]++;
			else if (ladron < 45)
				t2Stats [escolta2, rob]++;
			else if (ladron < 65)
				t2Stats [ala2, rob]++;
			else if (ladron < 90)
				t2Stats [alapivot2, rob]++;
			else t2Stats [pivot2, rob]++;
		}
	}

	void turnover(int p) {
		int loser = Random.Range (0, 100);

		if (p == 1) {
			if (loser < 25)
				t1Stats [base1, tov]++;
			else if (loser < 45)
				t1Stats [escolta1, tov]++;
			else if (loser < 65)
				t1Stats [ala1, tov]++;
			else if (loser < 90)
				t1Stats [alapivot1, tov]++;
			else t1Stats [pivot1, tov]++;
		} else {
			if (loser < 25)
				t2Stats [base2, tov]++;
			else if (loser < 45)
				t2Stats [escolta2, tov]++;
			else if (loser < 65)
				t2Stats [ala2, tov]++;
			else if (loser < 90)
				t2Stats [alapivot2, tov]++;
			else t2Stats [pivot2, tov]++;
		}
	}

	void block (int p) {
		int blocker = Random.Range (0, 100);

		if (p == 1) {
			if (blocker < 25)
				t1Stats [base1, tap]++;
			else if (blocker < 45)
				t1Stats [escolta1, tap]++;
			else if (blocker < 65)
				t1Stats [ala1, tap]++;
			else if (blocker < 90)
				t1Stats [alapivot1, tap]++;
			else t1Stats [pivot1, tap]++;
		} else {
			if (blocker < 25)
				t2Stats [base2, tap]++;
			else if (blocker < 45)
				t2Stats [escolta2, tap]++;
			else if (blocker < 65)
				t2Stats [ala2, tap]++;
			else if (blocker < 90)
				t2Stats [alapivot2, tap]++;
			else t2Stats [pivot2, tap]++;
		}
	}

	void timeOut () {
		if (corriendo) {
			if (minutos1 < maxMinutos) {
				tendencia1 = 5;
				tendencia2 = -5;
				minutos1++;
				corriendo = false;

				minuto = true;
			} 
		} else {
			timeOutS = duracionTO;
		}

		if (tiempoMuerto.GetComponentInChildren<Text> ().text == "FINAL") {
			if (mg.devolverJornada () >= 22) {
				SceneManager.LoadScene ("endScene");
			} else {
				SceneManager.LoadScene ("seasonScene");
			}
		}
	}

	void timeOutIA () {
		if (minutos2 < maxMinutos) {
			tendencia1 = 0;
			tendencia2 = 0;
			corriendo = false;

			minuto = true;
		}
	}

	void cronoTO() {
		if (minuto) {
			timeOutS += Time.deltaTime;

			if (timeOutS >= duracionTO) {
				corriendo = true;
				minuto = false;
				timeOutS = 0;

			}
		}
	}

	void decisionesIA() {
		int decision = Random.Range (15, 25);

		if (tendencia2 < - decision) {
			if (minutos2 < maxMinutos) {
				timeOutIA ();
				minutos2++;
			}

		}
	}

	void closeGame() {
		for (int i = 0; i < 10; i++) { //Jugadoras
			for (int j = 0; j < 12; j++) { //Estadisticas
				team1.setStats (i, j, t1Stats [i, j]);
				team2.setStats (i, j, t2Stats [i, j]);
				mg.SaveStats ();
				mg.SaveSeason ();
			}
		}
		if (score1 > score2) {
			team1.setV ();
			team2.setL ();
		} else {
			team2.setV ();
			team1.setL ();
		}
		base1 = 0;
		base2 = 0;
		escolta1 = 1;
		escolta2 = 1;
		ala1 = 2;
		ala2 = 2;
		alapivot1 = 3;
		alapivot2 = 3;
		pivot1 = 4;
		pivot2 = 4;

		mg.balance [12, 0] = mg.seleccion;
		mg.SaveSeason ();
	}

	void calcularPorcentajes() {
		float totalM = 0;
		float totalA = 0;
		for (int i = 0; i < 10; i++) {
			totalM += (t1Stats[i, p2m] + t1Stats[i, p3m]);
			totalA += (t1Stats [i, p2a] + t1Stats [i, p3a]);
		}
		if (totalA != 0) {
			fg100one = (totalM / totalA) * 100;
		}

		float totalM2 = 0;
		float totalA2 = 0;
		for (int i = 0; i < 10; i++) {
			totalM2 += (t2Stats[i, p2m] + t2Stats[i, p3m]);
			totalA2 += (t2Stats [i, p2a] + t2Stats [i, p3a]);
		}
		if (totalA2 != 0) {
			fg100two = (totalM2 / totalA2) * 100;
		}

		float totalM3 = 0;
		float totalA3 = 0;
		for (int i = 0; i < 10; i++) {
			totalM3 += (t1Stats[i, p3m]);
			totalA3 += (t1Stats [i, p3a]);
		}
		if (totalA2 != 0) {
			p3100one = (totalM3 / totalA3) * 100;
		}

		float totalM4 = 0;
		float totalA4 = 0;
		for (int i = 0; i < 10; i++) {
			totalM4 += (t2Stats[i, p3m]);
			totalA4 += (t2Stats [i, p3a]);
		}
		if (totalA2 != 0) {
			p3100two = (totalM4 / totalA4) * 100;
		}
	}

	void updateUI(){
		//Marcador
		int minutes;
		int seconds;
		minutes = (int)(Mathf.Floor ((float)(timeSeconds / 60)));
		seconds = timeSeconds % 60;

		timeClock.text = minutes.ToString ("00") + ":" + seconds.ToString("00");

		marca1.text = score1.ToString ("00");
		marca2.text = score2.ToString ("00");

		result1 [0].text = partial1Score1.ToString ("00");
		result1 [1].text = partial2Score1.ToString ("00");
		result1 [2].text = partial3Score1.ToString ("00");
		result1 [3].text = partial4Score1.ToString ("00");
		result1 [4].text = partial5Score1.ToString ("00");
		result1 [5].text = score1.ToString ("00");

		result2 [0].text = partial1Score2.ToString ("00");
		result2 [1].text = partial2Score2.ToString ("00");
		result2 [2].text = partial3Score2.ToString ("00");
		result2 [3].text = partial4Score2.ToString ("00");
		result2 [4].text = partial5Score2.ToString ("00");
		result2 [5].text = score2.ToString ("00");

		//Equipos
		logo1.sprite = team1.GetComponent<Image>().sprite;
		logo2.sprite = team2.GetComponent<Image>().sprite;
		nom1.text = team1.nombre + "\n\n" + team1.devolverV ().ToString () + " - " + team1.devolverL ().ToString ();
		nom2.text = team2.nombre + "\n\n" + team2.devolverV ().ToString () + " - " + team2.devolverL ().ToString ();

		//Stats
		nombres1[0].text = team1.devolverNombre(base1);
		nombres1[1].text = team1.devolverNombre(escolta1);
		nombres1[2].text = team1.devolverNombre(ala1);
		nombres1[3].text = team1.devolverNombre(alapivot1);
		nombres1[4].text = team1.devolverNombre(pivot1);

		nombres2[0].text = team2.devolverNombre(base2);
		nombres2[1].text = team2.devolverNombre(escolta2);
		nombres2[2].text = team2.devolverNombre(ala2);
		nombres2[3].text = team2.devolverNombre(alapivot2);
		nombres2[4].text = team2.devolverNombre(pivot2);

		tstats1[0].text = t1Stats [base1, pts].ToString ("00") + " : " + t1Stats [base1, reb].ToString ("00") + " : " +
			t1Stats [base1, asi].ToString ("00") + " : " + t1Stats [base1, rob].ToString ("00") + " : " +
			t1Stats [base1, tap].ToString ("00");
		tstats1[1].text = t1Stats [escolta1, pts].ToString ("00") + " : " + t1Stats [escolta1, reb].ToString ("00") + " : " +
			t1Stats [escolta1, asi].ToString ("00") + " : " + t1Stats [escolta1, rob].ToString ("00") + " : " +
			t1Stats [escolta1, tap].ToString ("00");
		tstats1[2].text = t1Stats [ala1, pts].ToString ("00") + " : " + t1Stats [ala1, reb].ToString ("00") + " : " +
			t1Stats [ala1, asi].ToString ("00") + " : " + t1Stats [ala1, rob].ToString ("00") + " : " +
			t1Stats [ala1, tap].ToString ("00");
		tstats1[3].text = t1Stats [alapivot1, pts].ToString ("00") + " : " + t1Stats [alapivot1, reb].ToString ("00") + " : " +
			t1Stats [alapivot1, asi].ToString ("00") + " : " + t1Stats [alapivot1, rob].ToString ("00") + " : " +
			t1Stats [alapivot1, tap].ToString ("00");
		tstats1[4].text = t1Stats [pivot1, pts].ToString ("00") + " : " + t1Stats [pivot1, reb].ToString ("00") + " : " +
			t1Stats [pivot1, asi].ToString ("00") + " : " + t1Stats [pivot1, rob].ToString ("00") + " : " +
			t1Stats [pivot1, tap].ToString ("00");
		
		tstats2[0].text = t2Stats [base2, pts].ToString ("00") + " : " + t2Stats [base2, reb].ToString ("00") + " : " +
			t2Stats [base2, asi].ToString ("00") + " : " + t2Stats [base2, rob].ToString ("00") + " : " +
			t2Stats [base2, tap].ToString ("00");
		tstats2[1].text = t2Stats [escolta2, pts].ToString ("00") + " : " + t2Stats [escolta2, reb].ToString ("00") + " : " +
			t2Stats [escolta2, asi].ToString ("00") + " : " + t2Stats [escolta2, rob].ToString ("00") + " : " +
			t2Stats [escolta2, tap].ToString ("00");
		tstats2[2].text = t2Stats [ala2, pts].ToString ("00") + " : " + t2Stats [ala2, reb].ToString ("00") + " : " +
			t2Stats [ala2, asi].ToString ("00") + " : " + t2Stats [ala2, rob].ToString ("00") + " : " +
			t2Stats [ala2, tap].ToString ("00");
		tstats2[3].text = t2Stats [alapivot2, pts].ToString ("00") + " : " + t2Stats [alapivot2, reb].ToString ("00") + " : " +
			t2Stats [alapivot2, asi].ToString ("00") + " : " + t2Stats [alapivot2, rob].ToString ("00") + " : " +
			t2Stats [alapivot2, tap].ToString ("00");
		tstats2[4].text = t2Stats [pivot2, pts].ToString ("00") + " : " + t2Stats [pivot2, reb].ToString ("00") + " : " +
			t2Stats [pivot2, asi].ToString ("00") + " : " + t2Stats [pivot2, rob].ToString ("00") + " : " +
			t2Stats [pivot2, tap].ToString ("00");

		//Teams Stats
		int totR = 0, totA = 0, totS = 0, totB = 0;
		for (int i = 0; i < 10; i++) {
			totR += t1Stats [i, reb];
			totA += t1Stats [i, asi];
			totS += t1Stats [i, rob];
			totB += t1Stats [i, tap];
		}
		teamStatsT1.text = fg100one.ToString ("F0") + "\n" + p3100one.ToString ("F0") + "\n" +
		totR.ToString () + "\n" + totA.ToString () + "\n" + totS.ToString () + "\n" + totB.ToString ();

		totR = 0; 
		totA = 0;
		totS = 0; 
		totB = 0;
		for (int i = 0; i < 10; i++) {
			totR += t2Stats [i, reb];
			totA += t2Stats [i, asi];
			totS += t2Stats [i, rob];
			totB += t2Stats [i, tap];
		}
		teamStatsT2.text = fg100two.ToString ("F0") + "\n" + p3100two.ToString ("F0") + "\n" +
			totR.ToString () + "\n" + totA.ToString () + "\n" + totS.ToString () + "\n" + totB.ToString ();

		//TimeOut
		float secondsTO;
		secondsTO = (timeOutS) % 60;

		timeOutClock.text = secondsTO.ToString ("00");

		//Botones
		if (minutos1 >= maxMinutos) {
			tiempoMuerto.GetComponentInChildren<Text> ().text = "MINUTOS AGOTADOS";
		}

		if ((quarter == 4 || quarter == 5) && timeSeconds == 0) {
			tiempoMuerto.GetComponentInChildren<Text> ().text = "FINAL";
			corriendo = false;
		}

		if (corriendo) {
			tiempoMuerto.GetComponentInChildren<Text> ().text = "JUGANDO";
		}


	}

	void actualizarBoton() {
		float col = 0.0f + Mathf.Abs((float) tendencia1 / 50);
		if (tendencia1 < 0) {
			tiempoMuerto.image.color = new Vector4 (col, 0, 0, 1);
		} else {
			tiempoMuerto.image.color = new Vector4 (0, col, 0, 1);
		}
	}
}
