using System;
using BEZAOPayDAL;


namespace BEZAOPay
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new BEZAODAL();

            var users = db.GetAllUsers();

            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}\nName: {user.Name}\nEmail: {user.Email}");
            }
            //Read
            var capturedUser = db.GetUser(4);

            //Create
            // db.InsertUser(20, "kamkam", "kamkam@yaooo.com");
            // db.InsertUserDynamically(capturedUser);

            //Update
            //var UpdatedRow = db.UpdateUser(capturedUser, "kamsyy", "kamssy@yaooo.com");
            //var UpdatedJustEmailinRow = db.UpdateUser(capturedUser, "kamssy@yaooo.com");
            //var UpdatedJustNameinRow = db.UpdateUser(capturedUser, "kamsyy");

            //Delete
            // var DeletedRow = db.DeleteUser(capturedUser);

            //transaction
            db.CapturePotentialScammer(true, capturedUser);

            Console.ReadLine();

        }
    }
}
