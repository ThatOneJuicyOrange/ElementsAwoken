using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Tiles.Crafting
{
    public class ScarletMechStationTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.Height = 4;
            TileID.Sets.HasOutlines[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Scarlet Mech Station");
            AddMapEntry(new Color(163, 74, 83), name);

            disableSmartCursor = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ItemType<Items.Placeable.ScarletMechStation>());
        }
        public override bool HasSmartInteract()
        {
            return true;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ItemType<Items.Placeable.ScarletMechStation>();
        }
        public override bool NewRightClick(int i, int j)
        {
            if (UI.MechCraftingUI.Visible)
            {
                UI.MechCraftingUI.Visible = false;
                Main.PlaySound(SoundID.MenuClose);
            }
            else
            {
                UI.MechCraftingUI.Visible = true;
                Main.PlaySound(SoundID.MenuOpen);
            }
            Main.playerInventory = true;
            Main.LocalPlayer.GetModPlayer<MyPlayer>().currentMechStation = new Vector2(i + 1,j + 1);
            return true;
        }
    }
}