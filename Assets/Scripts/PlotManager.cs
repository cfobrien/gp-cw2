using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Plots;

public class PlotManager : MonoBehaviour
{
    public static int numRoadPlots = 100;
	public static int numObstacles = 100;
    public static float rotationSpeed = 36.0f;
    private static float angleIncrement = 360.0f / (float)numRoadPlots;
    public static Plot[] roadPlots = new Plot[numRoadPlots];
    public static Plot[] lhsPlots = new Plot[numRoadPlots];
    public static Plot[] rhsPlots = new Plot[numRoadPlots];
	public static Plot[] obstaclePlots =  new Plot[numObstacles];
    public float roadLen, roadHeight;

    private float t = 0.0f;

    public GameObject road, building5, building8, patio, obstacle;

    Vector3 GetPlotSize(GameObject gameObject) {
        return gameObject.GetComponent<MeshRenderer>().bounds.size;
    }

    public float GetInradius(GameObject gameObject) {
        Vector3 plotSize = GetPlotSize(gameObject);
        float plotHeight = plotSize.y;
        float plotLen = plotSize.z;
        // Calculate inradius (apothem) of concentric circle to numRoadPlots-sided regular polygon
        float inradius = roadLen / (2 * Mathf.Tan(Mathf.PI / (float)numRoadPlots));
        return inradius;
    }

    bool IsPlaceable(GameObject gameObject, Plot[] side, int i) {
        return (side[i] == null);
        // return (side[(i-1+numRoadPlots)%numRoadPlots] == null)
        //     && (side[(i+1)%numRoadPlots] == null);
    }

    void GenRoad(GameObject rootRoad) {
        float inradius = GetInradius(rootRoad);
        for (int i = 0; i < roadPlots.Length; i++) {
            roadPlots[i] = MakePlot<Road>(Instantiate(road),
                                          (inradius-roadHeight)*Vector3.up,
                                          "Road " + (i+1).ToString());
            roadPlots[i].Rotate(angleIncrement*(i));
        }
    }

    void GenPlots(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            int plotIndexCandidate;
            bool rightHandSide = rand.Next(-1,1) == 0 ? true : false;
            Plot[] side = rightHandSide ? rhsPlots : lhsPlots;
            GameObject plot = Instantiate(root);
            do {
                plotIndexCandidate = rand.Next(0, numRoadPlots);
            } while (!IsPlaceable(plot, side, plotIndexCandidate));

            float inradius = GetInradius(plot);
            if (rightHandSide) {
                plot.GetComponent<Transform>().position = -2.0f*Vector3.right;
                plot.GetComponent<Transform>().rotation *= Quaternion.Euler(180*Vector3.up);
            } else {
                plot.GetComponent<Transform>().position = 2.0f*Vector3.right;
                plot.GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.zero);
            }
            lhsPlots[plotIndexCandidate] = MakePlot<Plot>(plot, inradius*Vector3.up);
            lhsPlots[plotIndexCandidate].Rotate(angleIncrement*plotIndexCandidate);
        }
    }

	void GenObstacles(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            int obstacleIndexCandidate;
			float rndXPos = (float)(rand.NextDouble() - 0.5); // range from - 0.5 to 0.5

            GameObject plot = Instantiate(root);
            obstacleIndexCandidate = rand.Next(0, numObstacles);

            float inradius = GetInradius(plot);
            plot.GetComponent<Transform>().position = rndXPos*Vector3.right;
            plot.GetComponent<Transform>().rotation *= Quaternion.Euler(Vector3.up);

            obstaclePlots[obstacleIndexCandidate] = MakePlot<Plot>(plot, inradius*Vector3.up);
            obstaclePlots[obstacleIndexCandidate].Rotate(angleIncrement*obstacleIndexCandidate);
        }
    }

    protected T MakePlot<T>(GameObject gameObject, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, name});
    }
    protected T MakePlot<T>(GameObject gameObject, Transform transform, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, transform, name});
    }
    protected T MakePlot<T>(GameObject gameObject, Vector3 position, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, position, name});
    }

    void Awake()
    {
        Vector3 roadSize = GetPlotSize(road);
        roadLen = roadSize.z;
        roadHeight = roadSize.y;

        GenRoad(road);
        GenPlots(building5, 5);
        GenPlots(building8, 5);
        GenPlots(patio, 2);
		GenObstacles(obstacle,20);
    }

    void Update()
    {
        t = Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) {
            for (int i = 0; i < numRoadPlots; i++) {
                roadPlots[i].Rotate(-rotationSpeed * t);
				if (obstaclePlots[i] != null){
					obstaclePlots[i].Rotate(-rotationSpeed * t);
				}
                if(lhsPlots[i] != null) {
                    lhsPlots[i].Rotate(-rotationSpeed * t);
                }
                if(rhsPlots[i] != null) {
                    rhsPlots[i].Rotate(-rotationSpeed * t);
                }
            }
        }
    }
}
