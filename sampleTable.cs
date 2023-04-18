namespace testAppSQLite
{
    class surveys
    {
        //Question 1
        public string getLayer1 { get; set; }
        //Question 2 here, if you want to put

        //Add +1 Overload (mas madali)
        public surveys()
        {

        }
        public surveys(string ans1)
        {
            //
            getLayer1 = ans1; 
        }

        //Get Result but every call out ToString(), it gets additional info
        public override string ToString() {
            return "Result is: " + getLayer1;
        }
    }
}

//This Reference will be like on Survey Table on SQLite, table sensitive
//Added another .cs but namespace must be same all the time
//Only one Question