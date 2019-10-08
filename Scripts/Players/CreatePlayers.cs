using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayers : MonoBehaviour {

	string[] nombres;
	string[] apellidos;

	int contador;
	bool reparto;
	int[,] draftMatrix; 

	public int[,] Jugadoras;

	// Use this for initialization
	void Awake () {
		nombres = new string[50];
		apellidos = new string[50];

		contador = 0;
		reparto = false;

		Jugadoras = new int[120,12];

		draftMatrix = new int[12, 5];

		generarJugadoras ();
		draft ();
	}

	void Update(){
		//print (Jugadoras [18, 4]);
	}

	public int[,] devolverJugadoras() {
		return Jugadoras;
	}

	void draft(){
		bool sigue = true;

		for (int i = 0; i < 10; i++) { //10 rondas
			for (int j = 0; j < 12; j++) { //12 equipos
				sigue = true;
				for (int k = 0; k < 120 && sigue; k++) {
					if (Jugadoras[k,4] == -1) {
						if (draftMatrix[j,Jugadoras[k,2] - 1] == 0) {
							Jugadoras[k,4] = j;
							draftMatrix [j, Jugadoras[k,2] - 1] = 1;
							sigue = false;
							bool primeraVuelta = true;
							for (int l = 0; l < 5; l++) {
								if (draftMatrix[j, l] == 0) {
									primeraVuelta = false;
									break;
								}
							}
							if (primeraVuelta) { //Resetear Matriz
								for (int l = 0; l < 5; l++) {
									draftMatrix [j, l] = 0;
								}
							}
						}
					}
				}
			}
		}
	}

	void generarJugadoras(){	
		
		//Superestrellas
		generarSuperestrellas();
		//Buenas jugadoras
		generarBuenas();
		//Jugadoras de rol
		generarRol();
		//Suplentes
		generarSuplentes();

	}

	void aplicarDatos(int nombre, int apellido, int posicion, int dorsal, int equipo, int p3, int p2e, int p2i, int defe, int defI, int rebo, int rebd, int c){
			Jugadoras [c, 0] = nombre;
			Jugadoras [c, 1] = apellido;
			Jugadoras [c, 2] = posicion;
			Jugadoras [c, 3] = dorsal;
			Jugadoras [c, 4] = equipo;
			Jugadoras [c, 5] = p3;
			Jugadoras [c, 6] = p2e;
			Jugadoras [c, 7] = p2i;
			Jugadoras [c, 8] = defe;
			Jugadoras [c, 9] = defI;
			Jugadoras [c, 10] = rebo;
			Jugadoras [c, 11] = rebd;
	}

	void generarSuperestrellas(){
		//Bases
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 1;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (85, 99);
			int p2e = Random.Range (90, 99);
			int p2i = Random.Range (80, 85);

			int defe = Random.Range (85, 99);
			int defI = Random.Range (60, 90);

			int rebo = Random.Range (50, 80);
			int rebd = Random.Range (50, 90);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Escoltas
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 2;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (85, 99);
			int p2e = Random.Range (90, 99);
			int p2i = Random.Range (80, 85);

			int defe = Random.Range (75, 90);
			int defI = Random.Range (60, 90);

			int rebo = Random.Range (60, 80);
			int rebd = Random.Range (60, 90);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Aleros
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 3;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (75, 95);
			int p2e = Random.Range (90, 99);
			int p2i = Random.Range (90, 99);

			int defe = Random.Range (85, 99);
			int defI = Random.Range (60, 90);

			int rebo = Random.Range (70, 90);
			int rebd = Random.Range (70, 90);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Ala Pivots
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 4;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (60, 90);
			int p2e = Random.Range (80, 99);
			int p2i = Random.Range (85, 99);

			int defe = Random.Range (60, 90);
			int defI = Random.Range (85, 99);

			int rebo = Random.Range (85, 99);
			int rebd = Random.Range (85, 99);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Pivots
		for (int i = 0; i < 4; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 5;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (50, 95);
			int p2e = Random.Range (80, 99);
			int p2i = Random.Range (90, 99);

			int defe = Random.Range (60, 95);
			int defI = Random.Range (90, 99);

			int rebo = Random.Range (90, 99);
			int rebd = Random.Range (90, 99);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}
	}

	void generarBuenas(){
		//Bases
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 1;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (70, 85);
			int p2e = Random.Range (70, 85);
			int p2i = Random.Range (60, 75);

			int defe = Random.Range (60, 85);
			int defI = Random.Range (50, 75);

			int rebo = Random.Range (40, 60);
			int rebd = Random.Range (60, 80);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Escoltas
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 2;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (60, 80);
			int p2e = Random.Range (75, 85);
			int p2i = Random.Range (75, 85);

			int defe = Random.Range (60, 80);
			int defI = Random.Range (60, 75);

			int rebo = Random.Range (50, 70);
			int rebd = Random.Range (60, 80);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Aleros
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 3;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (60, 70);
			int p2e = Random.Range (75, 85);
			int p2i = Random.Range (75, 80);

			int defe = Random.Range (70, 80);
			int defI = Random.Range (70, 80);

			int rebo = Random.Range (70, 80);
			int rebd = Random.Range (70, 80);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, 
				p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Ala Pivots
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 4;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (40, 70);
			int p2e = Random.Range (70, 85);
			int p2i = Random.Range (80, 90);

			int defe = Random.Range (40, 80);
			int defI = Random.Range (80, 90);

			int rebo = Random.Range (75, 90);
			int rebd = Random.Range (75, 90);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Pivots
		for (int i = 0; i < 8; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 5;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (40, 65);
			int p2e = Random.Range (70, 80);
			int p2i = Random.Range (80, 90);

			int defe = Random.Range (45, 60);
			int defI = Random.Range (80, 90);

			int rebo = Random.Range (75, 90);
			int rebd = Random.Range (85, 95);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}
	}

	void generarRol(){
		//Bases
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 1;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (60, 75);
			int p2e = Random.Range (65, 75);
			int p2i = Random.Range (50, 75);

			int defe = Random.Range (60, 85);
			int defI = Random.Range (25, 50);

			int rebo = Random.Range (25, 50);
			int rebd = Random.Range (50, 70);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Escoltas
		for (int i = 0; i < 4; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 2;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (55, 75);
			int p2e = Random.Range (65, 75);
			int p2i = Random.Range (65, 80);

			int defe = Random.Range (60, 85);
			int defI = Random.Range (45, 60);

			int rebo = Random.Range (45, 60);
			int rebd = Random.Range (50, 70);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Aleros
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 3;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (50, 60);
			int p2e = Random.Range (65, 80);
			int p2i = Random.Range (70, 80);

			int defe = Random.Range (60, 75);
			int defI = Random.Range (60, 75);

			int rebo = Random.Range (60, 70);
			int rebd = Random.Range (60, 70);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Ala Pivots
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 4;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (40, 60);
			int p2e = Random.Range (60, 75);
			int p2i = Random.Range (70, 85);

			int defe = Random.Range (25, 50);
			int defI = Random.Range (70, 80);

			int rebo = Random.Range (70, 80);
			int rebd = Random.Range (70, 80);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Pivots
		for (int i = 0; i < 5; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 5;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (35, 55);
			int p2e = Random.Range (60, 70);
			int p2i = Random.Range (70, 80);

			int defe = Random.Range (25, 35);
			int defI = Random.Range (70, 80);

			int rebo = Random.Range (70, 80);
			int rebd = Random.Range (80, 90);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}
	}

	void generarSuplentes(){

		//Bases
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 1;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (55, 70);
			int p2e = Random.Range (55, 65);
			int p2i = Random.Range (50, 75);

			int defe = Random.Range (55, 65);
			int defI = Random.Range (25, 35);

			int rebo = Random.Range (25, 35);
			int rebd = Random.Range (40, 50);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Escoltas
		for (int i = 0; i < 8; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 2;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (50, 60);
			int p2e = Random.Range (60, 70);
			int p2i = Random.Range (55, 65);

			int defe = Random.Range (50, 70);
			int defI = Random.Range (40, 50);

			int rebo = Random.Range (40, 50);
			int rebd = Random.Range (50, 60);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Aleros
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 3;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (40, 60);
			int p2e = Random.Range (55, 70);
			int p2i = Random.Range (60, 70);

			int defe = Random.Range (45, 60);
			int defI = Random.Range (45, 60);

			int rebo = Random.Range (50, 70);
			int rebd = Random.Range (50, 70);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Ala Pivots
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 4;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (25, 50);
			int p2e = Random.Range (50, 65);
			int p2i = Random.Range (60, 75);

			int defe = Random.Range (25, 35);
			int defI = Random.Range (50, 70);

			int rebo = Random.Range (60, 70);
			int rebd = Random.Range (60, 70);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}

		//Pivots
		for (int i = 0; i < 7; i++) {
			int nombre = Random.Range (0, nombres.Length);
			int apellido = Random.Range (0, nombres.Length);
			int posicion = 5;
			int dorsal = Random.Range (0, 99);
			int equipo = -1;

			int p3 = Random.Range (25, 35);
			int p2e = Random.Range (50, 60);
			int p2i = Random.Range (60, 75);

			int defe = Random.Range (25, 35);
			int defI = Random.Range (60, 70);

			int rebo = Random.Range (50, 70);
			int rebd = Random.Range (60, 75);

			aplicarDatos (nombre, apellido, posicion, dorsal, equipo, p3, p2e, p2i, defe, defI, rebo, rebd, contador);
			contador++;
		}
	}
}
