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
        var devices = new List<Device> {
       new Device("Light bulb", "RGB light buleqwedqwdbqwdbqwbdkqhwgdkqhwkdhkqwjhdkjqwhkjdhb"),
       new Device("Air conditioner"),
       new Device("Light bulb", "RGB light bulb"),
       new Device("Air conditioner"),
       new Device("Light bulb", "RGB light bulb"),
       new Device("Air conditioner"),
       new Device("Light bulb", "RGB light bulb"),
       new Device("Air conditioner")
     };
     
        //var devices = _dbContext.Devices.ToList();

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

  [HttpPost]
  public async Task<ActionResult<Device>> addDevice(Device device) {
    device.Id = Guid.NewGuid();
    this._dbContext.Devices.Add(device);
    await _dbContext.SaveChangesAsync();
    return Ok(device);
  }

  [HttpPut]
  [Route("{deviceId}")]
  public async Task<ActionResult<Device>> updateDevice(string deviceId, Device device) {
    var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
    if (!isValidGuid) {
      return BadRequest("Invalid id");
    }

    var dbDevice = await this._dbContext.Devices.FindAsync(parsedId);
    if (dbDevice == null) {
      return NotFound("Device not found");
    }

    dbDevice.Title = device.Title;
    dbDevice.Description = device.Description;
    await this._dbContext.SaveChangesAsync();

    return Ok(device);
  }

  [HttpDelete]
  [Route("{deviceId}")]
  public async Task<ActionResult> deleteDevice(string deviceId) {
    var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
    if (!isValidGuid) {
      return BadRequest("Invalid id");
    }

    var dbDevice = await this._dbContext.Devices.FindAsync(parsedId);
    if (dbDevice == null) {
      return NotFound("Device not found");
    }

    this._dbContext.Devices.Remove(dbDevice);
    await this._dbContext.SaveChangesAsync();
    return Ok(true);
  }
}