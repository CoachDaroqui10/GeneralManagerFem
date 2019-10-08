/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DBConnector : MonoBehaviour {

	private SqliteConnection _conexion;
	private SqliteCommand _command;
	private SqliteDataReader _reader;

	private string _query;

	public void OpenDB(string _dbName) {
		_conexion = new SqliteConnection (_dbName);
		_conexion.Open ();
	}

	public void SelectData() {
		_query = "SELECT * FROM Jugadoras WHERE Equipo = '1'";
		_command = _conexion.CreateCommand ();
		_command.CommandText = _query;
		_reader = _command.ExecuteReader ();

		if (_reader != null) {
			while (_reader.Read()) {
				print (_reader.GetValue (0).ToString () + " - "
					+ _reader.GetValue (1).ToString () + " - "
				+ _reader.GetValue (2).ToString () + " - "
				+ _reader.GetValue (3).ToString () + " - "
				+ _reader.GetValue (4).ToString () + " - "
				+ _reader.GetValue (5).ToString () + " - "
				+ _reader.GetValue (6).ToString () + " - "
				+ _reader.GetValue (7).ToString ());
			}
		}
	}

	public void InsertData (string _nombre, string _apellido, int _posicion, int _dorsal,
		int _ataque, int _defensa, int _rebote, int _equipo) {

		_query = "INSERT INTO Jugadoras VALUES('" + _nombre + "', '" + _apellido
			+ "', '" + _posicion + "', '" + _dorsal + "', '" + _ataque + "', '" + _defensa 
			+ "', '" + _rebote + "', '" + _equipo + "')";

		using (_command = _conexion.CreateCommand ()) {
			_command.CommandText = _query;
			_command.ExecuteReader ();
		}

	}

	public void UpdateAtaque (int _ataque) {
		_query = "UPDATE Jugadoras SET Ataque = '" + _ataque + "' WHERE Nombre = 'Belen'";
		_command = _conexion.CreateCommand ();
		_command.CommandText = _query;
		try {
			_command.ExecuteNonQuery();
		} catch (System.Exception ex) {
			print ("A tomar por culo");
		}

	}

	public void CloseDB() {
		_reader.Close ();
		_reader = null;
		_command = null;

		_conexion.Close ();
		_conexion = null;
	}

}*/
