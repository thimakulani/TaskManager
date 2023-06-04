using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Dialogs
{
    public class AddTaskDialog : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.add_task_fragment, container, false);
            ConnectView(view);
            return view;
        }

        private void ConnectView(View view)
        {

        }
        private async Task AddTaskAsync()
        {
            Tasks tasks = new Tasks()
            {
                EndDate = new DateTimeOffset(),
                Id = "",
                Priority = "",
                StartDate = null,
                Status = "",
                TaskDescription = "",
                TaskName = "",
            };
            await CrossCloudFirestore
                .Current
                .Instance
                .Collection(nameof(Tasks))
                .AddAsync(tasks);


            await CrossCloudFirestore.Current.Instance.RunTransactionAsync((transaction) =>
            {

            });


        }
    }
}