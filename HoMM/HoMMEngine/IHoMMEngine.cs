using UnityEngine;


namespace HoMM.HoMMEngine {
    public interface IHoMMEngine {
        GameObject CreateObject(string id, MapObject obj);
        void DeleteObject(string id);
    }
}
