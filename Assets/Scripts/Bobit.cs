using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobit : MonoBehaviour
{

    public List<Arrow> arrows;

    public GameManager gameManager;

    public Material green;
    public Material yellow;
    public Material red;
    public Material black;

    private int activeArrow;
    private Vector3 direction; //direction to move to the object
    private float goalDist = .05f; //distance to move the object

    private float turnTime = 3f; //time the player has to complete the turn
    private float turnInterval = 1f; //interval between turns
    private float curTimer = 0f;

    private bool inTurn = false;

    private Vector3 initPos;


    void OnEnable()
    {
      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      gameManager.AddBobbit(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if(!gameManager.gameRunning){ return; }

        if(inTurn){
            if(curTimer > turnTime){
              curTimer = 0f;
              Lose();
            }
            else{
              //check if user has moved the object in the correct direction
              Vector3 distTraveled = transform.position - initPos;
              bool x = (direction.x == 0);
              bool z = (direction.z == 0);

              if(!x){
                if(direction.x < 0){
                  x = distTraveled.x < direction.x * goalDist;
                }
                else{
                  x = distTraveled.x > direction.x * goalDist;
                }
              }
              if(!z){
                if(direction.z < 0){
                  z = distTraveled.z < direction.z * goalDist;
                }
                else{
                  z = distTraveled.z > direction.z * goalDist;
                }
              }

              if( x && z){
                //if reached position turn arrow back to default
                arrows[activeArrow].StopFlash(black);
                WinRound();
              }

            }

        }
        else{
          //if time for new turn pick a new direction and color the arrow
          if(curTimer > turnInterval){
            inTurn = true;
            curTimer = 0f;
            activeArrow = Random.Range(0, 8);
            direction = new Vector3(Mathf.Cos(Mathf.PI * activeArrow/4),
                                    0,
                                    Mathf.Sin(Mathf.PI * activeArrow/4));

            arrows[activeArrow].Flash(green, yellow, red, turnTime);

            initPos = transform.position;
          }
        }
        curTimer += Time.deltaTime;
    }

    public void Lose(){
      if(gameManager.Score > gameManager.HighScore){
        gameManager.HighScore = gameManager.Score;
      }
      gameManager.Score = 0;
      gameManager.EndGame();
      inTurn = false;
      curTimer = 0f;
      turnTime = 3f;
      arrows[activeArrow].StopFlash(black);
    }

    public void WinRound(){
      curTimer = 0f;
      inTurn = false;
      gameManager.Score++;
      turnTime *= .85f;
    }
}
