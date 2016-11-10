using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HoMM
{
    public class Round
    {
        public Map map;
        public List<Player> players;
        public int daysPassed;

        public Round(string filename, string[] playerNames)
        {
            map = new Map(filename);
            players = playerNames.Select(name => new Player(name, map)).ToList();
            daysPassed = 0;
        }

        public void UpdateTick(Point[] playerPositions)
        {
            if (playerPositions.Length != players.Count)
                throw new ArgumentException("wrong number of player positions!");
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Location = playerPositions[i];
                var currentTile = map[playerPositions[i].Y, playerPositions[i].X];
                if (currentTile.tileObject == null)
                    continue;
                else
                    currentTile.tileObject.InteractWithPlayer(players[i]);
                //InteractWithObject(players[i], currentTile.tileObject);
            }
        }

        private void InteractWithObject(Player currentPlayer, TileObject obj)
        {
            switch (obj.GetType().Name)
            {
                case "Mine":
                    {
                        var m = (Mine)obj;
                        m.Owner = currentPlayer;
                        break;
                    }
                case "ResourcePile":
                    {
                        var rp = (ResourcePile)obj;
                        currentPlayer.GainResources(rp.resource, rp.quantity);
                        obj = null;
                        break;
                    }
                default:
                    break;
            }
        }

        public void DailyTick()
        {
            foreach (var tile in map)
                if (tile.tileObject is Mine)
                {
                    var m = tile.tileObject as Mine;
                    if (m.Owner != null)
                        m.Owner.GainResources(m.Resource, m.Yield);
                }



            daysPassed++;
            if (daysPassed % 7 == 0)
                WeeklyTick();
        }
        public void WeeklyTick()
        {
            foreach (var tile in map)
                if (tile.tileObject is Dwelling)
                {
                    var d = tile.tileObject as Dwelling;
                    d.AddWeeklyGrowth();
                }
        }
    }
}
