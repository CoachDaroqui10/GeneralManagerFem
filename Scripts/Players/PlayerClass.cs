using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {
	
	CreatePlayers cp;

	//Info
	int nombre;
	int apellido;
	string nombreS;
	string apellidoS;
	int posicion;
	int dorsal;
	int equipo;
	//Ataque
	int pt3;
	int pt2Ext;
	int pt2Int;
	//Defensa
	int defExt;
	int defInt;
	//Rebote
	int rebOfe;
	int rebDef;
	//Stats
	int[] stats;

	// Use this for initialization
	void Start () {
		
		stats = new int[12];
		for (int i = 0; i < stats.Length; i++) {
			stats [i] = 0;
		}
		cp = GameObject.Find ("PlayersManager").GetComponent<CreatePlayers> ();
	}

	public void asignarVariables(int nom, int ape, int pos, int dor, int eq, int p3, int p2e, int p2i, int defe, int defi, int rebo, int rebd) {
		nombre = nom;
		apellido = ape;
		posicion = pos;
		dorsal = dor;
		equipo = eq;

		pt3 = p3;
		pt2Ext = p2e;
		pt2Int = p2i;
		defExt = defe;
		defInt = defi;
		rebOfe = rebo;
		rebDef = rebd;
	}

	public string devolverNomYApe() {
		return nombreS + " " + apellidoS;
	}
	public void setNomYApe (string n, string a) {
		nombreS = n;
		apellidoS = a;
	}

	public void setStats(int stat, int num){
		stats [stat] += num;
	}
	public void resetStats(){
		for (int i = 0; i < 12; i++) {
			stats [i] = 0;
		}
	}
	public int[] devolverStats() { return stats; }

	public int devolverEquipo() { return equipo; }
	public void setEquipo(int e) { equipo = e; }
	public int devolverPosicion() { return posicion; }

	public int devolver3Pt() { return pt3; }
	public int devolver2PtExt() { return pt2Ext; }
	public int devolver2PtInt() { return pt2Int; }

	public int devolverDefExt() { return defExt; }
	public int devolverDefInt() { return defInt; }

	public int devolverRebOfe() { return rebOfe; }
	public int devolverRebDef() { return rebDef; }

	public void entrenarAta(int q) {
		if (pt3 + q <= 99) {
			pt3 += q;
		} else {
			pt3 = 99;
		}
		if (pt2Ext + q <= 99) {
			pt2Ext += q;
		} else {
			pt2Ext = 99;
		}
		if (pt2Int + q <= 99) {
			pt2Int += q;
		} else {
			pt2Int = 99;
		}
	}

	public void entrenarDefensa(int q) {
		if (defExt + q <= 99) {
			defExt += q;
		}  else {
			defExt= 99;
		}
		if (defInt + q <= 99) {
			defInt += q;
		}  else {
			defInt = 99;
		}
	}

	public void entrenarRebote (int q) {
		if (rebDef + q <= 99) {
			rebDef += q;
		}  else {
			rebDef = 99;
		}
		if (rebOfe + q <= 99) {
			rebOfe += q;
		}  else {
			rebOfe = 99;
		}
	}
}
