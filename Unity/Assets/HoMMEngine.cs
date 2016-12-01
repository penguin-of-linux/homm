using UnityEngine;
using System.Collections;
using HoMM.HoMMEngine;
using HoMM;

public class HoMMEngine : MonoBehaviour {
    HoMM.HoMMEngine.HoMMEngine engine;
    // Use this for initialization
    void Start () {
        engine = new HoMM.HoMMEngine.HoMMEngine();

        var map = new Map("Assets/goodMap.txt");
        //var c = Resources.Load<GameObject>("hex");
        //var a = MapUnityConnecter.Connect(map, engine, new string[] { "1", "2" });
        MapUnityConnecter.Connect(map, engine, new string[] { "1", "2" });
        //var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //obj.GetComponent<MeshRenderer>().material.color = new Color32(0, 0x64, 0, 1);
        var b = 0;
        //var c1 = new Color(0x00, 0x64, 0x00);
        //var c2 = new Color(0xDA, 0xA5, 0x20);
        //var c3 = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
