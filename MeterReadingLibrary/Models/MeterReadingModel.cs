namespace EnsekTechincalTest.Models
{
    public class MeterReadingModel
    {
        public int AccountId { get; set; }

        public DateTimeOffset MeterReadDateTime { get; set; }

        public int MeterReadValue { get; set; }
    }
}
