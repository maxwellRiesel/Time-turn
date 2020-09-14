using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder : MonoBehaviour
{

    float TurnTime = 10f;//this sets all of the timers for the code, chaging this number changes all the timers
    float CurrentTime = 0f;//this will be the dynamic time variable
    bool MyTurn = true;//just helps with displaying turn order, if true it is your turn, if false the opponents
    bool CombatTimer = true;// this will set the combat timer's value inside of the update section
    int AttackPhase = 0;// this manages how many phases have been played, if it reaches two it will iniate the attack phase
    public Text TurnText;//displays what turn or phase it is currently 
    public Text TimerText;// displays timer
    public Button TurnChanger;//button that you click to change the turn

    void Start()
    {
        CurrentTime = TurnTime;//current time is set to turn time
        MyTurn = Random.value > .5f;//randomizes first turn
    }

   

    public void Onclick()
    {
        if (MyTurn)//changes the turn when clicked to enemy's
        {
            MyTurn = false;
            CurrentTime = TurnTime;
            AttackPhase++;
        }
        else if (!MyTurn)//changes the turn when clicked to players
        {
            MyTurn = true;
            CurrentTime = TurnTime;
            AttackPhase++;
        }
    }

    void Update()
    {
        //decreases current time and displays it
        CurrentTime -= 1 * Time.deltaTime;
        TimerText.text ="Time remaining:" + CurrentTime.ToString("f0");

        //chenges the text of the button to tell you whose turn it is
        if (MyTurn && TurnText.text != " Your Turn ")
        {
            TurnText.text = " Your Turn ";
        }
        else if (!MyTurn && TurnText.text != " Enemy Turn ")
        {
            TurnText.text = " Enemy Turn ";
        }


        if (AttackPhase == 2)//checks that both players have hade a turn to play then plays the combat step
        {
            /*faux start function inside of the update that sets the timer for combat,
            the timer is more just for show until we have things happening in 
            attack phase to show we do have multiple phases*/
            if (CombatTimer)
            {
                CurrentTime = TurnTime / 2f;
                CombatTimer = false;
            }

            CurrentTime -= 1 * Time.deltaTime;

            if(TurnText.text != " Combat ocurring... ")
            {
                TurnText.text = " Combat ocurring... ";
            }
               
            if (CurrentTime <= 0)//exits combat
            {
                CombatTimer = true;//rests the combat timer for further combat
                AttackPhase = 0;
                CurrentTime = TurnTime;// this sets the time for the next turn, otherwise it will immediately end the turn following combat
            }
            
        }

        else if (CurrentTime <= 0f && MyTurn)//changes the turn if the turn timer ends to enemy's
        {
            MyTurn = false;
            CurrentTime = TurnTime;
            AttackPhase++;
        }
        else if(CurrentTime <=0f && !MyTurn)//changes the turn if the turn timer ends to players
        {
            MyTurn = true;
            CurrentTime = TurnTime;
            AttackPhase++;
        }

        
    }
}
