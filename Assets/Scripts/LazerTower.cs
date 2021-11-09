using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTower : MonoBehaviour
{

    public GameObject barrel;
    public LineRenderer lineRenderer;

    public int team;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot(){
        lineRenderer.positionCount = 1;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        lineRenderer.startWidth = .03f;
        lineRenderer.endWidth = .03f;

        lineRenderer.SetPosition(0, barrel.transform.position);
        Vector3 curPos = barrel.transform.position;
        Vector3 curDir = -barrel.transform.up;

        RaycastHit hit;



        //Debug.DrawRay(barrel.transform.position ,-barrel.transform.up, Color.red);
        //Debug.DrawLine(barrel.transform.position , barrel.transform.position + 20 * barrel.transform.right, Color.blue);
        //Debug.Log(hit.collider.gameObject.name);

        int i = 1;
        do
        {

            Physics.Raycast(curPos, curDir, out hit, 30f);
            if (hit.collider != null)
            {

                if(i == lineRenderer.positionCount) { lineRenderer.positionCount++; }
                lineRenderer.SetPosition(i, new Vector3(hit.point.x, hit.point.y, hit.point.z));
                i++;

                curPos = hit.point;
                curDir = Vector3.Reflect(curDir, hit.normal);

                if (hit.collider.gameObject.layer == 6)
                {
                    if(hit.collider.gameObject.GetComponent<Target>().team == this.team)
                    {
                        Debug.Log("WON!");
                        return;
                    }
               
                }
            }


        } while (hit.collider != null);


    }

    private void BounceLazer(Vector3 bounceDirection){

    }

}
