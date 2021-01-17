using ElementsAwoken.NPCs.Town;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.UI
{
	internal class BootWingsUI : UIState
	{
		public static VanillaItemSlotWrapper itemSlot;
        public static bool Visible;

        public override void OnInitialize()
        {  
            itemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.EquipAccessory, 0.85f,"Attached Wings")
            {
                Left = { Pixels = 900 },
                Top = { Pixels = 900 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && item.wingSlot > 0
            };
            Append(itemSlot);
        }
        public override void Update(GameTime gameTime)
        {
            itemSlot.Left.Pixels = Main.screenWidth - 233;
            int mH = (int)((typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)).GetValue(null));
            itemSlot.Top.Pixels = mH + 320;
        }
    }
}
