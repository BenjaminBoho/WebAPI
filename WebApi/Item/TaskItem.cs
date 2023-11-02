using System;
namespace WebApi

{
	public class TaskItem
	{
        public string? taskId { get; set; }
        public string? tasks { get; set; }
        public bool completed { get; set; }
        public string? listId { get; set; }
    }
}