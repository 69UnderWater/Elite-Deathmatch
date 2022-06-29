using System;
using System.Collections.Generic;
using System.Text;

namespace Gangwar.Objects
{
    public class AdminClothingObject
    {
        public int MaskDrawable { get; set; }
        public int MaskTexture { get; set; }
        public int TorsoDrawable { get; set; }
        public int TorsoTexture { get; set; }
        public int LegsDrawable { get; set; }
        public int LegsTexture { get; set; }
        public int ShoeDrawable { get; set; }
        public int ShoeTexture { get; set; }
        public int UndershirtDrawable { get; set; }
        public int UndershirtTexture { get; set; }
        public int TopDrawable { get; set; }
        public int TopTexture { get; set; }

        public AdminClothingObject(int MaskDrawable, int MaskTexture, int TorsoDrawable, int TorsoTexture, int LegsDrawable, int LegsTexture, int ShoeDrawable, int ShoeTexture, int UndershirtDrawable, int UndershirtTexture, int TopDrawable, int TopTexture)
        {
            this.MaskDrawable = MaskDrawable;
            this.MaskTexture = MaskTexture;
            this.TorsoDrawable = TorsoDrawable;
            this.TorsoTexture = TorsoTexture;
            this.LegsDrawable = LegsDrawable;
            this.LegsTexture = LegsTexture;
            this.ShoeDrawable = ShoeDrawable;
            this.ShoeTexture = ShoeTexture;
            this.UndershirtDrawable = UndershirtDrawable;
            this.UndershirtTexture = UndershirtTexture;
            this.TopDrawable = TopDrawable;
            this.TopTexture = TopTexture;
        }

        public AdminClothingObject() { }
    }
}
