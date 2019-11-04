using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject cubePrefab;
    Vector3 cubePosition;
    public static GameObject activeCube;
    int airplaneX, airplaneY, startX, startY, depotX, depotY;
    GameObject[,] cubes;
    int gridWidth, gridHeight;
    bool airplaneActive;
    float turnLength, turnTimer;
    int airplaneCap, airplaneCargo;
    int cargoGain;
    int score;
    // Start is called before the first frame update

    void Start()
    {
        gridWidth = 16;
        gridHeight = 9;
        cubes = new GameObject[gridWidth, gridHeight];

        startX = 0;
        startY = gridHeight-1;

        airplaneX = startX;
        airplaneY = startY;

        airplaneActive = false;

        turnLength = 1.5f;
        turnTimer = turnLength;

        airplaneCap = 90;
        airplaneCargo = 0;
        cargoGain = 10;
        score = 0;

        depotX = gridWidth-1;
        depotY = 0;



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

        //set airplane + depot
        cubes[airplaneX, airplaneY].GetComponent<Renderer>().material.color=Color.red;
        cubes[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;
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
                cubeClicked.transform.localScale *= 1.5f;
            } else
            {

                //If the player clicks the active airplane, it should deactivate.
                //(deactivate)

                airplaneActive = false;
                cubeClicked.transform.localScale /= 1.5f;
            }
        } else
        {

            //If the player clicks clear sky(a white cube) while there is an active airplane, the airplane should teleport to that location(i.e.the cube that was previously red should now be white, and the clicked cube should now be red).
            //(move cube)

            if (airplaneActive)
            {
                //remove from old spot
                if (airplaneX == depotX && airplaneY == depotY)
                {
                    cubes[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;
                    cubes[depotX, depotY].transform.localScale /= 1.5f;

                } else
                {
                    cubes[airplaneX, airplaneY].GetComponent<Renderer>().material.color = Color.white;
                    cubes[airplaneX, airplaneY].transform.localScale /= 1.5f;
                }

                //move to new spot
                airplaneX = x;
                airplaneY = y;
                cubes[x, y].GetComponent<Renderer>().material.color = Color.red;
                cubes[x, y].transform.localScale *= 1.5f;
            }
        }
    }

    void LoadCargo()
    {
        if (airplaneX == startX && airplaneY == startY)
        {
            airplaneCargo += cargoGain;
                airplaneCargo = Math.Min(airplaneCargo, airplaneCap);
        }
    }
    void DeliverCargo()
    {
        if (airplaneX== depotX && airplaneY == depotY)
        {
            score += airplaneCargo;
            airplaneCargo = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > turnTimer)
        {//take a turn
            //upper left, give cargo
            LoadCargo();
            //lower right, score points
            DeliverCargo();

            print("Cargo: " + airplaneCargo+ " _ Score: " + score);
            turnTimer += turnLength;
        }
    }
}
