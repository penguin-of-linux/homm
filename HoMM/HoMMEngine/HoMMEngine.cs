using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HoMM.HommEngine {
    public class HommEngine : IHommEngine {//setspeed(enum), commonengine, coords, del color
        private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>() {
            {"hex",  Resources.Load<GameObject>("hex")}
        };

        public GameObject CreateObject(string id, MapObject mapObject) {
            GameObject obj = null;
            switch (mapObject) {
                //case MapObject.Hexagon: obj = GameObject.Instantiate<GameObject>(prefabs["hex"]); break;
                case MapObject.Hexagon:
                    obj = GameObject.Instantiate<GameObject>(prefabs["hex"]);
                    obj.transform.Rotate(90, 0 ,0);
                    break;
                case MapObject.Mine: obj = GameObject.CreatePrimitive(PrimitiveType.Cube); break;
                case MapObject.Flag: obj = GameObject.CreatePrimitive(PrimitiveType.Sphere); break;
                case MapObject.Dwelling: obj = GameObject.CreatePrimitive(PrimitiveType.Capsule); break;
                case MapObject.NeutralArmy: obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder); break;
                case MapObject.ResourcesPile: obj = GameObject.CreatePrimitive(PrimitiveType.Sphere); break;
                default: obj = GameObject.CreatePrimitive(PrimitiveType.Cube); break;
            }
            obj.name = id;

            return obj;
        }
        
        public void DeleteObject(string id) {
            GameObject.Destroy(GameObject.Find(id));
        }

        public void SetFlag(string id, Color color) {
            var obj = GameObject.Find(id);
            var flagPosition = new Vector3(obj.transform.position.x,
                                           2,
                                           obj.transform.position.z); // ?

            var flagId = id + " flag";
            var oldFlag = GameObject.Find(flagId);
            if (oldFlag != null)
                DeleteObject(flagId);

            CreateObject(flagId, MapObject.Flag);
            SetPosition(flagId, flagPosition);
            SetColor(flagId, color);
            SetSize(flagId, 0.5f, 0.5f, 0.5f);
        }

        public void SetPosition(string id, float x, float y, float z) {
            SetPosition(id, new Vector3(x, y, z));
        }

        public void SetPosition(string id, Vector3 position) {
            GameObject.Find(id).transform.position = position;
        }

        public void SetColor(string id, Color color) {
            var obj = GameObject.Find(id);
            foreach (var mr in obj.transform.GetComponentsInChildren<MeshRenderer>())
                mr.material.color = color;
        }

        public void SetColor(string id, byte r, byte g, byte b) {
            SetColor(id, new Color32(r, g, b, 1));
        }

        public void SetSize(string id, float x, float y, float z) {
            SetSize(id, new Vector3(x, y, z));
        }

        public void SetSize(string id, Vector3 size) {
            GameObject.Find(id).transform.localScale = size;
        }

        public void SetCamera(float w, float h) {
            var camera = GameObject.Find("Main Camera");
            SetPosition("Main Camera", w / 2, 10, -h / 2);
            camera.transform.Rotate(90, 0, 0);
        }
    }
}
