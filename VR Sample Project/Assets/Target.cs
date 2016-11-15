using UnityEngine;
using System.Collections;

/* In the context of Neuro Target Practice Game, this class
 * contains the logic for the instantiated Target for a 
 * given trial with which the user will interact upon
 * encountering a particular set of stimuli.
 * Author: Anita Popescu
 * Sachs Lab, 2016.
 */

public class Target : MonoBehaviour {

    private GameObject gameController;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void rotate()
    {
        transform.Rotate(Vector3.back);
    }

    void OnMouseDown()
    {
        gameController.GetComponent<GameController>().TargetSelected();
    }
}
