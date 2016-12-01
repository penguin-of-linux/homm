using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace HoMM.HoMMEngine {
    public static class MapUnityConnecter {
        private static HoMMEngine engine;
        private const float hexHeight = 1; // !
        private static Dictionary<MapObject, int> objects = new Dictionary<MapObject, int>() {
            {MapObject.Mine, 0 },
            {MapObject.Dwelling, 0 },
            {MapObject.ResourcesPile, 0 },
            {MapObject.NeutralArmy, 0 }
        };
        private static Dictionary<string, Color> playersColors = new Dictionary<string, Color>();
        public static void Connect(Map map, HoMMEngine e, string[] players) {
            engine = e;

            playersColors[players[0]] = Color.red;
            playersColors[players[1]] = Color.blue;
            playersColors[""] = Color.gray;

            engine.SetCamera(map.Width, map.Height);

            for (int x = 0; x < map.Width; x++) {
                for (int y = 0; y < map.Height; y++) {
                    CreateHexagon(map[y, x]);
                    CreateTileObject(map[y, x].tileObject);
                }
            }
        }

        private static void CreateHexagon(Tile tile) {
            var x = tile.location.X;
            var y = tile.location.Y;
            var hexId = string.Format("Tile {0} {1}", x, y);
            engine.CreateObject(hexId, MapObject.Hexagon);

            var coords = ConvertToUnityCoordinates(x, y);
            engine.SetPosition(hexId, coords[1], 0, coords[0]);

            var color = GetHexagonColor(tile.tileTerrain);
            engine.SetColor(hexId, color);
        }

        private static Color GetHexagonColor(TileTerrain terrain) {
            if (terrain == TileTerrain.Grass) return Color.green;
            if (terrain == TileTerrain.Road) return Color.gray;
            if (terrain == TileTerrain.Arid) return new Color32(0xDA, 0xA5, 0x20, 1);
            if (terrain == TileTerrain.Desert) return Color.yellow;
            if (terrain == TileTerrain.Marsh) return new Color32(0x00, 0x64, 0x00, 1);
            if (terrain == TileTerrain.Snow) return Color.white;
            return Color.black;
        }

        private static void CreateTileObject(TileObject tileObject) {
            //return tileObject;
            if (tileObject != null) {
                if (tileObject is Mine) {
                    tileObject.unityID = $"Mine {objects[MapObject.Mine]++}";
                    engine.CreateObject(tileObject.unityID, MapObject.Mine);
                }
                if (tileObject is Dwelling) {
                    tileObject.unityID = $"Dwelling {objects[MapObject.Dwelling]++}";
                    engine.CreateObject(tileObject.unityID, MapObject.Dwelling);
                }
                if (tileObject is ResourcePile) {
                    tileObject.unityID = $"Resources pile {objects[MapObject.ResourcesPile]++}";
                    engine.CreateObject(tileObject.unityID, MapObject.ResourcesPile);
                }
                if (tileObject is NeutralArmy) {
                    tileObject.unityID = $"Neutral army {objects[MapObject.NeutralArmy]++}";
                    engine.CreateObject(tileObject.unityID, MapObject.NeutralArmy);
                }

                engine.SetSize(tileObject.unityID, 0.5f, 0.5f, 0.5f);


                var coords = ConvertToUnityCoordinates(tileObject.location.X, tileObject.location.Y);
                engine.SetPosition(tileObject.unityID, coords[1], 0, coords[0]);

                if (tileObject is CapturableObject) {
                   var owner = (tileObject as CapturableObject).Owner;
                   engine.SetFlag(tileObject.unityID, playersColors[owner == null? "" : owner.Name]);
                }

                ConnectTileObject(tileObject);
            }
        }

        private static float[] ConvertToUnityCoordinates(int x, int y) {
            return new float[] {
                0 - (y * hexHeight + (x % 2 == 0 ? 0 : hexHeight / 2)),
                (3 * hexHeight * x) / (2 * (float)Math.Sqrt(3))
            };
        }

        private static void ConnectTileObject(TileObject tileObject) {
            if (tileObject == null) return;

            tileObject.Remove += DeleteHandler;

            if (tileObject is INotifyPropertyChanged) {
                ((INotifyPropertyChanged)tileObject).PropertyChanged += UpdateHandler;
            }
        }

        private static void UpdateHandler(object sender, PropertyChangedEventArgs e) {
            TileObject obj;
            try {
                obj = (TileObject)sender;
            }
            catch (InvalidCastException) {
                Console.WriteLine("UpdateHandler: wrong sender"); // log
                return;
            }

            if (e.PropertyName == "Owner") {
                var owner = ((CapturableObject)obj).Owner;
                engine.SetFlag(obj.unityID, playersColors[owner == null? "" : owner.Name]);
            }
        }

        private static void DeleteHandler(TileObject obj) {
            engine.DeleteObject(obj.unityID);
        }
    }
}
