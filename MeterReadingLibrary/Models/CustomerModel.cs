using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingLibrary.Models;

public class CustomerModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int MeterReading { get; set; }

    public DateTimeOffset LastMeterReadUpload { get; set; }
    
}
