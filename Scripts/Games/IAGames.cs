using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAGames : MonoBehaviour {

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
	const int duracionTO = 3;
	int quarter;
	int cambio;

	float timer = 0;
	float timeMax = 0.1f;

	float timerTO = 0;

	//Teams info
	public int p1;
	public int p2;
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

	float media = 0;
	float media2 = 0;

	//Acciones de jugador


	// Use this for initialization
	void Start () {
		mg = GameObject.Find ("TeamManager").GetComponent<Manager> ();

		corriendo = true;

		possession = (int) Random.Range (1, 2);
		timeSeconds = duracionQ;
		quarter = 1;
		cambio = 1;
		minuto = false;

		t1 = mg.devolverCalendario()[mg.devolverJornada() - 1, p1];
		t2 = mg.devolverCalendario()[mg.devolverJornada() - 1, p2];
		team1 = GameObject.Find ("Team" + t1).GetComponent<Team> ();
		team2 = GameObject.Find ("Team" + t2).GetComponent<Team> ();
		t1Stats = new int[10,12];
		t2Stats = new int[10,12];

		base1 = 0;
		escolta1 = 1;
		ala1 = 2;
		alapivot1 = 3;
		pivot1 = 4;

		base2 = 0;
		escolta2 = 1;
		ala2 = 2;
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
			if (timeSeconds > 0) {
				makePlay (possession);
				//print ("Partido " + p1 + ": " + score1 + ":" + score2 + " ::: " + t1Stats [0, 0]);
			}
			timer = 0;
		}
	}

	void makePlay (int pos) {
		int[] tira = new int[2];
		tira = selectShooter (pos);
		//int tiradora = selectShooter (pos);
		int rango, eleccion1, eleccion2;

		if (pos == 1) {
			//Modificar valores segun atributos de equipo y usuario
			rango = Random.Range(0,100);
			if (rango < 85) {
				eleccion1 = Random.Range (0,100);
				if (eleccion1 < team1.devolverStat(tira[0], pt3) * 0.3f) {
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
		} else {
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
				if (exito2 < 78 + (tendenciasJ1[t, fire] * 5) - ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
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
			exito1 = Random.Range (0, team2.devolverStat (t, p2e) + team1.devolverStat (d, defe));
			if (exito1 < team2.devolverStat (t, p2e) + 10) {
				exito2 = Random.Range (0, 100);
				if (exito2 < 78 + (tendenciasJ2[t, fire] * 5) - ((int) (tendenciasJ2[t, energia]) * 0.5f)) {
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
				if (exito2 < 87  + (tendenciasJ1[t, fire] * 5)  - ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
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
				if (exito2 < 87  + (tendenciasJ2[t, fire] * 5)  + ((int) (tendenciasJ1[t, energia]) * 0.5f)) {
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
				exito2 = (int) Random.Range (0, (int) 175 - (tendenciasJ1[t, fire] * 10)  + ((int) (tendenciasJ1[t, energia]) * 0.5f));
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
				exito2 = (int) Random.Range (0, (int)150  - (tendenciasJ2[t, fire] * 10)  - ((int) (tendenciasJ2[t, energia]) * 0.5f));
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
		} else {
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
		} else {
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
			if ((i == base1 || i == escolta1 || i == ala1 || i == alapivot1 || i == pivot1) && tendenciasJ1[i, energia] > Random.Range (20, 25)) {
				switch (i) {
				case (0):
					if (tendenciasJ1[i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 400)) {
						base1 = 5;
					}
					break;
				case (1):
					if (tendenciasJ1 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 400)) {
						escolta1 = 6;
					}
					break;
				case (2):
					if (tendenciasJ1 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 400)) {
						ala1 = 7;
					}
					break;
				case (3):
					if (tendenciasJ1 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 400)) {
						alapivot1 = 8;
					}
					break;
				case (4):
					if (tendenciasJ1 [i, energia] > Random.Range (45, 75) || (quarter == 4 && timeSeconds < 400)) {
						pivot1 = 9;
					}
					break;
				case (5):
					base1 = 0;
					break;
				case (6):
					escolta1 = 1;
					break;
				case (7):
					ala1 = 2;
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
				t1Stats [base1, tov]++;
			else if (blocker < 45)
				t1Stats [escolta1, tov]++;
			else if (blocker < 65)
				t1Stats [ala1, tov]++;
			else if (blocker < 90)
				t1Stats [alapivot1, tov]++;
			else t1Stats [pivot1, tov]++;
		} else {
			if (blocker < 25)
				t2Stats [base2, tov]++;
			else if (blocker < 45)
				t2Stats [escolta2, tov]++;
			else if (blocker < 65)
				t2Stats [ala2, tov]++;
			else if (blocker < 90)
				t2Stats [alapivot2, tov]++;
			else t2Stats [pivot2, tov]++;
		}
	}
		
	void closeGame() {
		for (int i = 0; i < 10; i++) { //Jugadoras
			for (int j = 0; j < 12; j++) { //Estadisticas
				team1.setStats (i, j, t1Stats [i, j]);
				team2.setStats (i, j, t2Stats [i, j]);
				mg.SaveStats ();
			}
		}
		if (score1 > score2) {
			team1.setV ();
			team2.setL ();
		} else {
			team2.setV ();
			team1.setL ();
		}

		//print ("Partido " + p1 + ": " + score1 + ":" + score2 + " ::: " + t1Stats [0, 0]);
	}

	void calcularPorcentajes() {
		float totalM = 0;
		float totalA = 0;
		for (int i = 0; i < 10; i++) {
			totalM += (t1Stats[i, p2m] + t1Stats[i, p3m]);
			totalA += (t1Stats [i, p2a] + t1Stats [i, p3a]);
		}
		if (totalA != 0) {
			media = totalM / totalA;
		}

		float totalM2 = 0;
		float totalA2 = 0;
		for (int i = 0; i < 10; i++) {
			totalM2 += (t2Stats[i, p2m] + t2Stats[i, p3m]);
			totalA2 += (t2Stats [i, p2a] + t2Stats [i, p3a]);
		}
		if (totalA2 != 0) {
			media2 = totalM2 / totalA2;
		}
	}
}
