using CVARC.V2;
using UnityEngine;


namespace HoMM.HommEngine {
    public interface IHommEngine : IEngine {
        GameObject CreateObject(string id, MapObject obj);
        void DeleteObject(string id);
    }
}
