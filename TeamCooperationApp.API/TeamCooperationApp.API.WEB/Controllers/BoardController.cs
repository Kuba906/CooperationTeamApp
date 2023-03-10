using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCooperationApp.API.WEB.DataDB;
using TeamCooperationApp.API.WEB.Model;
using Task = TeamCooperationApp.API.WEB.DataDB.Task;

namespace TeamCooperationApp.API.WEB.Controllers
{
    [ApiController]
    public class BoardController : Controller
    {

        [HttpPost]
        [Route("Board")]
        public IActionResult GetBoardsForUser([FromBody] BoardResponse board)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();
            var user = context.User.FirstOrDefault(x => board.Name == x.Name);
            var data = context.Board.Where(x => x.Owner == user.Name || x.Contributor == user.Name).ToList();

            return Ok(data);

        }

        [HttpPost]
        [Route("Tasks")]
        public IActionResult GetTasksForBoard([FromBody] TaskResponse task)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();
            var data = context.Task.Where(x => x.BoardId == task.Id).ToList();

            return Ok(data);

        }


        [HttpPost]
        [Route("MoveTask")]
        public IActionResult MoveTask([FromBody] MovedTaskResponse task)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();
            var element = context.Task.FirstOrDefault(x => task.Id == x.Id);
            element.Status = task.Status;
            context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("AddTask")]
        public IActionResult AddTask([FromBody] NewTaskResponse task)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();

            Task newTask = new Task
            {
                    Name = task.Name,
                    Description = task.Description,
                    UserName = task.UserName,
                    Estimation = Convert.ToInt32(task.Estimation),
                    Status = task.Status,
                    BoardId = task.BoardId

                };

            context.Task.Add(newTask);

            context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("EditTask")]
        public IActionResult EditTask([FromBody] Task task)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();

            var element = context.Task.FirstOrDefault(x => task.Id == x.Id);

            element.Name = task.Name;
            element.Description = task.Description;
            element.UserName = task.UserName;
            element.Estimation = task.Estimation;
            context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("AddBoard")]
        public IActionResult AddBoard([FromBody] NewBoardResponse board)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();

            Board newTask = new Board
            {
                Name = board.Name,
                Owner = board.Owner,
                Contributor = board.Contributor

            };

            context.Board.Add(newTask);

            context.SaveChangesAsync();

            return Ok();
        }

    }
}
