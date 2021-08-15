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

           // UNCOMMENT METODS BELOW TO TEST

            //Read
            var capturedUser = db.GetUser(1007);

            //Create
             //var AffectedRow = db.InsertUser("kamkam", "kamkam@yaooo.com");
             //var affectedRow = db.InsertUserDynamically(capturedUser);

            //Update
             var UpdatedRow = db.UpdateUser(capturedUser,"kamssy@yaooo.com", "kamsyy");
             //var UpdatedJustEmailinRow = db.UpdateUser(capturedUser, "kamssy@yaooo.com");
            //var UpdatedJustNameinRow = db.UpdateUser(capturedUser, "kamsyy");

            //Delete
            //NOTE TO TESTER :
            //Please wen tryin dis delete metod , make sure captured user has an Id within the range 1007 - 1047 
            //db.DeleteUser(capturedUser);

            //Transaction
            //NOTE TO TESTER :
            //This metod will delete any User passed to it
            //Please wen tryin dis Transaction metod , make sure captured user has an Id within the range 1007 - 1047 
           // db.CapturePotentialScammer(false, capturedUser);           

            Console.ReadLine();

        }
    }
}
