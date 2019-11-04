using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{
    gameController myGameController;
    public int myX, myY;
    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.Find("GameControllerObj").GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
            myGameController.ProcessClick(gameObject, myX, myY);
        
    }
}
