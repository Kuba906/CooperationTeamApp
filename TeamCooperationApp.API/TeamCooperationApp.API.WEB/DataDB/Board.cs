using Microsoft.AspNetCore.Mvc;

namespace TeamCooperationApp.API.WEB.DataDB
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Owner { get; set; }
        public string? Contributor { get; set; }
    }
}
