using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Effects.Fog
{
    public class FogHandler : ModWorld
    {
		ScreenFog encounterFog = new ScreenFog(false);
		
        public override void PostDrawTiles()
        {
            encounterFog.Update(mod.GetTexture("Effects/Fog/Fog"));
            encounterFog.Draw(mod.GetTexture("Effects/Fog/Fog"), false, Color.White, true);
        }
    }
}