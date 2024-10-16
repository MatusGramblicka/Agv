namespace ConsoleApp1.Contracts.Output
{
    public class TransportOrderMidProduct
    {
        public long Time { get; set; }

        public int Agv { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public long MidTime { get; set; }

        public int Takt { get; set; }
    }
}
