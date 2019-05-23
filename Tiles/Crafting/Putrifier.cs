using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Tiles.Crafting
{
    public class Putrifier : ModTile
    {
        public int soundCD = 0;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Putrifier");
            AddMapEntry(new Color(39, 78, 96), name);

            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.addTile(Type);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("Putrifier"));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Item iron = null;
            Item fragment = null;
            for (int k = 0; k < Main.item.Length; k++)
            {
                Item item = Main.item[k];
                if (Vector2.Distance(item.Center, new Vector2(i * 16, j * 16)) <= 50 && item.active)
                {
                    if (item.type == mod.ItemType("SunFragment"))
                    {
                        fragment = item;
                        ChewDust(item);
                    }
                    if (item.type == ItemID.IronOre)
                    {
                        iron = item;
                        ChewDust(item);
                    }
                }
            }
            soundCD--;
            if (iron != null && fragment != null)
            {
                if (iron.active && fragment.active)
                {
                    if (iron.stack > 0 && fragment.stack > 0)
                    {
                        if (iron.stack > 1)
                        {
                            iron.stack--;
                        }
                        else
                        {
                            iron.active = false;
                        }
                        if (fragment.stack > 1)
                        {
                            fragment.stack--;
                        }
                        else
                        {
                            fragment.active = false;
                        }

                        Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("PutridOre"));
                        if (soundCD <= 0)
                        {
                            Main.PlaySound(SoundID.NPCDeath13, new Vector2(i * 16, j * 16));
                            soundCD = 20;
                        }
                    }
                }
            }
            /*
            for (int k = 0; k < Main.item.Length; k++)
            {
                Item item = Main.item[k];
                if (Vector2.Distance(item.Center, new Vector2(i * 16, j * 16)) <= 50)
                {
                    if (item.type == mod.ItemType("SunFragment"))
                    {
                        fragmentWho = item.whoAmI;
                        fragmentStack = Main.item[fragmentWho].stack;

                        if (Main.rand.Next(20) == 0)
                        {
                            int dust = Dust.NewDust(item.Center, item.width, item.height, 2, 0f, 0f, 100, new Color(14, 122, 82));
                        }
                    }
                    if (item.type == ItemID.IronOre)
                    {
                        ironWho = item.whoAmI;
                        ironStack = Main.item[ironWho].stack;

                        if (Main.rand.Next(20) == 0)
                        {
                            int dust = Dust.NewDust(item.Center, item.width, item.height, 2, 0f, 0f, 100, new Color(14, 122, 82));
                        }
                    }

                }
            }
            if (fragmentWho > -1)
            {
                Main.NewText(Main.item[fragmentWho].Name);
            }
            if ((fragmentStack > 0 && Main.item[fragmentWho].type == mod.ItemType("SunFragment")) && (ironStack > 0 && Main.item[ironWho].type == ItemID.IronOre))
            {
                if (Main.item[ironWho].type == ItemID.IronOre)
                {
                    if (Main.item[ironWho].stack > 1)
                    {
                        Main.item[ironWho].stack--;
                        ironStack--;
                    }
                    else
                    {
                        Main.item[ironWho].active = false;
                    }
                }
                if (Main.item[fragmentWho].type == mod.ItemType("SunFragment"))
                {
                    if (Main.item[fragmentWho].stack > 1)
                    {
                        Main.item[fragmentWho].stack--;
                        fragmentStack--;
                    }
                    else
                    {
                        Main.item[fragmentWho].active = false;
                    }
                }
                Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("PutridOre"));
                Main.PlaySound(SoundID.NPCDeath13, new Vector2(i * 16, j * 16));
            }*/
        }
        private void ChewDust(Item item)
        {
            if (Main.rand.Next(20) == 0)
            {
                int dust = Dust.NewDust(item.Center, item.width, item.height, 2, 0f, 0f, 100, new Color(14, 122, 82));
            }
        }
    }
}