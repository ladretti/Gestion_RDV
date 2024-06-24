using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public TestController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost("send-reminder-emails")]
    public async Task<IActionResult> SendReminderEmails()
    {
        try
        {
            await _appointmentService.SendReminderEmails();
            return Ok("Reminder emails sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error sending reminder emails: {ex.Message}");
        }
    }
}
