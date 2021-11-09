using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameRunning;
    private List<GameObject> towers = new List<GameObject>();

    public void Start(){
        StartGame();
    }

    public void Update(){
      if(gameRunning){ PlayGame(); }
    }

    public void AddTower(GameObject newTower){
      towers.Add(newTower);
    }

    public void PlayGame(){
        foreach(GameObject tower in towers){
          tower.GetComponent<LazerTower>().Shoot();
        }
    }

    public void StartGame(){ gameRunning = true;}

    public void EndGame(){ gameRunning = false;}


}
