using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraAnimator : MonoBehaviour {

    public Camera MovingCamera;
    public bool isMoving;

    public List<GameObject> Menus;



    public Transform destinationPoint;

    public Transform MainMenuPosition;
    public Transform CharacterPosition;
    public Transform ItemMenuPosition;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(isMoving)
        {
            MovingCamera.transform.position = Vector3.Lerp(MovingCamera.transform.position, destinationPoint.position, 0.1f);
            MovingCamera.transform.rotation = Quaternion.Lerp(MovingCamera.transform.rotation, destinationPoint.rotation, 0.1f);
            if (MovingCamera.transform.position == destinationPoint.position)
            {
                isMoving = false;
            }
        }
	}

    public void MoveCamera(Transform _destinationPoint)
    {
        destinationPoint = _destinationPoint;
        isMoving = true;

        
    }

    public void ChangeMenu(GameObject Menu)
    {
       
        foreach (var menu in Menus)
        {
            if (Menu == menu)
                menu.SetActive(true);
            else
                menu.SetActive(false);
        }



    }

    

    public void CloseAllMenus()
    {
        foreach (var menu in Menus)
        {

            menu.SetActive(false);
        }
    }


}
