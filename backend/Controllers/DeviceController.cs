using Microsoft.AspNetCore.Mvc;
using System;
using wet_api.Dtos;
using System.Collections.Generic;

namespace wet_api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        if (!isValidGuid)
        {
            return BadRequest("Invalid id");
        }

        var device = await _dbContext.Devices.FindAsync(parsedId);
        if (device == null)
        {
            return NotFound("Device not found");
        }

        return Ok(device);
    }

    [HttpPost]
    public async Task<ActionResult<Device>> addDevice(string url)
    {
        // device.Id = Guid.NewGuid();
        // this._dbContext.Devices.Add(device);
        // await _dbContext.SaveChangesAsync();
        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.GetFromJsonAsync<DeviceResponseDto>(new Uri(url));
            System.Console.WriteLine(data);
            var device = new Device(data.Title, data.Description);
            device.Base = data.Base;
            device.Href = data.Href;
            device.Properties = data.Properties;
            device.Actions = data.Actions;
            this._dbContext.Devices.Add(device);
            await _dbContext.SaveChangesAsync();
            return Ok(device);
        }
    }

    [HttpPut]
    [Route("{deviceId}")]
    public async Task<ActionResult<Device>> updateDevice(string deviceId, Device device)
    {
        var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
        if (!isValidGuid)
        {
            return BadRequest("Invalid id");
        }

        var dbDevice = await this._dbContext.Devices.FindAsync(parsedId);
        if (dbDevice == null)
        {
            return NotFound("Device not found");
        }

        dbDevice.Title = device.Title;
        dbDevice.Description = device.Description;
        await this._dbContext.SaveChangesAsync();

        return Ok(device);
    }

    [HttpDelete]
    [Route("{deviceId}")]
    public async Task<ActionResult> deleteDevice(string deviceId)
    {
        var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
        if (!isValidGuid)
        {
            return BadRequest("Invalid id");
        }

        var dbDevice = await this._dbContext.Devices.FindAsync(parsedId);
        if (dbDevice == null)
        {
            return NotFound("Device not found");
        }

        this._dbContext.Devices.Remove(dbDevice);
        await this._dbContext.SaveChangesAsync();
        return Ok(true);
    }

    [HttpGet]
    [Route("{deviceId}/properties")]
    public async Task<ActionResult> GetProperties(string deviceId)
    {
        var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
        if (!isValidGuid)
        {
            return BadRequest("Invalid id");
        }

        var device = await this._dbContext.Devices.FindAsync(parsedId);
        if (device == null)
        {
            return NotFound("Device not found");
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.GetFromJsonAsync<Dictionary<string, object>>(new Uri($"{device.Base}/properties"));
            System.Console.WriteLine(data);
            return Ok(data);
        }
    }


    [HttpPatch]
    [Route("{deviceId}/properties/{propertyName}")]
    public async Task<ActionResult> updateProperty(string deviceId, string propertyName, [FromBody] string newVal)
    {
        var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
        if (!isValidGuid)
        {
            return BadRequest("Invalid id");
        }

        var device = await this._dbContext.Devices.FindAsync(parsedId);
        if (device == null)
        {
            return NotFound("Device not found");
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.PutAsJsonAsync(new Uri($"{device.Base}/properties/{propertyName}"), new
            {
                propertyName = newVal
            });
            System.Console.WriteLine(data);
            return Ok(data);
        }
    }

    [HttpPut]
    [Route("{deviceId}/{command}")]
    public async Task executeAction()
    {

    }
}