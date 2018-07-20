using UnityEngine;
using System.Collections;

public class TilesManager : MonoBehaviour {

	public GameObject  tilesPrefab;
	public GameObject currentTiles;

	// Use this for initialization
	void Start () {
		for(int i=0;i<=10;i++)
		TilesGenarator ();
	
	}
	

	void Update () {
		//TilesGenarator ();
	
	}





	public void TilesGenarator(){
		int n = Random.Range (0, 3);
	//	Debug.Log ("tiles No:"+n);

		  if (n == 0) {
			currentTiles = (GameObject)Instantiate (tilesPrefab, currentTiles.transform.GetChild (0).transform.GetChild (n).position, Quaternion.Euler (0f, (int)currentTiles.transform.rotation.eulerAngles.y+90f, 0f));

			//Set it in stack

		} else if (n == 1) {
			currentTiles = (GameObject)Instantiate (tilesPrefab, currentTiles.transform.GetChild (0).transform.GetChild (n).position, currentTiles.transform.rotation);
			//Set it in stack
		} else if(n==2) {
			currentTiles=(GameObject) Instantiate (tilesPrefab, currentTiles.transform.GetChild (0).transform.GetChild (n).position,Quaternion.Euler(0f,(int)currentTiles.transform.rotation.eulerAngles.y-90f,0f));
			//Set it in stack
		}



	
	
	}



}
