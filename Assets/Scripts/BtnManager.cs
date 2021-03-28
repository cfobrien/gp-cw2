using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
	public void SendLevel(int lvl){
		GameManager.Instance.level = lvl;
		Debug.Log(GameManager.Instance.level);
		SceneManager.LoadScene(0);
	}
}
