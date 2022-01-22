using Microsoft.AspNetCore.Mvc;
using System;

namespace wet_api.Controllers;

[ApiController]
[Route("devices")]
public class DeviceController : ControllerBase 
{
  private readonly DataContext _dbContext;
  public DeviceController(DataContext dataContext)
  {
      this._dbContext = dataContext;
  }

  [HttpGet]
  public async Task<ActionResult<List<Device>>> Get()
  {
    // var devices = new List<Device> {
    //   new Device("Light bulb", "RGB light bulb"),
    //   new Device("Air conditioner")
    // };

    var devices = _dbContext.Devices.ToList();

    return Ok(devices);
  }

  [HttpGet]
  [Route("{deviceId}")]
  public async Task<ActionResult<Device>> GetById(string deviceId)
  {
    var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
    if (!isValidGuid) {
      return BadRequest("Invalid id");
    }

    var device = await _dbContext.Devices.FindAsync(parsedId);
    if (device == null) {
      return NotFound("Device not found");
    }

    return Ok(device);
  }
}