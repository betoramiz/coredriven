using CoreDriven.Application.UseCases.Todos;
using Todos = CoreDriven.Application.UseCases.Todos;
using Microsoft.AspNetCore.Mvc;

namespace CoreDriven.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class Todo : ControllerBase
    {
        private readonly TodoUseCases _useCases;
        public Todo(TodoUseCases useCases) => _useCases = useCases;


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var todos = await _useCases.List.ExecuteAsync();
            return Ok(todos);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Todos.Create.Request request)
        {
            var result = await _useCases.Create.ExecuteAsync(request);
            return Ok(result);
        }
    }
}
