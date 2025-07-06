using EnsekTechincalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingLibrary.DataAccess;

public class MeterReadingData : IMeterReadingData
{
    private readonly ISqlDataAccess _sql;

    private MeterReadingUploadResult meterReadingUploadResult;

    public MeterReadingData(ISqlDataAccess sql)
    {
        _sql = sql;
        meterReadingUploadResult = new MeterReadingUploadResult();
    }


    public async Task<MeterReadingUploadResult> UploadMeterRead(IEnumerable<MeterReadingModel> meterReadings)
    {
        List<MeterReadingModel> invalidResults = new List<MeterReadingModel>();
        var query = @"UPDATE customers
                    SET MeterReadValue = RIGHT(REPLICATE('0', 5) + CAST(@MeterReadValue AS VARCHAR(5)), 5),
                        LastMeterReadDateTime = @MeterReadDateTime
                    WHERE @MeterReadDateTime > ISNULL(LastMeterReadDateTime, '1000-01-01')
                      AND @MeterReadValue BETWEEN 0 AND 99999
                      AND AccountId = @AccountId; ";
        try
        {
            foreach (var meterReading in meterReadings)
            {
                var result = await _sql.UploadMeterRead<int, dynamic>(
                    query,
                    new
                    {
                        AccountId = meterReading.AccountId,
                        MeterReadDateTime = meterReading.MeterReadDateTime,
                        MeterReadValue = meterReading.MeterReadValue
                    },
                    "Default");

                if (result == 1)
                {
                    meterReadingUploadResult.successfulUploadCount++;
                }
                else
                {
                    meterReadingUploadResult.RejectedUploadCount++;
                    invalidResults.Add(meterReading);
                }


            }
        }
        catch (Exception ex)
        {
           throw new Exception("An error occured when writing the meter read", ex);
        }
    

        return meterReadingUploadResult;
    }
}
