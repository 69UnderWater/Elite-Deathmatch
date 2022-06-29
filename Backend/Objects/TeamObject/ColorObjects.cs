namespace Gangwar.Objects.TeamObject
{
    public class ColorObjects
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
        
        public ColorObjects() {}

        public ColorObjects(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}