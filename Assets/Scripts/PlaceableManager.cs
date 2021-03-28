using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Placeables;

public class PlaceableManager : MonoBehaviour
{
    public static int numRoadPlaceables = 100;
	public static int numObstacles = 100;
    public static int numNPCs = 50;
    public static float rotationSpeed;
    private static float angleIncrement = 360.0f / (float)numRoadPlaceables;
    public static Placeable[] roadPlaceables = new Placeable[numRoadPlaceables];
    public static Placeable[] lhsPlaceables = new Placeable[numRoadPlaceables];
    public static Placeable[] rhsPlaceables = new Placeable[numRoadPlaceables];
	public static Placeable[] obstaclePlaceables =  new Placeable[numObstacles];
    public static NPC[] npcs = new NPC[numNPCs];
    public float roadLen, roadHeight;
    public float roadRadius;

    private float t = 0.0f;
    private float roadsPerSec = 5.0f;      // number of road Placeables covered by player in 1 second

    public GameObject road;
    public GameObject building2, building5, building8, patio;
    public GameObject npc1, npc2, npc3, npc4;

    Vector3 GetPlaceableSize(GameObject gameObject) {
        return gameObject.GetComponent<MeshRenderer>().bounds.size;
    }

    public float GetInradius(GameObject gameObject) {
        Vector3 placeableSize = GetPlaceableSize(gameObject);
        float placeableHeight = placeableSize.y;
        float placeableLen = placeableSize.z;
        // Calculate inradius (apothem) of concentric circle to numRoadPlaceables-sided regular polygon
        float inradius = roadLen / (2 * Mathf.Tan(Mathf.PI / (float)numRoadPlaceables));
        return inradius;
    }

    bool IsPlaceable(GameObject gameObject, Placeable[] side, int i) {
        return (side[i] == null);
        // return (side[(i-1+numRoadPlaceables)%numRoadPlaceables] == null)
        //     && (side[(i+1)%numRoadPlaceables] == null);
    }

    void GenRoad(GameObject rootRoad) {
        float inradius = GetInradius(rootRoad);
        roadRadius = inradius;
        for (int i = 0; i < roadPlaceables.Length; i++) {
            roadPlaceables[i] = MakePlaceable<Road>(Instantiate(road),
                                          (inradius-roadHeight)*Vector3.up,
                                          "Road " + (i+1).ToString());
            roadPlaceables[i].Rotate(angleIncrement*(i));
        }
    }

    void GenPlaceables(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            int placeableIndexCandidate;
            bool rightHandSide = rand.Next(-1,1) == 0 ? true : false;
            Placeable[] side = rightHandSide ? rhsPlaceables : lhsPlaceables;
            GameObject placeable = Instantiate(root);
            do {
                placeableIndexCandidate = rand.Next(0, numRoadPlaceables);
            } while (!IsPlaceable(placeable, side, placeableIndexCandidate));

            float inradius = GetInradius(placeable);
            if (rightHandSide) {
                placeable.GetComponent<Transform>().position = -2.0f*Vector3.right;
                placeable.GetComponent<Transform>().rotation *= Quaternion.Euler(180*Vector3.up);
            } else {
                placeable.GetComponent<Transform>().position = 2.0f*Vector3.right;
                placeable.GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.zero);
            }
            lhsPlaceables[placeableIndexCandidate] = MakePlaceable<Placeable>(placeable, inradius*Vector3.up);
            lhsPlaceables[placeableIndexCandidate].Rotate(angleIncrement*placeableIndexCandidate);
        }
    }

	void GenObstacles(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            int obstacleIndexCandidate;
			float rndXPos = (float)(rand.NextDouble() - 0.5); // range from - 0.5 to 0.5

            GameObject placeable = Instantiate(root);
            obstacleIndexCandidate = rand.Next(0, numObstacles);

            float inradius = GetInradius(placeable);
            placeable.GetComponent<Transform>().position = rndXPos*Vector3.right;
            placeable.GetComponent<Transform>().rotation *= Quaternion.Euler(Vector3.up);

            obstaclePlaceables[obstacleIndexCandidate] = MakePlaceable<Placeable>(placeable, inradius*Vector3.up);
            obstaclePlaceables[obstacleIndexCandidate].Rotate(angleIncrement*obstacleIndexCandidate);
        }
    }

    void GenNPCs(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            int NPCIndexCandidate;
			float rndXPos = (float)(rand.NextDouble() - 0.5); // range from - 0.5 to 0.5

            GameObject npc = Instantiate(root);
            NPCIndexCandidate = rand.Next(0, numNPCs);

            float inradius = GetInradius(npc);
            npc.GetComponent<Transform>().position = rndXPos*Vector3.right;
            npc.GetComponent<Transform>().rotation *= Quaternion.Euler(Vector3.up);

            npcs[NPCIndexCandidate] = MakePlaceable<NPC>(npc, inradius*Vector3.up);
            npcs[NPCIndexCandidate].Rotate(angleIncrement*NPCIndexCandidate);
        }
    }

    protected T MakePlaceable<T>(GameObject gameObject, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, name});
    }
    protected T MakePlaceable<T>(GameObject gameObject, Transform transform, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, transform, name});
    }
    protected T MakePlaceable<T>(GameObject gameObject, Vector3 position, string name = null) {
        return (T)Activator.CreateInstance(typeof(T), new object[] {gameObject, position, name});
    }

    void Awake()
    {
        Vector3 roadSize = GetPlaceableSize(road);
        roadLen = roadSize.z;
        roadHeight = roadSize.y;

        rotationSpeed = (360 * roadsPerSec) / numRoadPlaceables;

        GenRoad(road);
		//GenPlaceables(building2, 20);
        GenPlaceables(building5, 20);
        GenPlaceables(building8, 20);
        GenPlaceables(patio, 2);
        GenNPCs(npc1, 20);
        GenNPCs(npc2, 20);
        GenNPCs(npc3, 20);
        GenNPCs(npc4, 20);
    }

    void UpdatePlaceables(Placeable[] placeables) {
        for (int i = 0; i < placeables.Length; i++) {
            if (placeables[i] != null) {
                placeables[i].Rotate(-rotationSpeed * t);
            }
        }
    }

    void Update()
    {
        t = Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) {
            UpdatePlaceables(roadPlaceables);
            UpdatePlaceables(npcs);
            UpdatePlaceables(lhsPlaceables);
            UpdatePlaceables(rhsPlaceables);
        }
    }
}
