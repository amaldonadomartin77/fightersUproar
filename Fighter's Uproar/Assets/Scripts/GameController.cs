using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}
