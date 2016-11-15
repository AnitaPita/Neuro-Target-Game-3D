using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* This class is used to control the flow of the Neuro Target Practice Game.
 * Author: Anita Popescu
 * Sachs Lab, 2016.
 */

public class GameController : MonoBehaviour {

    public static float STIM1TIMER = 2f;
    public static float STIM2TIMER = 5f;
    public static float FEEDBACKTIMER = 3f;
    public static int NUMTRIALS = 5;

    enum Stage {init, stim1, stim2, feedback, reset};

    Stage gameState;

    //public GameObject hub;
    public GameObject target;
    public GameObject activeTarget;
    public GameObject text;
    private TextMesh feedback;
    public float stageTime;
    public int score;
    private bool isHome;
    private bool stim1Presented;
    private bool stim2Presented;
    private bool feedbackPresented;
    private bool roundSuccess;
    
    private int currentTrial;

	// Use this for initialization
	void Start () {
        gameState = Stage.init;
        //hub = GameObject.Find("Canvas");
        target = GameObject.Find("Cube (1)");
        text = GameObject.Find("Text");
        feedback = text.GetComponent<TextMesh>();
        text.SetActive(false);
        currentTrial = 0;
        stageTime = 0f;
        score = 0;
        resetStage();
        isHome = false;
	}
	
	// Update is called once per frame
	void Update () {
        stageTime += Time.deltaTime;

        if (gameState == Stage.init)
        {
            //chooseActiveTarget();
            if(isHome)
            {
                gameState++;
            }
        }
        else if (gameState == Stage.stim1)
        {
            if(!stim1Presented)
            {
                stageTime = 0f;
            }
            
            if(stageTime>= STIM1TIMER)
            {
                gameState++;
            }
            else
            {
                presentFirstStimulus();
            }
        }
        else if(gameState == Stage.stim2)
        {
            if (!stim2Presented)
            {
                stageTime = 0f;
                presentSecondStimulus();
            }

            if(stageTime >= STIM2TIMER||roundSuccess)
            {
                gameState++;
            }
        }
        else if(gameState == Stage.feedback)
        {
            if(!feedbackPresented)
            {
                presentFeedback();
                stageTime = 0;
            }
            if (stageTime >= FEEDBACKTIMER)
            {
                gameState++;
            }
        }
        else if(gameState == Stage.reset)
        {
            if(currentTrial+1 == NUMTRIALS)
            {
                endGame();
            }
            else
            {
                gameState = Stage.init;
                resetStage();
                currentTrial++;
            }
        }
	}

    //Called by the target when it is clicked upon
    public void TargetSelected()
    {
        if (gameState == Stage.stim2)
        {
            roundSuccess = true;
        }
    }

    //Called by HomeBase when mouse moves off or onto the center
    public void setIsHome (bool trigger)
    {
        isHome = trigger;
        if(!trigger && gameState==Stage.stim1)
        {
            DepartedCenterEarly();
        }
    }

    //If the mouse was moved off the homebase and the state was stim1,
    //then the user moved too soon. The result in the described method
    //is to restart the current trial, but another implementation may
    //also set the 
    void DepartedCenterEarly()
    {
        resetStage();
        gameState = Stage.init;
        feedback.text = "You moved too soon! Restarting trial.";
        text.SetActive(true);
    }

    //Creates a target or selects from a set of targets on the screen
    void chooseActiveTarget()
    {
        //WIP
        //activeTarget = (GameObject)Instantiate(target, new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0), Quaternion.identity);
        //activeTarget.SetActive(true);
    }
    
    //Presents the planning stimulus
    void presentFirstStimulus()
    {
        // presents the first stimulus e.g shake/rotate
        target.GetComponent<Target>().rotate();
        stim1Presented = true;
        feedback.text = "Presenting first stimulus.";
        text.SetActive(true);
    }

    //Presents the execution stimulus
    void presentSecondStimulus()
    {
        // presents the second stimulus e.g auditory cue or colour change
        target.GetComponent<AudioSource>().Play();
        stim2Presented = true;
        feedback.text = "Presenting second stimulus.";
    }

    //Presents textual feedback to user about the result of the trial
    void presentFeedback()
    {
        TextMesh feedback = text.GetComponent<TextMesh>();
        if (roundSuccess)
        {
            feedback.text = "Nice job!";
            score++;
            // present positive feedback (e.g congrats, you hit the target!)
        }
        else
        {
            feedback.text = "Oops, too slow!";
            // present negative feedback (e.g oops too late)
        }
        text.SetActive(true);
        feedbackPresented = true;
    }

    void resetStage()
    {
        stim1Presented = false;
        stim2Presented = false;
        feedbackPresented = false;
        roundSuccess = false;
        feedback.text = "Please return to the center.";
    }

    //Called when the number of trials is reached
    void endGame()
    {
        //Present Game Over animation
        feedback.text = "Game Over! You succeeded in "+score+" out of "+NUMTRIALS+" trials.";
        text.SetActive(true);
    }


}
