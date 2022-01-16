using Microsoft.AspNetCore.Mvc;
using System;

namespace wet_api.Controllers;

[ApiController]
[Route("devices")]
public class DeviceController : ControllerBase 
{
  public DeviceController()
  {
      
  }

  [HttpGet(Name = "GetDevicesList")]
  public IEnumerable<Device> Get()
  {
    return new List<Device>() {
      new Device() { Id = Guid.NewGuid(), Title = "Light bulb", Description = "RGB light bulb"},
      new Device() { Id = Guid.NewGuid(), Title = "Air conditioner"}
    };
  }

  [HttpGet]
  [Route("{deviceId}")]
  public Device GetById(string deviceId)
  {
    return new Device() { Id = Guid.NewGuid(), Title = "Light bulb", Description = "RGB light bulb" };
  }
}