namespace ClassMapper.Entities
{
    public class Foo
    {
        public string Doo { get; set; }
        public string Jar;
        public int Car { get; set; }
        public int Loo;
        public int Mou { get; set; }

        public override string ToString()
        {
            return "Doo: " + Doo + " Jar: " + Jar + " Car: " + Car + " Loo: " + Loo + " Mou: " + Mou;
        }
    }
}
