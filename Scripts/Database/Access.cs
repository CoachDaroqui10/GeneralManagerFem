/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Access : MonoBehaviour {

	private DBConnector _connector;

	// Use this for initialization
	void Start () {

		_connector = gameObject.AddComponent<DBConnector> ();

		_connector.OpenDB ("URI=file:Assets\\Database\\JugadorasDB.db");
		//_connector.InsertData ("Belen", "Garcia", 3, 10, 79, 75, 82, 3);
		_connector.SelectData ();
		//_connector.UpdateAtaque (94);
		//_connector.SelectData ();
		_connector.CloseDB ();
	}

}
*/