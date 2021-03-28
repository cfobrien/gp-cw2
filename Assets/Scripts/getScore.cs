using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour
{
    public float StartTime;
    public float timeAlive;
    public Text score;
    
    // Start is called before the first frame update
    void Start()
    {
        StartTime = 0.0f;
        timeAlive = 0.0f;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.lives < GameManager.Instance.MAXLIVES)
        {
            timeAlive += Time.deltaTime;
            score.text = timeAlive.ToString("F2") + "s";
        }
    }
}
