using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todoserver.Models
{
    public class Tasks
    {
        private int id;
        private string title;
        private string desc;
        private bool markasdone;
        private DateTime createat;
        private string ownerId;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Desc { get => desc; set => desc = value; }
        public bool MarkAsDone { get => markasdone; set => markasdone = value; }
        public DateTime CreateAt { get => createat; set => createat = value; }
        public string OwnerId { get => ownerId; set => ownerId = value; }
    }
}