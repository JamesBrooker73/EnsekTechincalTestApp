using EnsekTechincalTest.Models;
using EnsekTechincalTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EnsekTechincalTest.Controllers;

[ApiController]
public class MeterReadsController : ControllerBase
{
    private readonly IMeterReadUploadService _meterReadUploadService;

    public MeterReadsController(IMeterReadUploadService meterReadUploadService)
    {
        _meterReadUploadService = meterReadUploadService;
    }
    // POST api/<MeterReadsController>
    [HttpPost]
    [Route("/meter-reading-uploads")]
    public async Task<ActionResult<MeterReadUploadResultModel>> Post([Required] IFormFile csvFile)
    {
        try
        {
            if (Path.GetExtension(csvFile.FileName) != ".csv")
            {
                return BadRequest("File needs to be a csv");
            }
            var result = await _meterReadUploadService.UploadMeterReads(csvFile.OpenReadStream());
            return Ok(result);

        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
}
