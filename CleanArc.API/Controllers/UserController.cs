using CleanArc.Application.Services.Users.Command.Add;
using CleanArc.Application.Services.Users.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: User
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("User Index");
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        // GET: User/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await Task.FromResult<IActionResult>(Ok(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var user = await _mediator.Send(request);
            return user != null ? Ok(user) : BadRequest("Failed to create user");
        }


    }
}
