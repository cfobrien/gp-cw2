using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Placeables;

public class PlaceableManager : MonoBehaviour
{
    public static int numRoadPlaceables = 100;
	public static int numObstacles = 100;
    public static int numNPCs = 100;
    public static float rotationSpeed, rotationSpeedScale;
    private static float angleIncrement = 360.0f / (float)numRoadPlaceables;
    public static Road[] roadPlaceables = new Road[numRoadPlaceables];
    public static Placeable[] lhsPlaceables = new Placeable[numRoadPlaceables];
    public static Placeable[] rhsPlaceables = new Placeable[numRoadPlaceables];
	public static Obstacle[] obstacles =  new Obstacle[numObstacles];
    public static NPC[] npcs = new NPC[numNPCs];
    public float roadLen, roadHeight;
    public float roadRadius;

    private float t = 0.0f;
    private float roadsPerSec = 2.0f;      // number of road Placeables covered by player in 1 second

    public GameObject player;
    public GameObject cylinder;
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

    public float GetRadius(GameObject gameObject) {
        Vector3 placeableSize = GetPlaceableSize(gameObject);
        float placeableHeight = placeableSize.y;
        float placeableLen = placeableSize.z;
        // Calculate radius of concentric circle to numRoadPlaceables-sided regular polygon
        float radius = roadLen / (2 * Mathf.Sin(Mathf.PI / (float)numRoadPlaceables));
        return radius;
    }

    int GetEmptyIndex(Placeable[] placeables, System.Random rand) {
        List<int> explored = new List<int>();
        int numExplored = 0;
        int indexCandidate = -1;
        do {
            indexCandidate = rand.Next(0, placeables.Length);
            if (!explored.Contains(indexCandidate)) {
                explored.Add(indexCandidate);
                numExplored++;
            }
            if (numExplored == placeables.Length) {// if (explored.Capacity == placeables.Length) {
                return -1;
            }
        } while(placeables[indexCandidate] != null);
        return indexCandidate;
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
            bool rightHandSide = rand.Next(-1,1) == 0 ? true : false;
            Placeable[] side = rightHandSide ? rhsPlaceables : lhsPlaceables;

            int index = GetEmptyIndex(side, rand);
            if (index == -1) {
                Debug.Log("No more space in array");
            } else {
                GameObject placeable = Instantiate(root);

                float inradius = GetInradius(placeable);
                if (rightHandSide) {
                    placeable.GetComponent<Transform>().position = -2.0f*Vector3.right;
                    placeable.GetComponent<Transform>().rotation *= Quaternion.Euler(180*Vector3.up);
                } else {
                    placeable.GetComponent<Transform>().position = 2.0f*Vector3.right;
                    placeable.GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.zero);
                }
                lhsPlaceables[index] = MakePlaceable<Placeable>(placeable, inradius*Vector3.up);
                lhsPlaceables[index].Rotate(angleIncrement*index);
            }
        }
    }

	void GenObstacles(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
            float rndXPos = (float)(rand.NextDouble() - 0.5); // range from - 0.5 to 0.5

            int index = GetEmptyIndex(obstacles, rand);
            if (index == -1) {
                Debug.Log("No more space in array");
            } else {
                GameObject obstacle = Instantiate(root);

                float inradius = GetInradius(obstacle);
                obstacle.GetComponent<Transform>().position = rndXPos*Vector3.right;
                obstacle.GetComponent<Transform>().rotation *= Quaternion.Euler(Vector3.up);

                obstacles[index] = MakePlaceable<Obstacle>(obstacle, inradius*Vector3.up);
                obstacles[index].Rotate(angleIncrement*index);
            }
        }
    }

    void GenNPCs(GameObject root, int num) {
        System.Random rand = new System.Random();
        for (int i = 0; i < num; i++) {
			float rndXPos = (float)(rand.NextDouble() - 0.5); // range from - 0.5 to 0.5

            int npcIndex = GetEmptyIndex(npcs, rand);
            if (npcIndex == -1) {
                Debug.Log("No more space in npcs array");
            } else {
                GameObject npc = Instantiate(root);

                float inradius = GetInradius(npc);
                npc.GetComponent<Transform>().position = rndXPos*Vector3.right;
                npc.GetComponent<Transform>().rotation *= Quaternion.Euler(Vector3.up);

                npcs[npcIndex] = MakePlaceable<NPC>(npc, inradius*Vector3.up);
                npcs[npcIndex].Rotate(angleIncrement*npcIndex);
            }
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
        rotationSpeedScale = 1.0f;

        cylinder.transform.localScale = Vector3.one * 2.0f * GetRadius(road);

        GenRoad(road);
		GenPlaceables(building2, 10);
        GenPlaceables(building5, 10);
        GenPlaceables(building8, 10);
        GenPlaceables(patio, 10);

        // GenObstacles(patio, 3);

        GenNPCs(npc1, 20);
        GenNPCs(npc2, 20);
        GenNPCs(npc3, 20);
        GenNPCs(npc4, 20);
    }

    void UpdatePlaceables(Placeable[] placeables) {
        foreach (Placeable placeable in placeables) {
            if (placeable != null) {
                placeable.Rotate(-rotationSpeed * rotationSpeedScale * t);
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
            UpdatePlaceables(obstacles);
        }
        int count = 0;
        int countnonnull = 0;
        foreach (NPC npc in npcs) {
            count++;
            if (npc != null) {
                countnonnull++;
                // npc.RandomWalk(Vector3.up, 0.001f);
                npc.Face(player.transform);
            }
        }
        // Debug.Log("NPC array slots checked: " + count.ToString());
        // Debug.Log("non null NPC array slots: " + countnonnull.ToString());
    }
}
