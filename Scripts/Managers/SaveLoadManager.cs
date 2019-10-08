using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	//Jugadoras
	public static void SavePlayers(Manager m) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath
		                    + "/players.sav", FileMode.Create);

		PlayersData data = new PlayersData (m);

		bf.Serialize (stream, data);
		stream.Close ();

	}

	public static int[,] LoadPlayers() {
		if (File.Exists(Application.persistentDataPath+ "/players.sav")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath
				+ "/players.sav", FileMode.Open);
			PlayersData data = bf.Deserialize(stream) as PlayersData;

			stream.Close();
			return data.players;

		} else {
			Debug.LogError ("NOP");
			int[,] vacio = new int[120, 12];
			return vacio;
		}
	}

	//TEMPORADA
	public static void SaveSeason(Manager m) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath
			+ "/season.sav", FileMode.Create);

		SeasonData data = new SeasonData (m);

		bf.Serialize (stream, data);
		stream.Close ();

	}

	public static int[,] LoadSeason() {
		if (File.Exists(Application.persistentDataPath+ "/season.sav")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath
				+ "/season.sav", FileMode.Open);
			SeasonData data = bf.Deserialize(stream) as SeasonData;

			stream.Close();
			return data.balance;

		} else {
			Debug.LogError ("NOP");
			return null;
		}
	}

	//Stats

	public static void SaveStats(Manager m) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath
			+ "/stats.sav", FileMode.Create);

		StatsData data = new StatsData (m);

		bf.Serialize (stream, data);
		stream.Close ();

	}

	public static int[,] LoadStats() {
		if (File.Exists(Application.persistentDataPath+ "/stats.sav")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath
				+ "/stats.sav", FileMode.Open);
			StatsData data = bf.Deserialize(stream) as StatsData;

			stream.Close();
			return data.stats;

		} else {
			Debug.LogError ("NOP");
			int[,] vacio = new int[10, 12];
			return vacio;
		}
	}

	// Mejoras

	public static void SaveTrain(Manager m) {

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath
			+ "/train.sav", FileMode.Create);

		MejorasData data = new MejorasData (m);

		bf.Serialize (stream, data);
		stream.Close ();

	}

	public static int[,] LoadTrain() {
		if (File.Exists(Application.persistentDataPath+ "/train.sav")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath
				+ "/train.sav", FileMode.Open);
			MejorasData data = bf.Deserialize(stream) as MejorasData;

			stream.Close();
			return data.mejoras;

		} else {
			Debug.LogError ("NOP");
			int[,] vacio = new int[5,3];
			return vacio;
		}
	}
}

[Serializable]
public class PlayersData {
	public int[,] players;

	public PlayersData(Manager m) {
		players = new int[120, 12];
		players = m.devolverJugadoras ();
	}
}

[Serializable]
public class SeasonData {
	public int[,] balance;

	public SeasonData(Manager m) {
		balance = new int[13,2];

		balance = m.balanceVL ();
	}
}

[Serializable]
public class StatsData {
	public int[,] stats;

	public StatsData(Manager m) {
		stats = new int[120, 12];

		stats = m.devolverStats ();
	}
}

[Serializable]
public class MejorasData {
	public int[,] mejoras;

	public MejorasData(Manager m) {
		mejoras = new int[5,3];
		mejoras = m.mejoras;
	}
}
