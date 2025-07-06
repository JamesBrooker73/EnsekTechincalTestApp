using MeterReadingLibrary.Models;
using CsvHelper.Configuration;

namespace MeterReadingLibrary.DataMap;

public class CustomerDataMap : ClassMap<CustomerModel>
{
    public CustomerDataMap() 
    {
        Map(m => m.Id).Name("AccountId");
        Map(m => m.FirstName).Name("FirstName");
        Map(m => m.LastName).Name("LastName");

    }
}
