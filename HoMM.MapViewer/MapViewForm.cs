using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;
using HoMM.Generators;

namespace HoMM.MapViewer
{
    public partial class MapViewForm : Form
    {
        int diameter = 30;
        
        public MapViewForm()
        {
            InitializeComponent();
            
            Size = new Size(620, 650);

            DoubleBuffered = true;
            
            var r = new Random();
            
            var gen = HommMapGenerator
                .From(new DiagonalMazeGenerator(r))
                .With(new BfsRoadGenerator(r, TileTerrain.Road)
                    .Over(new VoronoiTerrainGenerator(r, TileTerrain.Nature.ToArray())))
                .With(new EntitiesGenerator(r, 6, p => new Mine(Resource.Ore, p)))
                .With(new EntitiesGenerator(r, 6, p => new Mine(Resource.Crystals, p)))
                .And(new EntitiesGenerator(r, 6, p => new Mine(Resource.Gems, p)));

            Map map = null;
            
            var generateButton = new Button { Text = "Generate!", Location = new Point(150, 0) };

            var mapSizeBox = new ComboBox();

            for (var size = 4; size < 20; ++size)
                mapSizeBox.Items.Add(2 * size);
            
            mapSizeBox.SelectedIndex = 5;

            generateButton.Click += (s, e) =>
            {
                var mapSize = (int)mapSizeBox.SelectedItem;
                map = gen.GenerateMap(mapSize);
                this.Invalidate();
            };

            Controls.Add(mapSizeBox);
            Controls.Add(generateButton);

            Paint += (s, e) => {
                if (map != null)
                    foreach (var tile in map)
                        DrawTile(tile, e.Graphics);
            };
        }

        private void DrawTile(Tile cell, Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var brush = new SolidBrush(GetColor(cell.tileObject, cell.tileTerrain));
            var dy = cell.location.X % 2 * diameter / 2;
            g.FillEllipse(brush, (cell.location.X+1) * diameter, (1 + cell.location.Y) * diameter + dy, diameter, diameter);
        }

        Dictionary<TileTerrain, Color> terrainColor = new Dictionary<TileTerrain, Color>
        {
            { TileTerrain.Arid, Color.LightGoldenrodYellow },
            { TileTerrain.Desert, Color.LightYellow },
            { TileTerrain.Grass, Color.LightGreen },
            { TileTerrain.Marsh, Color.Pink },
            { TileTerrain.Road, Color.LightGray },
            { TileTerrain.Snow, Color.LightBlue }
        };

        Dictionary<Resource, Color> resourceColor = new Dictionary<Resource, Color>
        {
            { Resource.Crystals, Color.Blue },
            { Resource.Ore, Color.Red },
            { Resource.Gems, Color.Magenta },
        };

        private Color GetColor(TileObject obj, TileTerrain terrain)
        {
            if (obj as Impassable != null)
                return Color.DarkSlateGray;

            if (obj as Mine != null)
                return resourceColor[(obj as Mine).Resource];

            if (terrainColor.ContainsKey(terrain))
                return terrainColor[terrain];

            return Color.Pink;
        }
    }
}
