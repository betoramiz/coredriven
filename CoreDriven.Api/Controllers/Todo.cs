using CoreDriven.Api;
using CoreDriven.Application.UseCases.Todos;
using CoreDriven.Application.UseCases.Todos.List;
using Todos = CoreDriven.Application.UseCases.Todos;
using Microsoft.AspNetCore.Mvc;

namespace CoreDriven.api.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class Todo : ApiControllerBase
    {
        private readonly TodoUseCases _useCases;
        public Todo(TodoUseCases useCases) => _useCases = useCases;


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var request = new Request() { ItemsPerPage = 1, Page = 1};
            var todos = await _useCases.List.ExecuteAsync(request);
            return Ok(todos);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Todos.Create.Request request)
        {
            var result = await _useCases.Create.ExecuteAsync(request);
            return result.Match(Ok, Problem);
        }
    }
}
