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
        public IActionResult List()
        {
            var todos = _useCases.List.Execute();
            return Ok(todos);
        }
        
        [HttpPost]
        public IActionResult Create(Todos.Create.Request request)
        {
            var result = _useCases.Create.Execute(request);
            return Ok(result);
        }
    }
}
