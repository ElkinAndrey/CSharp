namespace RandomColors.Models
{
    public static class Colors
    {
        public static System.Drawing.Color[,] Array(int x, int y)
        {
            System.Drawing.Color[,] mas = new System.Drawing.Color[x, y];

            Random rnd = new Random();

            for (int i = 0; i < x; i++)
                for(int j = 0; j < y; j++)
                    mas[i, j] = System.Drawing.Color.FromArgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next());

            return mas;
        }

        public static System.Drawing.Color Color()
        {
            Random rnd = new Random();
            System.Drawing.Color color = System.Drawing.Color.FromArgb((byte)rnd.Next(), (byte)rnd.Next(), (byte)rnd.Next());
            return color;           
        }
    }
}
