using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;  // singleton, only one instance

    public static GameManager Instance
    {
        get  // allow other scripts to get info but can't change it
        {
           if(_instance == null)
            {
                GameObject gObj = new GameObject("GameManager");
                gObj.AddComponent<GameManager>();
                DontDestroyOnLoad(gObj);
            }
            return _instance;
        }
    }

    void Awake()  // run only once
    {
       if (_instance != null){
           Debug.Log("not null, destroy");
           Destroy(gameObject);
       }else{
           DontDestroyOnLoad(gameObject);
           _instance = this;
       }
       _instance = this;

    }

    [SerializeField] private int MAXLIVES = 4;
    public int lives;
    static private int prevLifeCounter;  // so can detect when changes


    // Start is called before the first frame update
    void Start()
    {
        prevLifeCounter = lives;
    }

    // Update is called once per frame
    void Update()
    {
        didPlayerLoseLife();  
    }

    void didPlayerLoseLife(){
        if (prevLifeCounter != lives) // life lost
        {
            Debug.Log("lost life");
        }
        prevLifeCounter = lives; // update

        if (lives == MAXLIVES){  // check if lost game
            Debug.Log("lost game");      
        }
    }

}
