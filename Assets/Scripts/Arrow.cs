using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float timer = 0f;
    private float turnTime = 0f;
    private bool flashing = false;
    Material[] colors;

    // Update is called once per frame
    void Update()
    {
        if(flashing){
            Flash();
            timer += Time.deltaTime;
        }
    }

    //initializes the color flashing of the arrow
    public void Flash(Material first, Material second, Material third, float turnTime){
        colors = new Material[3] {first, second, third};
        flashing = true;
        this.turnTime = turnTime;
        timer = 0;
    }

    //controls color of the arrow 
    private void Flash(){
        ChangeColor(colors[Mathf.Min((int)(timer/(turnTime/3)), colors.Length)]);
    }

    public void StopFlash(Material resetColor){
      ChangeColor(resetColor);
      StopFlash();
    }

    public void StopFlash(){
      flashing = false;
      timer = 0f;
    }

    public void ChangeColor(Material newColor){
      gameObject.GetComponent<MeshRenderer>().material = newColor;
    }
}
