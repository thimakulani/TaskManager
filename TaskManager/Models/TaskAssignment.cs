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
    public class TaskAssignment
    {
        [Id]
        public string Id { get; set; }
        public string User { get; set; }
    }
}