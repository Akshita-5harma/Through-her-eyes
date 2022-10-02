using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClicker : MonoBehaviour
{
    public GameObject ballRed;
    public GameObject ballGreen;
    public GameObject ballBlue;
    public Camera gameCamera;

    public GameObject BlueText;
    public GameObject RedText;
    public GameObject GreenText;

    private bool BlueDestroyed;
    private bool GreenDestroyed;
    public static bool AllDestroyed;

   

    private void Update()
    {
        if(GameManager.playMode==true&&Input.GetMouseButtonUp(0)){
        Ray ray = gameCamera.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

            if(Physics.Raycast (ray, out hit))
            {
                if(hit.collider.gameObject.tag == "BlueBall"){
                    ballBlue.SetActive(false);
                    BlueText.SetActive(false);
                    BlueDestroyed=true;
                }
                if(BlueDestroyed==true&&hit.collider.gameObject.tag == "GreenBall"){
                    ballGreen.SetActive(false);
                    GreenText.SetActive(false);
                    GreenDestroyed=true;                    
                }
                if(GreenDestroyed==true&&hit.collider.gameObject.tag == "RedBall"){
                    ballRed.SetActive(false);
                    RedText.SetActive(false);
                    AllDestroyed=true;
                }
            }
        }   
    } 

}
