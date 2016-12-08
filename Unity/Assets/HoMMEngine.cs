using UnityEngine;
using System.Collections;
using HoMM.HommEngine;
using HoMM;
using CVARC.V2;

public class HoMMEngine : MonoBehaviour {
    HoMM.HommEngine.HommEngine engine;
    // Use this for initialization
    void Start () {
        engine = new HommEngine(new CommonEngine());

        var map = new Map("Assets/goodMap.txt");
        var round = new Round("Assets/goodMap.txt", new string[] { "1", "2" });
        MapUnityConnecter.Connect(round, engine);
        engine.Move("1", Directions.Up);  
        //var b = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
