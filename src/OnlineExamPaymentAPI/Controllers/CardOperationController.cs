using Domain.Entity;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamPaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = nameof(Roles.QuizParticipant))]
    public class CardOperationController: ControllerBase
    {

    }
}
