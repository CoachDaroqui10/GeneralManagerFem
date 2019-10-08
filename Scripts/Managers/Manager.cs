using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public Button nueva;
	public Button cargar;

	public Team[] teams;

	public bool cargada = false;

	//Creacion jugadoras
	public CreatePlayers cp;

	int[,] jugadoras;
	string[] nombres;
	string[] apellidos;

	//Desarrollo de la temporada
	int jornada;
	int[,] calendario;
	public int[,] balance;
	int[,] stats;
	public int[,] mejoras;
	public int acciones;
	public int accMax = 3;
	public int lider;
	public int maxJornadas = 1;
	public int[] provisionalMVP;
	public int campeon;
	public int seleccion;

	//Preferencias
	int[] preferencias;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		nueva.onClick.AddListener (escenaNueva);
		cargar.onClick.AddListener (escena);

		//Creacion de jugadoras
		jugadoras = new int[120, 12];
		nombres = new string[50];
		apellidos = new string[50];
		iniciarNombres (nombres);
		iniciarApellidos (apellidos);

		jugadoras = cp.devolverJugadoras ();

		//Desarrollo temporada
		jornada = 0;
		calendario = new int[22, 12] {{1,6,2,8,3,10,4,7,5,11,9,12}, 
			{1,2,3,12,4,9,5,7,6,10,8,11},
			{1,11,2,7,3,5,4,6,8,9,10,12},
			{1,10,2,11,3,4,5,9,6,12,7,8},
			{1,12,2,9,3,8,4,10,5,6,7,11},
			{1,7,2,3,4,12,5,10,6,8,9,11},
			{1,4,2,6,3,11,5,12,7,9,8,10},
			{1,9,2,10,3,7,4,5,6,11,8,12},
			{1,5,2,12,3,9,4,8,6,7,10,11},
			{1,3,2,4,5,8,6,9,7,10,11,12},
			{1,8,2,5,3,6,4,11,7,12,9,10},
			{1,6,2,8,3,10,4,7,5,11,9,12}, 
			{1,2,3,12,4,9,5,7,6,10,8,11},
			{1,11,2,7,3,5,4,6,8,9,10,12},
			{1,10,2,11,3,4,5,9,6,12,7,8},
			{1,12,2,9,3,8,4,10,5,6,7,11},
			{1,7,2,3,4,12,5,10,6,8,9,11},
			{1,4,2,6,3,11,5,12,7,9,8,10},
			{1,9,2,10,3,7,4,5,6,11,8,12},
			{1,5,2,12,3,9,4,8,6,7,10,11},
			{1,3,2,4,5,8,6,9,7,10,11,12},
			{1,8,2,5,3,6,4,11,7,12,9,10}};
		balance = new int[13, 2];
		stats = new int[120, 12];
		mejoras = new int[5,3];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 3; j++) {
				mejoras [i, j] = 0;
			}
		}
		acciones = 0;
		provisionalMVP = new int[2];
		seleccion = 0;

		//Preferencias
		preferencias = new int[3];
	}

	void Update() {
		
	}

	public void reset(){

		//balance [12, 0] = 1;

		for (int i = 0; i < 12; i++) {
			Team t = GameObject.Find ("Team" + (i + 1)).GetComponent<Team> (); 
			for (int j = 0; j < 10; j++) {
				t.devolverJugadora (j).resetStats ();
			}
		}
		for (int i = 0; i < 12; i++) {
			Team t = GameObject.Find ("Team" + (i + 1)).GetComponent<Team> (); 
			t.cargarV (0);
			t.cargarL (0);
		}

		jornada = 0;

		SaveSeason ();
		SaveStats ();
	}

	void escena() {
		SceneManager.LoadScene ("seasonScene");
	}

	void escenaNueva() {
		SceneManager.LoadScene ("teamScene");
	}
	//Preferencias
	public int[] devolverPrefs() { return preferencias; }
	public void setPrefs(int e1, int e2, int r) { 
		preferencias [0] = e1;
		preferencias [1] = e2;
		preferencias [2] = r;
	} 

	//Desarrollo temporada
	public int devolverJornada() {
		return jornada; 
	}
	public void setJornada(int j) { jornada = j; }

	public int[,] devolverCalendario() {return calendario; }

	public int[,] balanceVL() {
		for (int i = 0; i < teams.Length; i++) {
			balance [i, 0] = teams [i].devolverV ();
			balance [i, 1] = teams [i].devolverL ();
		}
		return balance;
	}

	public int[,] devolverStats() {
		return stats;
	}

	//Creacion Jugadoras
	public int[,] devolverJugadoras(){
		return jugadoras;
	}

	public string devolverNombre(int n) {
		return nombres [n];
	}

	public string devolverApellido(int a) {
		return apellidos [a];
	}

	//GUARDAR Y CARGAR

	public void Save(){
		SaveLoadManager.SavePlayers (this);
	}

	public void SaveSeason () {
		SaveLoadManager.SaveSeason (this);
	}

	public void SaveStats() {
		for (int i = 0; i < 12; i++) {
			Team t = GameObject.Find ("Team" + (i + 1)).GetComponent<Team> (); 
			t.crearMatrizStats ();
			for (int j = 0; j < 10; j++) {
				for (int k = 0; k < 12; k++) {
					stats [j + (10 * t.equipo), k] = t.devolverStats () [j, k];
				}
			}
		}
			
		SaveLoadManager.SaveStats (this);
	}

	public void SaveTrain(){
		SaveLoadManager.SaveTrain (this);
	}

	public void Load(){
		int[,] loadedPlayers = SaveLoadManager.LoadPlayers ();

		jugadoras = loadedPlayers;
		print ("Actualizadas");

		cargada = true;
	}

	public void LoadSeason(){

		int[,] balance = SaveLoadManager.LoadSeason ();
		int i = 0;

		foreach (Team t in teams) {
			t.cargarV (balance [i, 0]);
			t.cargarL (balance [i, 1]);
			i++;
		}
		seleccion = balance [12, 0];
		print (seleccion);
		balance [12, 0] = seleccion;
		print (seleccion);
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

		jornada = teams [0].devolverV () + teams [0].devolverL ();
		print ("Temporada cargada");

		LoadStats ();
	}

	public void LoadStats() {
		stats = SaveLoadManager.LoadStats ();
	}

	public void LoadTrain() {
		mejoras = SaveLoadManager.LoadTrain ();
	}

	void iniciarNombres(string[] nom){
		nom [0] = "Maria";
		nom [1] = "Carmen";
		nom [2] = "Isabel";
		nom [3] = "Ana";
		nom [4] = "Laura";
		nom [5] = "Cristina";
		nom [6] = "Marta";
		nom [7] = "Dolores";
		nom [8] = "Lucia";
		nom [9] = "Pilar";
		nom [10] = "Elena";
		nom [11] = "Sara";
		nom [12] = "Paula";
		nom [13] = "Raquel";
		nom [14] = "Teresa";
		nom [15] = "Beatriz";
		nom [16] = "Nuria";
		nom [17] = "Silvia";
		nom [18] = "Julia";
		nom [19] = "Rosa";
		nom [20] = "Patricia";
		nom [21] = "Irene";
		nom [22] = "Andrea";
		nom [23] = "Rocio";
		nom [24] = "Monica";
		nom [25] = "Alba";
		nom [26] = "Angela";
		nom [27] = "Sonia";
		nom [28] = "Alicia";
		nom [29] = "Sandra";
		nom [30] = "Susana";
		nom [31] = "Marina";
		nom [32] = "Yolanda";
		nom [33] = "Natalia";
		nom [34] = "Eva";
		nom [35] = "Ester";
		nom [36] = "Alejandra";
		nom [37] = "Noelia";
		nom [38] = "Claudia";
		nom [39] = "Veronica";
		nom [40] = "Amparo";
		nom [41] = "Carolina";
		nom [42] = "Nerea";
		nom [43] = "Sofia";
		nom [44] = "Gloria";
		nom [45] = "Celia";
		nom [46] = "Nieves";
		nom [47] = "Ines";
		nom [48] = "Luisa";
		nom [49] = "Pepa";
	}

	void iniciarApellidos(string[] nom){
		nom [0] = "Garcia";
		nom [1] = "Lopez";
		nom [2] = "Perez";
		nom [3] = "Gonzalez";
		nom [4] = "Hernandez";
		nom [5] = "Rodriguez";
		nom [6] = "Moreno";
		nom [7] = "Alonso";
		nom [8] = "Gutierrez";
		nom [9] = "Torres";
		nom [10] = "Romero";
		nom [11] = "Navarro";
		nom [12] = "Ramos";
		nom [13] = "Gil";
		nom [14] = "Molina";
		nom [15] = "Castillo";
		nom [16] = "Herrera";
		nom [17] = "Rubio";
		nom [18] = "Vega";
		nom [19] = "Lozano";
		nom [20] = "Aguilar";
		nom [21] = "Salas";
		nom [22] = "Carmona";
		nom [23] = "Otero";
		nom [24] = "Ruiz";
		nom [25] = "Menendez";
		nom [26] = "Franco";
		nom [27] = "Vila";
		nom [28] = "Maldonado";
		nom [29] = "Rivas";
		nom [30] = "Calderon";
		nom [31] = "Quintana";
		nom [32] = "De Blas";
		nom [33] = "Torrecillas";
		nom [34] = "Pelayo";
		nom [35] = "Riveiro";
		nom [36] = "Iglesias";
		nom [37] = "Goikoetxea";
		nom [38] = "Escriva";
		nom [39] = "Pallardo";
		nom [40] = "Iniesta";
		nom [41] = "Montana";
		nom [42] = "Rebollo";
		nom [43] = "Jover";
		nom [44] = "Iriarte";
		nom [45] = "Dionisio";
		nom [46] = "Pinedo";
		nom [47] = "Pardo";
		nom [48] = "Daroqui";
		nom [49] = "Riba";
	}
}
