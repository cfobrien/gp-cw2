using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public HealthBar healthBar;  // ref to health bar
    [SerializeField] public int MAXLIVES = 4;
    public int lives = 0;
	public int level = 0;  // based on btn from scene select
    static private int prevLifeCounter;  // so can detect when changes


    // Start is called before the first frame update
    void Start()
    {
		if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2){
			return;
		}
		// healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        // healthBar.SetMaxHealth(MAXLIVES);;
		// lives = 0;
		// prevLifeCounter = lives;

    }

	void OnLevelWasLoaded()  // start and awake dont run on other scenes bc dont destroy on load
    {
		if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2){
			return;
		}
		healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(MAXLIVES);;
		lives = 0;
		prevLifeCounter = lives;
		Debug.Log(lives);
        if (level == 0) {
            GameObject.Find("Sky").GetComponent<SpriteRenderer>().sprite = GameObject.Find("rome").GetComponent<SpriteRenderer>().sprite;
        } else if (level == 1) {
            GameObject.Find("Sky").GetComponent<SpriteRenderer>().sprite = GameObject.Find("paris").GetComponent<SpriteRenderer>().sprite;
        } else if (level == 2) {
            GameObject.Find("Sky").GetComponent<SpriteRenderer>().sprite = GameObject.Find("madrid").GetComponent<SpriteRenderer>().sprite;
        }

	}

    // Update is called once per frame
    void Update()
    {
		if(SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2){
			return;
		}
			DidPlayerLoseLife();
    }

    void DidPlayerLoseLife(){
        if (prevLifeCounter != lives) // life lost
        {
            Debug.Log("lost life");
			healthBar.SetHealth(MAXLIVES - lives);

        }
        prevLifeCounter = lives; // update

        if (lives == MAXLIVES){  // check if lost game
            Debug.Log("lost game");
			SceneManager.LoadScene(2);
        }
    }

}
