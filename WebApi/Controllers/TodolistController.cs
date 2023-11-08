using Microsoft.AspNetCore.Mvc;
using static WebApi.Controllers.TodoAppController;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TodoAppController : ControllerBase
    {
        [HttpGet("list")]
        public ActionResult<IEnumerable<ListItem>> GetList()
        {
            var list = ListService.Get();
            return Ok(list);
        }

        [HttpGet("task")]
        public ActionResult<IEnumerable<TaskItem>> GetTask(string listId)
        {
            var task = TaskService.Get(listId);
            return Ok(task);
        }

        [HttpGet("listid")]
        public ActionResult<ListItem> Get(string id)
        {
            var list = new List<ListItem>();
            ListService.Get(list, id);

            if (list.Count == 0)
            {
                return NotFound();
            }

            return Ok(list[0]);
        }

        [HttpPost("InsertList")]
        public ActionResult<ListItem> PostList(ListItem insertList)
        {
            if (insertList == null)
            {
                return BadRequest();
            }
            ListService.Insert(insertList);

            return Ok();
        }

        [HttpPost("InsertTask")]
        public ActionResult<TaskItem> PostTask(TaskItem insertTask)
        {
            if (insertTask == null)
            {
                return BadRequest();
            }

            TaskService.InsertTask(insertTask);

            return Ok();
        }

        [HttpPut("listUpdate")]
        public IActionResult UpdateList(string id, ListItem updatedlist)
        {
            if (updatedlist == null)
            {
                return NotFound();
            }

            if (id != updatedlist.listId)
            {
                return BadRequest();
            }

            ListService.UpdateList(updatedlist);

            return NoContent();
        }

        [HttpPut("taskUpdate")]
        public IActionResult UpdateTask(string id, TaskItem updatedTask)
        {
            if (updatedTask == null)
            {
                return NotFound();
            }

            if (id != updatedTask.taskId)
            {
                return BadRequest();
            }

            TaskService.UpdateTask(updatedTask);

            return NoContent();
        }

        [HttpDelete("list/{id}")]
        public IActionResult DeleteList(string id)
        {

            ListService.DeleteListRow(id);
            TaskService.DeleteTasksByListId(id);

            return NoContent();
        }

        [HttpDelete("task/{id}")]
        public IActionResult DeleteTask(string id)
        {

            TaskService.DeleteTaskRow(id);

            return NoContent();
        }

        [HttpPut("taskUpdateCompleted/{id}")]
        public IActionResult UpdateTaskCompleted(string id, bool completed)
        {
            var task = TaskService.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }

            task.completed = completed;

            TaskService.UpdateTask(task);

            return NoContent();
        }
    }
}
