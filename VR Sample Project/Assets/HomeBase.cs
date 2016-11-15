using UnityEngine;
using System.Collections;

/* In the context of Neuro Target Practice Game, this class
 * contains the logic for the centre of the board to which
 * the user must return after each trial and must remain 
 * there until the execution (i.e 2nd) stimulus is presented.
 * Author: Anita Popescu
 * Sachs Lab, 2016.
 */

public class HomeBase : MonoBehaviour {

    private GameController gameController;
    private float timeHome;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        timeHome = 0f;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseOver ()
    {
        gameController.setIsHome(true);
        timeHome += Time.deltaTime;
    }

    void OnMouseExit()
    {
        gameController.setIsHome(false);
        timeHome = 0f;
    }
}
