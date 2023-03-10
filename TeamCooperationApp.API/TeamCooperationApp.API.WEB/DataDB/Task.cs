using Microsoft.AspNetCore.Mvc;

namespace TeamCooperationApp.API.WEB.DataDB
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public int Estimation { get; set; }
        public string Status { get; set; }
        public int BoardId { get; set; }
    }
}
