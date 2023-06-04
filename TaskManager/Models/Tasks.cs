using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Models
{
    public class Tasks
    {
        [Id]
        public string Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Status { get; set; }
        public Array AssignedTo { get; set; }
    }
}