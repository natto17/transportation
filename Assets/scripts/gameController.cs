using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject cubePrefab;
    Vector3 cubePosition;
    public static GameObject activeCube;
    int airplaneX, airplaneY;
    GameObject[,] cubes;
    int gridWidth, gridHeight;
    bool airplaneActive;
    // Start is called before the first frame update

    void Start()
    {
        airplaneX = 0;
        airplaneY = 8;
        airplaneActive = false;

        gridWidth = 16;
        gridHeight = 9;
        cubes = new GameObject[gridWidth, gridHeight];

        //set cubes in grid
        for (int h = 0; h < gridHeight; h++)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                cubePosition = new Vector3(i * 2, h*2, 0);
                cubes[i,h]= Instantiate(cubePrefab, cubePosition, Quaternion.identity);
                cubes[i, h].GetComponent<cubeController>().myX = i;
                cubes[i, h].GetComponent<cubeController>().myY = h;
            }
        }

        //set airplane
        cubes[airplaneX, airplaneY].GetComponent<Renderer>().material.color=Color.red;
    }

    public void ProcessClick(GameObject cubeClicked, int x, int y)
    {
        // When the player clicks an airplane

 
        if (x == airplaneX && y == airplaneY)
        {   //should highlight in some way to show that it’s the active airplane. (Change to a different color, glow, enlarge, etc.)
            //(activate)
            if (!airplaneActive)
            {
                airplaneActive = true;
                cubeClicked.transform.localScale *=1.5f;
            }

            //If the player clicks the active airplane, it should deactivate.
            //(deactivate)
            else
            {
                airplaneActive = false;
                cubeClicked.transform.localScale /= 1.5f;
            }
        }

        //If the player clicks clear sky(a white cube) while there is an active airplane, the airplane should teleport to that location(i.e.the cube that was previously red should now be white, and the clicked cube should now be red).
        //(move cube)
        else
            if (airplaneActive)
        {
            //remove from old spot
            cubes[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
            cubes[airplaneX, airplaneY].transform.localScale /= 1.5f;

            //move to new spot
            airplaneX = x;
            airplaneY = y;
            cubes[x,y].GetComponent<Renderer>().material.color = Color.red;
            cubes[x,y].transform.localScale *= 1.5f;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
