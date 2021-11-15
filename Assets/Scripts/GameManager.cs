using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class controls game states and on screen text
//some things are left over from original lazer game idea as
//a demonstration of what I did
public class GameManager : MonoBehaviour
{
    public Text debugText;
    public Text RoundOverText;

    public int Score;
    public int HighScore;

    public bool gameRunning;
    private List<GameObject> bobbit = new List<GameObject>();

    public void Update(){
      // if(gameRunning){ PlayGame(); }

      if(gameRunning){
        debugText.gameObject.SetActive(true);
        debugText.text = "Score: " + Score + "\n" + "HighScore: " + HighScore; //"X: " + towers[0].transform.position.x + ", Z: " + towers[0].transform.position.z;
      }
    }

    public void AddBobbit(GameObject newBobbit){
      bobbit.Add(newBobbit);
    }


    public void StartGame(){
      RoundOverText.gameObject.SetActive(false);
      gameRunning = true;
    }

    public void EndGame(){
      RoundOverText.gameObject.SetActive(true);
      gameRunning = false;
    }


}
