namespace ClassMapper.Entities
{
    public class Bar
    {
        public string Doo { get; set; }
        public string Jar;
        public int Car { get; set; }
        public int Loo;
        public int Shoo;

        public override string ToString()
        {
            return "Doo: " + Doo + "\nJar: " + Jar + "\nCar: " + Car + "\nLoo: " + Loo + "\nShoo: " + Shoo;
        }
    }
}
