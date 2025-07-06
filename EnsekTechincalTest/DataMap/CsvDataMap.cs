using CsvHelper.Configuration;
using EnsekTechincalTest.Models;

namespace EnsekTechincalTest.DataMap;

public class CsvDataMap : ClassMap<MeterReadingModel>
{
    public CsvDataMap() {
        Map(m => m.AccountId).Name("AccountId");
        Map(m => m.MeterReadDateTime).Name("MeterReadingDateTime");
        Map(m => m.MeterReadValue).Name("MeterReadValue");

    }
}
