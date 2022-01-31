using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wet_api.Dtos;
using System.Security.Claims;

namespace wet_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly DataContext _dbContext;
    public DevicesController(DataContext dataContext)
    {
        this._dbContext = dataContext;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Device>>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var devices = _dbContext.Devices.Where(d => d.UserId == Int32.Parse(userId)).ToList();

        return Ok(devices);
    }

    [Authorize]
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (device.UserId != Int32.Parse(userId))
        {
            return Unauthorized();
        }

        return Ok(device);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Device>> addDevice(AddDeviceDto deviceData)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        System.Console.WriteLine(userId);
        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.GetFromJsonAsync<DeviceResponseDto>(new Uri(deviceData.Url));
            System.Console.WriteLine(data);
            var device = new Device(data.Title, data.Description);
            device.Base = data.Base;
            device.Href = data.Href;
            device.Properties = data.Properties;
            device.Actions = data.Actions;
            device.UserId = Int32.Parse(userId);
            this._dbContext.Devices.Add(device);
            await _dbContext.SaveChangesAsync();
            return Ok(device);
        }
    }

    [HttpGet]
    [Route("/test")]
    public async Task<ActionResult> testSelect()
    {
        var dbDevice = await this._dbContext.Persons.FindAsync(2);
        return Ok(dbDevice);
    }

    [Authorize]
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

    [Authorize]
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
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (dbDevice.UserId != Int32.Parse(userId))
        {
            return Unauthorized();
        }

        this._dbContext.Devices.Remove(dbDevice);
        await this._dbContext.SaveChangesAsync();
        return Ok(true);
    }

    [Authorize]
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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (device.UserId != Int32.Parse(userId))
        {
            return Unauthorized();
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.GetFromJsonAsync<Dictionary<string, object>>(new Uri($"{device.Base}/properties"));
            System.Console.WriteLine(data);
            return Ok(new JsonResult(data));
        }
    }

    [Authorize]
    [HttpPatch]
    [Route("{deviceId}/properties/{propertyName}")]
    public async Task<ActionResult> updateProperty(string deviceId, string propertyName, [FromBody]object newVal)
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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (device.UserId != Int32.Parse(userId))
        {
            return Unauthorized();
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.PutAsJsonAsync(new Uri($"{device.Base}/properties/{propertyName}"), new Dictionary<string, object> {{propertyName, newVal}});
            System.Console.WriteLine(data);
            return Ok(data);
        }
    }

    [Authorize]
    [HttpPut]
    [Route("{deviceId}/{command}")]
    public async Task<ActionResult> executeAction(string deviceId, string command, [FromBody] string newVal)
    {
        return Ok();
    }
}