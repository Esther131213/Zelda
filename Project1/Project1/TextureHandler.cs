using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    public class TextureHandler
    {
        public static Texture2D TileMap;
        public static Texture2D Link;
        public static Texture2D Linkness;
        public static Texture2D Key;

        public static void LoadTextures(ContentManager content)
        {
            TileMap = content.Load<Texture2D>("tileMap");
            Link = content.Load<Texture2D>("testLink(1)");
            Linkness = content.Load<Texture2D>("LocknessLink");
            Key = content.Load<Texture2D>("Namnlös");
        }
    }
}
