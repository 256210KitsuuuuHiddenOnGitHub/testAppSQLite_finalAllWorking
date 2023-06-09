﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;

namespace testAppSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Ready Database
        SQLiteConnection db;
        //Set Path
        string pathCombo = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "nameOfDatabase.sqlite");

        //Initiation
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //StartHere
            createDatabase();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //Start Point
        public void createDatabase()
        {
            //Backend
            Button submit = FindViewById<Button>(Resource.Id.btnSubmit);
            Button retrieve = FindViewById<Button>(Resource.Id.retAns);
            TextView txRes = FindViewById<TextView>(Resource.Id.dispResult);

            //Delegate Command, auto increment(Create Database)
            //Insert answer Here, outside do answer withot user knowing it
            submit.Click += delegate
            {
                //Get RG and Rd to get Value
                //Question 1
                RadioGroup rg = FindViewById<RadioGroup>(Resource.Id.questionOne);
                RadioButton rd = FindViewById<RadioButton>(rg.CheckedRadioButtonId);

                //Question 2
                RadioGroup rg2 = FindViewById<RadioGroup>(Resource.Id.questionTwo);
                RadioButton rd2 = FindViewById<RadioButton>(rg2.CheckedRadioButtonId);

                //Store Answer
                string ans = rd.Text.ToString();
                string ans2 = rd2.Text.ToString();

                //Create Connection
                db = new SQLiteConnection(pathCombo);

                //Table Ready Here
                db.CreateTable<surveys>();

                //Insert Surveys Here
                surveys ss = new surveys(ans,ans2);

                //Insert to Database
                db.Insert(ss);
            };

            //Retrieve Data from Database
            retrieve.Click += delegate
            {
                //Create Connection
                db = new SQLiteConnection(pathCombo);

                //Connect to Table
                var resultHere = db.Table<surveys>().ToList();

                //Retrieve Info
                for (int loop=0; loop<resultHere.Count;loop++)
                {
                    txRes.Text = resultHere[loop].ToString();
                }

            };
            

        }
    }
}