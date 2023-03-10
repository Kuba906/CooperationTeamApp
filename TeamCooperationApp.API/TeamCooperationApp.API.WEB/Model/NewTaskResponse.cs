namespace TeamCooperationApp.API.WEB.Model
{
    public class NewTaskResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public int Estimation { get; set; }
        public string Status { get; set; }
        public int BoardId { get; set; }
    }
}
