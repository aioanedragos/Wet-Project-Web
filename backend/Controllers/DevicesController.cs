using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using wet_api.Dtos;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId).Select(x => x.DeviceId).ToListAsync();
        var devices = await _dbContext.Devices.Where(d => devicesUserControls.Contains(d.Id)).ToListAsync();

        return Ok(devices);
    }

    [Authorize]
    [HttpGet]
    [Route("{deviceId}")]
    public async Task<ActionResult<Device>> GetById(string deviceId)
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

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

        // check if user has access to device
        var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == parsedId).FirstOrDefaultAsync();
        if (devicesUserControls == null)
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
            var device = new Device(data.Title, data.Description);
            device.Base = data.Base;
            device.Href = data.Href;
            device.Properties = data.Properties;
            device.Actions = data.Actions;
            this._dbContext.Devices.Add(device);
            this._dbContext.PersonDeviceAccesses.Add(new PersonDeviceAccess
            {
                UserId = Int32.Parse(userId),
                DeviceId = device.Id,
                ControlType = ControlTypes.OWNER
            });
            await _dbContext.SaveChangesAsync();
            return Ok(device);
        }
    }

    // [HttpGet]
    // [Route("/test")]
    // public async Task<ActionResult> testSelect()
    // {
    //     var dbDevice = await this._dbContext.Persons.FindAsync(2);
    //     return Ok(dbDevice);
    // }

    // [Authorize]
    // [HttpPut]
    // [Route("{deviceId}")]
    // public async Task<ActionResult<Device>> updateDevice(string deviceId, Device device)
    // {
    //     var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
    //     if (!isValidGuid)
    //     {
    //         return BadRequest("Invalid id");
    //     }

    //     var dbDevice = await this._dbContext.Devices.FindAsync(parsedId);
    //     if (dbDevice == null)
    //     {
    //         return NotFound("Device not found");
    //     }

    //     dbDevice.Title = device.Title;
    //     dbDevice.Description = device.Description;
    //     await this._dbContext.SaveChangesAsync();

    //     return Ok(device);
    // }

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

        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == parsedId).FirstOrDefaultAsync();
        if (devicesUserControls == null || devicesUserControls.ControlType != ControlTypes.OWNER)
        {
            return Unauthorized();
        }
        
        var deviceUsersControls = await _dbContext.PersonDeviceAccesses.Where(x => x.DeviceId == parsedId).ToListAsync();
        this._dbContext.PersonDeviceAccesses.RemoveRange(deviceUsersControls);

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

        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == parsedId).FirstOrDefaultAsync();
        if (devicesUserControls == null)
        {
            return Unauthorized();
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.GetFromJsonAsync<Dictionary<string, object>>(new Uri($"{device.Base}/properties"));
            System.Console.WriteLine(data);
            return Ok(data);
        }
    }

    [Authorize]
    [HttpPatch]
    [Route("{deviceId}/properties/{propertyName}")]
    public async Task<ActionResult> updateProperty(string deviceId, string propertyName, [FromBody] object newVal)
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

        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == parsedId).FirstOrDefaultAsync();
        if (devicesUserControls == null || (new List<ControlTypes> {ControlTypes.OWNER, ControlTypes.READ_WRITE}).Contains(devicesUserControls.ControlType))
        {
            return Unauthorized();
        }

        using (var httpClient = new HttpClient())
        {
            var data = await httpClient.PutAsJsonAsync(new Uri($"{device.Base}/properties/{propertyName}"), new Dictionary<string, object> { { propertyName, newVal } });
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