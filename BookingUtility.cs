namespace mis_221_pa_5_rowecjessica
{
    public class BookingUtility
    {
        private Listing[] listings;
        private Booking[] bookings;
        private Trainer[] trainers;

        public BookingUtility(Booking[] bookings, Listing[] listings, Trainer[] trainers)
        {
            this.bookings = bookings;
            this.listings = listings;
            this.trainers = trainers;
        }

        public void BookingFile(string path)
        {
            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
            }
        }

        public void BookSession()
        {
            System.Console.WriteLine("Would you like to book a session? Y to book, N to go back to Booking Menu:");
            string input = Console.ReadLine().ToUpper();

            if(input == "Y")
            {
                System.Console.WriteLine("Please enter the session ID of the session you'd like to book:");
                int ID = int.Parse(Console.ReadLine());

                CheckBookingIsOpen(ID);
            }
        }

        public int CheckBookingIsOpen(int ID)
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt"; 
            int foundVal = -1;
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);

                if(ID == int.Parse(temp[0]))
                {
                    foundVal = Listing.GetCount();
                    Listing.IncCount();
                }

                line = inFile.ReadLine();
            }

            if(foundVal != -1)
            {
                if(listings[foundVal].GetSpotsLeft() < 1)
                {
                    System.Console.WriteLine("This session is full, please go back and look for a different session!");
                    ViewAvailableSessions();
                }else
                {
                    FinalizeBooking(ID, listings, trainers);
                }
            }else
            {
                System.Console.WriteLine($"Session ID {ID} was not found, please go back and look for a different session");
                ViewAvailableSessions();
            }

            return ID;
        }



        public void FinalizeBooking(int ID, Listing[] listings, Trainer[] trainers)
        {
            string customerFirstName = "Null";
            string customerLastName = "Null";
            string customerEmail = "Null";
            string trainingDate = "00/00";
            string trainerFirstName = "Null";
            string trainerLastName = "Null";
            string status = "Null";

            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";

            System.Console.WriteLine("Are you a returning customer? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            int customerID = 0;
            int foundVal = -1;

            if(input == "Y")
            {
                System.Console.WriteLine("What is your customer ID?");
                customerID = int.Parse(Console.ReadLine());
                ReadInAllBookings(bookings, path);

                for(int i = 1; i < Booking.GetCount(); i ++)
                {
                    if(bookings[i].GetCustomerID() == customerID)
                    {
                        foundVal = i;
                        customerFirstName = bookings[i].GetCustomerFirstName();
                        customerLastName = bookings[i].GetCustomerLastName();
                        customerEmail = bookings[i].GetCustomerEmail();
                    }
                }

                if(foundVal < 0)
                {
                    System.Console.WriteLine("Cusomter ID not found");
                }

            } else
            {
                ReadInAllBookings(bookings, path);
                int max = bookings[0].GetCustomerID();

                for(int i = 1; i < Booking.GetCount(); i ++)
                {
                    if(bookings[i].GetCustomerID() > max)
                    {
                        max = bookings[i].GetCustomerID();
                    }
                }
                customerID = max + 1;

                System.Console.WriteLine($"Your customer ID is {customerID}");

                System.Console.WriteLine("Enter your first name:");
                customerFirstName = Console.ReadLine();

                System.Console.WriteLine("Enter your last name:");
                customerLastName = Console.ReadLine();

                System.Console.WriteLine("Enter your email:");
                customerEmail = Console.ReadLine();
            }

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            StreamReader inFile = new StreamReader(path);
            Listing.SetCount(0);
            string line = inFile.ReadLine(); 

            while( line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = inFile.ReadLine();
            }

            for(int i = 0; i < Listing.GetCount(); i ++)
            {
                if(ID == listings[i].GetListingID())
                {
                    trainingDate = listings[i].GetListingDate();
                    trainerFirstName = listings[i].GetTrainerFirstName();
                    trainerLastName = listings[i].GetTrainerLastName();

                    if(listings[i].GetSpotsLeft() > 0)
                    {
                        status = "Open";
                    } else
                    {
                        status = "Closed";
                    }
                }
            }
            inFile.Close();



            int trainerID = FindTrainerID(trainerLastName);

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";
            StreamWriter sw = File.AppendText(path);
            
            sw.WriteLine();
            sw.Write($"{customerID}#");
            sw.Write($"{ID}#");
            sw.Write($"{customerFirstName}#");
            sw.Write($"{customerLastName}#");
            sw.Write($"{customerEmail}#");
            sw.Write($"{trainingDate}#");
            sw.Write($"{trainerID}#");
            sw.Write($"{trainerFirstName}#");
            sw.Write($"{trainerLastName}#");
            sw.Write($"{status}#");

            sw.Close();
            
        }

        public void ReadInAllBookings(Booking[] bookings, string path)
        {
            StreamReader inFile = new StreamReader(path);
            Booking.SetCount(0);
            string line = inFile.ReadLine();

            while( line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                Booking.IncCount();
                line = inFile.ReadLine();
            }

            inFile.Close();
        }


        public int FindTrainerID(string trainerLastName)
            {
                string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt";
                StreamReader newFile = new StreamReader(path);
                Trainer.SetCount(0);
                string line = newFile.ReadLine();
                int trainerID = 0;

                while(line != null)
                {
                    string[] temp = line.Split('#');
                    trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                    Trainer.IncCount();
                    line = newFile.ReadLine();
                }

                for(int i = 0; i < Trainer.GetCount(); i ++)
                {
                    if(trainerLastName == trainers[i].GetLastName())
                    {
                        trainerID = trainers[0].GetTrainerID();
                    }
                }

                newFile.Close();

                return trainerID;

            }
























        public void ViewAvailableSessions()
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";

            System.Console.WriteLine("How would you like to view available bookings?");
            System.Console.WriteLine("1 - By trainer");
            System.Console.WriteLine("2 - By day of week");
            System.Console.WriteLine("3 - By date");
            System.Console.WriteLine("4 - View all available");
            string userInput = Console.ReadLine(); 

            if(userInput == "1")
            {
                string searchLastName = ViewAvailableSessionsByTrainer(path);
                ViewFullSessionsByTrainer(path, searchLastName);
                BookSession();
            }

            if(userInput == "2")
            {
                string searchDay = ViewAvailableSessionsByDay(path);
                ViewFullSessionsByDay(path, searchDay);
                BookSession();
            }

            if(userInput == "3")
            {
                string searchDate = ViewAvailableSessionsByDate(path);
                ViewFullSessionsByDate(path, searchDate);
                BookSession();
            }

            if(userInput == "4")
            {
                ViewAllAvailable(path);
                BookSession();
            }


        }



        public string ViewAvailableSessionsByTrainer(string path)
        {
            System.Console.WriteLine("Please enter the last name of the trainer whose availabe sessions you'd like to see:");
            string searchLastName = Console.ReadLine().ToUpper();

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            int foundVal = -1;
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                
                if(temp[2].ToUpper() == searchLastName && int.Parse(temp[10]) > 0)
                {
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                line = inFile.ReadLine();
            }

            return searchLastName;
            inFile.Close();
        }

        public void ViewFullSessionsByTrainer(string path, string searchLastName)
        {
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            int foundVal = -1;
            System.Console.WriteLine("The following sessions by this trainer ARE AT CAPACITY; however, you can check back later to see if there are any cancelations!");

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);

                if(temp[2].ToUpper() == searchLastName && int.Parse(temp[10]) < 1)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                Listing.IncCount();
                line = inFile.ReadLine();
            }

            if(foundVal == -1)
            {
                System.Console.WriteLine($"There are no listings under the trainer last name {searchLastName}");
            }

            inFile.Close();
        }


        public string ViewAvailableSessionsByDay(string path)
        {
            System.Console.WriteLine("Please enter the weekday you would like to see available sessions for (write full day, do not abreviate)");
            string searchDay = Console.ReadLine().ToUpper();

            int foundVal = -1;
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);

                if(temp[3].ToUpper() == searchDay && int.Parse(temp[10]) > 0)
                {
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                line = inFile.ReadLine();
            }

            return searchDay;
            inFile.Close();
        }

        public void ViewFullSessionsByDay(string path, string searchDay)
        {
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            int foundVal = -1;
            System.Console.WriteLine($"The following sessions on {searchDay}'s ARE AT CAPACITY; however, you can check back later to see if there are any cancelations!");


            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);

                if(temp[3].ToUpper() == searchDay && int.Parse(temp[10]) < 1)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                Listing.IncCount();
                line = inFile.ReadLine();
            }

            if(foundVal == -1)
            {
                System.Console.WriteLine($"There are no listings on {searchDay}'s");
            }

            inFile.Close();
        }


         public string ViewAvailableSessionsByDate(string path)
        {
            System.Console.WriteLine("Please enter the date for sessions you'd like to see:");
            string searchDate = Console.ReadLine().ToUpper();

            int foundVal = -1;
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                
                if(temp[4] == searchDate && int.Parse(temp[10]) > 0)
                {
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                line = inFile.ReadLine();
            }

            return searchDate;
            inFile.Close();
        }

        public void ViewFullSessionsByDate(string path, string searchDate)
        {
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            int foundVal = -1;
            System.Console.WriteLine("The following sessions by this trainer ARE AT CAPACITY; however, you can check back later to see if there are any cancelations!");

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);

                if(temp[4] == searchDate && int.Parse(temp[10]) < 1)
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                Listing.IncCount();
                line = inFile.ReadLine();
            }

            if(foundVal == -1)
            {
                System.Console.WriteLine($"There are no listings on {searchDate}");
            }

            inFile.Close();
        }

        public void ViewAllAvailable(string path)
        {
            System.Console.WriteLine("Here are all available sessions:");

            int foundVal = -1;
            Listing.SetCount(0);
            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                
                if(int.Parse(temp[10]) > 0)
                {
                    System.Console.WriteLine($"Session {temp[0]} is being run by {temp[1]} {temp[2]} on {temp[3]} {temp[4]} at {temp[5]}.");
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has a {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                line = inFile.ReadLine();
            }

            inFile.Close();
        }


    }
}