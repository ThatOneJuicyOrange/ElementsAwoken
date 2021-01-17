using System;
using ElementsAwoken.Items.Placeable.Tiles.Plateau;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Tiles.VolcanicPlateau
{
    public class AcidWeb : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileCut[Type] = true;
            //drop = ModContent.ItemType<ActiveIgneousRockItem>();
            AddMapEntry(new Color(214, 232, 166));
            soundType = -1;
            soundStyle = 0;

        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.PlaySound(SoundID.Grass, i * 16, j * 16);
                //Main.PlaySound(2, i * 16, j * 16, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/AcidHiss")); // god damn my ears
            }
        }
        public override bool Dangersense(int i, int j, Player player)
        {
            return true;
        }
    }
}