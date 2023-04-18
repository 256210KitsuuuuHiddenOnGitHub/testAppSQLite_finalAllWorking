using Android.App;
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
            //Get RG and Rd to get Value
            RadioGroup rg = FindViewById<RadioGroup>(Resource.Id.questionOne);
            RadioButton rd = FindViewById<RadioButton>(rg.CheckedRadioButtonId);

            //Store Answer
            string ans = rd.Text.ToString();

            //Delegate Command, auto increment(Create Database)
            submit.Click += delegate
            {
                //Create Connection
                db = new SQLiteConnection(pathCombo);

                //Table Ready Here
                db.CreateTable<surveys>();

                //Insert Surveys Here
                surveys ss = new surveys(ans);

                //Insert to Database
                db.Insert(ss);
            };

            //Retrieve Data from Database
            retrieve.Click += delegate
            {
                //Create Connection
                db = new SQLiteConnection(pathCombo);

                //Connect to Table
                var resultHere = db.Table<surveys>();

                //Retrieve Info
                txRes.Text = resultHere.ToString();
            };
            

        }
    }
}