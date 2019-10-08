/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DBManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		string conn = "URI=file:Assets\\Database\\JugadorasDB.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		dbconn.Open ();
		IDbCommand dbcmd = dbconn.CreateCommand ();

		string query = "UPDATE Jugadoras SET Ataque = '' WHERE Nombre = 'Belen'";
		dbcmd.CommandText = query;
		IDataReader reader = dbcmd.ExecuteReader ();

		while (reader.Read ()) {

			string nombre = reader.GetString (0);

			Debug.Log (nombre);

		}

		reader.Close();
		reader = null;
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
		dbconn = null;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
*/