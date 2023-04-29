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

        // if transaction file doesnt exist, create it 
        public void BookingFile(string path)
        {
            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
                tf.Close();
            }
            
        }

        // ask user how they would like to view availble bookings
        public void ViewAvailableSessions(Listing[] listings)
        {
            Console.Clear();
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            System.Console.WriteLine("How would you like to view available bookings?");
            System.Console.WriteLine("1 - By trainer");
            System.Console.WriteLine("2 - By day of week");
            System.Console.WriteLine("3 - By date");
            System.Console.WriteLine("4 - View all available");
            System.Console.WriteLine("5 - Main Menu");
            string userInput = Console.ReadLine(); 
            string valid = "No";

            while(valid == "No")
            {
                if( (userInput == "1") || (userInput == "2") || (userInput == "3") || (userInput == "4") || (userInput == "5"))
                {
                    valid = "Yes";
                }else
                {
                    valid = "No";
                    System.Console.WriteLine("Please enter a valid menu option:");
                    userInput = Console.ReadLine();
                }
            }


            if(userInput == "1")
            {
                string searchLastName = ViewAvailableSessionsByTrainer(path, listings);
                BookSession(listings);
            }

            if(userInput == "2")
            {
                string searchDay = ViewAvailableSessionsByDay(path);
                BookSession(listings);
            }

            if(userInput == "3")
            {
                string searchDate = ViewAvailableSessionsByDate(path);
                BookSession(listings);
            }

            if(userInput == "4")
            {
                ViewAllAvailable(path);
                BookSession(listings);
            }

            if(userInput == "5")
            {
                MainMenu();
            }
        }


        // ask user the last name of the trainer they would like to see available sessions for
        public string ViewAvailableSessionsByTrainer(string path, Listing[] listings)
        {
            Console.Clear();
            System.Console.WriteLine("Please enter the last name of the trainer whose availabe sessions you'd like to see:");
            string searchLastName = Console.ReadLine().ToUpper();

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            int foundVal = -1;
            int found = -1;

            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            Listing.SetCount(0);

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = inFile.ReadLine();
            }

            
            while(found != 100)
            {
                for(int i = 0; i < Listing.GetCount(); i ++)
                {
                    if(searchLastName == listings[i].GetTrainerLastName().ToUpper())
                    {
                        found = 10;
                    }
                    
                    if(searchLastName == listings[i].GetTrainerLastName().ToUpper() && (listings[i].GetSpotsLeft()) > 0)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine();
                        System.Console.WriteLine($"Session {listings[i].GetListingID()} is being run by {listings[i].GetTrainerFirstName()} {listings[i].GetTrainerLastName()} on {listings[i].GetListingDay()} {listings[i].GetListingDate()} at {listings[i].GetListingTime()}.");
                        System.Console.WriteLine($"This session has {listings[i].GetSpotsLeft()} out of {listings[i].GetMaxCustomers()} spots left and costs ${listings[i].GetListingCost()} per person; this trainer has {listings[i].GetDiscount()}.");
                        System.Console.WriteLine("");
                        foundVal = i;
                        found = 100;
                    }
               
                }

                if(found == -1)
                {
                    System.Console.WriteLine("Trainer not found, please enter a valid trainer last name");
                    searchLastName = Console.ReadLine().ToUpper();
                }

                if(found == 10)
                {
                    System.Console.WriteLine($"All sessions lead by {searchLastName} are full, please enter a different trainer last name:");
                    searchLastName = Console.ReadLine().ToUpper();
                }
            }

            inFile.Close();
            return searchLastName;
        }


        // prompt user to enter what weekday theyd like to see available sessions for 
        public string ViewAvailableSessionsByDay(string path)
        {
            Console.Clear();
            System.Console.WriteLine("Please enter the weekday you would like to see available sessions for (write full day, do not abreviate)");
            string searchDay = Console.ReadLine().ToUpper();

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            int foundVal = -1;
            int found = -1;

            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            Listing.SetCount(0);

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = inFile.ReadLine();
            }

            
            while(found != 100)
            {
                for(int i = 0; i < Listing.GetCount(); i ++)
                {
                    if(searchDay == listings[i].GetListingDay().ToUpper())
                    {
                        found = 10;
                    }
                    
                    if(searchDay == listings[i].GetListingDay().ToUpper() && (listings[i].GetSpotsLeft()) > 0)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine();
                        System.Console.WriteLine($"Session {listings[i].GetListingID()} is being run by {listings[i].GetTrainerFirstName()} {listings[i].GetTrainerLastName()} on {listings[i].GetListingDay()} {listings[i].GetListingDate()} at {listings[i].GetListingTime()}.");
                        System.Console.WriteLine($"This session has {listings[i].GetSpotsLeft()} out of {listings[i].GetMaxCustomers()} spots left and costs ${listings[i].GetListingCost()} per person; this trainer has {listings[i].GetDiscount()}.");
                        System.Console.WriteLine("");
                        foundVal = i;
                        found = 100;
                    }
            
                }

                if(found == -1)
                {
                    System.Console.WriteLine("Invalid input: Please enter the weekday you would like to see available sessions for (write full day, do not abreviate)");
                    searchDay = Console.ReadLine().ToUpper();
                }

                if(found == 10)
                {
                    System.Console.WriteLine($"All sessions on {searchDay}s are full, please pick a different day:");
                    searchDay = Console.ReadLine().ToUpper();
                }
            }

            inFile.Close();
            return searchDay;

        }


        // ask user what date they would like to see available sessions for 
        public string ViewAvailableSessionsByDate(string path)
        {
            Console.Clear();
            System.Console.WriteLine("Please enter the date for sessions you'd like to see: May-August in MM/DD format");
            string searchDate = Console.ReadLine();


            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            int foundVal = -1;
            int found = -1;

            StreamReader inFile = new StreamReader(path);
            string line = inFile.ReadLine();
            Listing.SetCount(0);

            while(line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = inFile.ReadLine();
            }

            
            while(found != 100)
            {
                for(int i = 0; i < Listing.GetCount(); i ++)
                {
                    if(searchDate == listings[i].GetListingDate())
                    {
                        found = 10;
                    }
                    
                    if(searchDate == listings[i].GetListingDate() && (listings[i].GetSpotsLeft()) > 0)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine();
                        System.Console.WriteLine($"Session {listings[i].GetListingID()} is being run by {listings[i].GetTrainerFirstName()} {listings[i].GetTrainerLastName()} on {listings[i].GetListingDay()} {listings[i].GetListingDate()} at {listings[i].GetListingTime()}.");
                        System.Console.WriteLine($"This session has {listings[i].GetSpotsLeft()} out of {listings[i].GetMaxCustomers()} spots left and costs ${listings[i].GetListingCost()} per person; this trainer has {listings[i].GetDiscount()}.");
                        System.Console.WriteLine("");
                        foundVal = i;
                        found = 100;
                    }
               
                }

                if(found == -1)
                {
                    System.Console.WriteLine("Please enter a valid date, May-August in MM/DD format");
                    searchDate = Console.ReadLine();
                }

                if(found == 10)
                {
                    System.Console.WriteLine($"All sessions on {searchDate} are full, please pick a different date:");
                    searchDate = Console.ReadLine();
                }
            }

            inFile.Close();
            return searchDate;
        }


        // show user all available sessions
        public void ViewAllAvailable(string path)
        {
            Console.Clear();
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
                    System.Console.WriteLine($"This session has {temp[10]} out of {temp[8]} spots left and costs {temp[7]} per person; this trainer has {temp[12]}.");
                    System.Console.WriteLine("");
                    foundVal = Listing.GetCount();
                }

                line = inFile.ReadLine();
            }

            inFile.Close();
        }


        // ask user if they would like to book one of the available sessions
        public void BookSession(Listing[] listings)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Would you like to book a session? Y to book, N to go back to Booking Menu:");
            string input = Console.ReadLine().ToUpper();
            string valid = "No";
            
            while(valid == "No")
            {
                if( (input == "Y") || (input == "N"))
                {
                    valid = "Yes";
                }else
                {
                    valid = "No";
                    System.Console.WriteLine("Please enter Y or N:");
                    input = Console.ReadLine().ToUpper();
                }
            }


            if(input == "Y")
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Please enter the session ID of the session you'd like to book:");
                int ID = int.Parse(Console.ReadLine());

                CheckBookingIsOpen(ID, listings);
            }

            if(input == "N")
            {
                ViewAvailableSessions(listings);
            }

        }


        // when the user enters the listing ID of the session they would like to book, double check the listing is oepn
        public int CheckBookingIsOpen(int ID, Listing[] listings)
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

            inFile.Close();

            if(foundVal != -1)
            {
                if(listings[foundVal].GetSpotsLeft() < 1)
                {
                    System.Console.WriteLine("This session is full, please go back and look for a different session!");
                    ViewAvailableSessions(listings);
                }else
                {
                    FinalizeBooking(ID, listings, trainers);
                }
            }else
            {
                System.Console.WriteLine($"Session ID {ID} was not found, please go back and look for a different session");
                ViewAvailableSessions(listings);
            }


            return ID;
        }


        // if user is a returning customer ask for their customerID, else make a customer ID and have them enter their information
        public void FinalizeBooking(int ID, Listing[] listings, Trainer[] trainers)
        {
            Console.Clear();
            string customerFirstName = "Null";
            string customerLastName = "Null";
            string customerEmail = "Null";
            string trainingDate = "00/00";
            string trainerFirstName = "Null";
            string trainerLastName = "Null";
            string status = "Null";
            int customerID = 0;
            int foundVal = -1;

            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";

            System.Console.WriteLine();
            System.Console.WriteLine("Are you a returning customer? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            string valid = "No";
            
            while(valid == "No")
            {
                if( (input == "Y") || (input == "N"))
                {
                    valid = "Yes";
                }else
                {
                    valid = "No";
                    System.Console.WriteLine("Please enter Y or N:");
                    input = Console.ReadLine().ToUpper();
                }
            }

            if(input == "Y")
            {
                System.Console.WriteLine();
                System.Console.WriteLine("What is your customer ID?");
                customerID = int.Parse(Console.ReadLine());
                ReadInAllBookings(bookings, path);

                foundVal = -1;

                while (foundVal == -1)
                {
                    for(int i = 0; i < Booking.GetCount(); i ++)
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
                        System.Console.WriteLine("Cusomter ID not found, please enter a valid customer ID:");
                        customerID = int.Parse(Console.ReadLine());
                    }
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

            
            StreamReader readIn = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt");
            Listing.SetCount(0);
            string line = readIn.ReadLine(); 

            while( line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = readIn.ReadLine();
            }
            readIn.Close();


            for(int i = 0; i < Listing.GetCount(); i ++)
            {
                if(ID == listings[i].GetListingID())
                {
                    trainingDate = listings[i].GetListingDate();
                    trainerFirstName = listings[i].GetTrainerFirstName();
                    trainerLastName = listings[i].GetTrainerLastName();

                    int spotsTaken = listings[i].GetSpotsTaken() + 1;
                    listings[i].SetSpotsTaken(spotsTaken);

                    int spotsLeft = listings[i].GetSpotsLeft() - 1;
                    listings[i].SetSpotsLeft(spotsLeft);

                    if( spotsLeft == 0)
                    {
                        listings[i].SetAvailability("Closed for booking - session is full");
                    }

                    if(listings[i].GetSpotsLeft() > 0)
                    {
                        status = "Open";
                    } else
                    {
                        status = "Closed";
                    }

                } ///// END FOR LOOP BEFORE
            }

            StreamWriter reWrite = new StreamWriter(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt");
            for(int j = 0; j < Listing.GetCount(); j ++)
            {
                reWrite.Write($"{listings[j].GetListingID()}#");
                reWrite.Write($"{listings[j].GetTrainerFirstName()}#");
                reWrite.Write($"{listings[j].GetTrainerLastName()}#");
                reWrite.Write($"{listings[j].GetListingDay()}#");
                reWrite.Write($"{listings[j].GetListingDate()}#");
                reWrite.Write($"{listings[j].GetListingTime()}#");
                reWrite.Write($"{listings[j].GetRecurring()}#");
                reWrite.Write($"{listings[j].GetListingCost()}#");
                reWrite.Write($"{listings[j].GetMaxCustomers()}#");
                reWrite.Write($"{listings[j].GetSpotsTaken()}#");
                reWrite.Write($"{listings[j].GetSpotsLeft()}#");
                reWrite.Write($"{listings[j].GetAvailability()}#");
                reWrite.Write($"{listings[j].GetDiscount()}#");
                reWrite.WriteLine();
            }
            reWrite.Close();


        



            int trainerID = FindTrainerID(trainerLastName);

            path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";
            StreamWriter sw = File.AppendText(path);
            
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
            sw.WriteLine();

            sw.Close();

            System.Console.WriteLine("Session booked! Press any key to return to Booking Menu:");
            Console.ReadKey();
            ViewAvailableSessions(listings);
            
        }


        // read in transactions file and make booking array
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


        // read in trainers file and find the trainerID that makes the one enterd
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
                        trainerID = trainers[i].GetTrainerID();
                    }
                }

                newFile.Close();

                return trainerID;

            }



        static void MainMenu()
        {
            Console.Clear();
            System.Console.WriteLine("Please select what you would like to do:");
            System.Console.WriteLine("1 - Manage Trainer Data");
            System.Console.WriteLine("2 - Managae Listing Data");
            System.Console.WriteLine("3 - Manage Customer booking data");
            System.Console.WriteLine("4 - Run Reports");
            System.Console.WriteLine("5 - Exit the application");

            string line = Console.ReadLine();
            int userInput = MenuErrorHandle(line);

            if (userInput == 1)
            {
                ManageTrainerData();
            }

            if(userInput == 2)
            {
                ManageListingData();
            } 
            
            if (userInput == 3)
            {
                ManageCustomerBookingData();
            }
                    
            if (userInput == 4)
            {
                RunReports();            
            }

            if (userInput == 5)
            {
                Console.Clear();
            }

        }

        static int MenuErrorHandle(string line)
        {
            string inputCheck = "";
            int userInput = 0;
            int result = 0;

            if ((line == "1") || (line == "2") || (line == "3") || (line == "4") || (line == "5"))
            {
                inputCheck = "yes";
            } else inputCheck = "no";

            bool parseSuccessful = int.TryParse(line, out result);
            while (result == 0 || inputCheck == "no")
            {
                System.Console.WriteLine("Invalid input, please enter a correct option:");
                line = Console.ReadLine();

                if((line == "1") || (line == "2") || (line == "3") || (line == "4") || (line == "5"))
                {
                    inputCheck = "yes";
                } else inputCheck = "no";

                parseSuccessful = int.TryParse(line, out result);
            }
            userInput = result;
            return userInput;
        }

        static void ManageTrainerData()
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt"; 
            Trainer[] trainers = new Trainer[200];
            TrainerUtility trainerUtility = new TrainerUtility(trainers);

            trainerUtility.TrainerFile(path); 
            trainerUtility.NewTrainer(path);
        }

        static void ManageListingData()
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            Listing[] listings = new Listing[200];
            Trainer[] trainers = new Trainer[200];
            ListingUtility listingUtility = new ListingUtility(listings, trainers);

            listingUtility.ListingFile(path);
            listingUtility.NewListing(path, listings, trainers);
        }

        static void ManageCustomerBookingData()
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";
            Booking[] bookings = new Booking[200];
            Listing[] listings = new Listing[200];
            Trainer[] trainers = new Trainer[200];
            BookingUtility bookingUtility = new BookingUtility(bookings, listings, trainers);

            bookingUtility.BookingFile(path);
            bookingUtility.ViewAvailableSessions(listings);
        }

        static void RunReports()
        {
            Booking[] bookings = new Booking[200];
            Listing[] listings = new Listing[200];
            Trainer[] trainers = new Trainer[200];
            ReportUtility reportUtility = new ReportUtility(bookings, listings, trainers);


            reportUtility.ReportMenu(bookings, listings, trainers);
        }

    }
}