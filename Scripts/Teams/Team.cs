using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour {

	public int equipo;
	public string nombre;
	public Sprite campo;
	public Sprite btn;
	public Sprite entrenes;

	int victorias;
	int derrotas;

	PlayerClass[] jugadoras;
	int[,] stats;

	public Manager mg;

	PlayerClass j1;
	PlayerClass j2;
	PlayerClass j3;
	PlayerClass j4;
	PlayerClass j5;

	int preferenciaIntExt;
	public int eleccion1 = 0;
	public int eleccion2 = 0;
	public int reparto = 0;

	float ataque;
	float defensa;
	float rebote;
	float media;

	// Use this for initialization
	void Start () {
		jugadoras = new PlayerClass[10];
		stats = new int[10, 12];
		victorias = 0;
		derrotas = 0;

		//Sacar jugadoras de la base de datos
		asignarJugadoras(equipo);
		//Calcular medias de atributos
		calcularMedias();
	}
	
	// Update is called once per frame
	void Update () {
		calcularMedias ();
	}

	public void calcularMedias() {
		for (int i = 0; i < jugadoras.Length; i++) {
			int totalJ = jugadoras [i].devolver3Pt() + jugadoras [i].devolver2PtInt() 
				+ jugadoras [i].devolver2PtExt();
			float mediaJ = totalJ / 3;
			ataque += mediaJ;
		}
		ataque = ataque / jugadoras.Length;

		for (int i = 0; i < jugadoras.Length; i++) {
			int totalJ = jugadoras [i].devolverDefExt() + jugadoras [i].devolverDefInt();
			float mediaJ = totalJ / 2;
			defensa += mediaJ;
		}
		defensa = defensa / jugadoras.Length;

		for (int i = 0; i < jugadoras.Length; i++) {
			int totalJ = jugadoras [i].devolverRebDef() + jugadoras [i].devolverRebOfe();
			float mediaJ = totalJ / 2;
			rebote += mediaJ;
		}
		rebote = rebote / jugadoras.Length;

		media = (ataque + defensa + rebote) / 3;
	}

	public void asignarJugadoras(int team) {

		int[,] jugs = new int[120, 12];
		jugs = mg.devolverJugadoras ();
		int k = 0;

		//Segun el equipo, sacar de la base de datos y crear jugadoras

		for (int i = 0; i < 120; i++) {
			if (jugs[i,4] == team) {
				jugadoras [k] = gameObject.AddComponent<PlayerClass> ();
				jugadoras [k].asignarVariables (jugs [i, 0], jugs [i, 1], jugs [i, 2], 
					jugs [i, 3], jugs [i, 4], jugs [i, 5], jugs [i, 6], jugs [i, 7], 
					jugs [i, 8], jugs [i, 9], jugs [i, 10], jugs [i, 11]);
				jugadoras [k].setNomYApe (mg.devolverNombre (jugs [i, 0]), 
					mg.devolverApellido (jugs [i, 1]));
				k++;
			}
			if (k == 10) {
				break;
			}
		}
	}

	public int devolverStat (int player, string stat) {

		if (stat == "3p") {
			return jugadoras [player].devolver3Pt();
		}
		if (stat == "p2e") {
			return jugadoras [player].devolver2PtExt ();
		}
		if (stat == "p2i") {
			return jugadoras [player].devolver2PtInt ();
		}

		if (stat == "defe") {
			return jugadoras [player].devolverDefExt ();
		}
		if (stat == "defi") {
			return jugadoras [player].devolverDefInt ();
		}

		if (stat == "rebd") {
			return jugadoras [player].devolverRebDef ();
		}
		if (stat == "rebo") {
			return jugadoras [player].devolverRebDef ();
		}

		return -1;
	}

	public void entrenar(int pos, int atr, int cant) {
		foreach (PlayerClass jug in jugadoras) {
			if (jug.devolverPosicion () == pos) {
				if (atr == 1) {
					jug.entrenarAta (cant);
				} else if (atr == 2) {
					jug.entrenarDefensa (cant);
				} else if (atr == 3) {
					jug.entrenarRebote (cant);
				}
			}
		}
	}

	public string devolverNombre (int player) { return jugadoras [player].devolverNomYApe (); }

	public void setEleccion (int ele) {
		if (eleccion1 == 0) {
			eleccion1 = ele;
		} else if (eleccion2 == 0 && ele != eleccion1){
			eleccion2 = ele;
		} else if (ele != eleccion1) {
			eleccion1 = eleccion2;
			eleccion2 = ele;
		}
		if (eleccion1 == eleccion2) {
			eleccion2 = 0;
		}
	}

	public void setReparto (int rep) { 
		reparto = rep; 
	}
	public int devolverReparto () { return reparto; }
	public int devolverE1 () { return eleccion1; }
	public int devolverE2 () { return eleccion2; }

	public void setStats (int jug, int stat, int num){
		jugadoras [jug].setStats (stat, num);
	}

	public void crearMatrizStats(){
		for (int i = 0; i < jugadoras.Length; i++) { // Jugadoras
			for (int j = 0; j < 12; j++) { // Stats
				stats[i, j] = jugadoras[i].devolverStats()[j];
			}
		}
	}

	public int[,] devolverStats() { return stats; }

	public int devolverAta () { return Mathf.RoundToInt(ataque); }
	public int devolverDef () { return Mathf.RoundToInt(defensa); }
	public int devolverReb () { return Mathf.RoundToInt(rebote); }
	public int devolverMed () { return Mathf.RoundToInt(media); }

	public int devolverV () { return victorias; }
	public void setV() { victorias++; }
	public int devolverL () { return derrotas; }
	public void setL() { derrotas++; }
	public void cargarV(int v) { victorias = v; }
	public void cargarL(int l) { derrotas = l; }

	public PlayerClass devolverJugadora(int j) { return jugadoras [j]; }

	public void loadStats(int[,] carga) {
		stats = carga;
	}
}
