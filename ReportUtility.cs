namespace mis_221_pa_5_rowecjessica
{
    public class ReportUtility
    {
        private Booking[] bookings;
        private Listing[] listings;
        private Trainer[] trainers;

        public ReportUtility(Booking[] bookings, Listing[] listings, Trainer[] trainers)
        {
            this.bookings = bookings;
            this.listings = listings;
            this.trainers = trainers;
        }
        public void ReportMenu(Booking[] bookings, Listing[] listings, Trainer[] trainers)
        {
            Console.Clear();
            System.Console.WriteLine("What report function would you like to do?");
            System.Console.WriteLine("1 - Individual Customer Sessions");
            System.Console.WriteLine("2 - Historical Customer Sessions");
            System.Console.WriteLine("3 - Historical Revenue Report");
            System.Console.WriteLine("4 - Main Menu");

            ReportMenuNav(bookings, listings, trainers);
        }

        public void ReportMenuNav(Booking[] bookings, Listing[] listings, Trainer[] trainers)
        {
            string userInput = Console.ReadLine();
            string valid = "No";

            while(valid == "No")
            {
                if(userInput == "1")
                {
                    IndividualCustomerSesssions();
                    valid = "Yes";
                }

                if(userInput == "2")
                {
                    HistoricalCustomerSessions(bookings);
                    valid = "Yes";
                }

                if(userInput == "3")
                {
                    HistoricalRevenueReport();
                    valid = "Yes";
                }

                if(userInput == "4")
                {
                    MainMenu();
                    valid = "Yes";
                }

                if(valid == "No")
                {
                    System.Console.WriteLine("Please enter a valid menu option:");
                    userInput = Console.ReadLine();
                }
            }
        }




        public void IndividualCustomerSesssions()
        {
            Console.Clear();
            System.Console.WriteLine("What is the email address of the customer you would like to see a report for?");
            string email = Console.ReadLine();
            email = EmailCheck(email);
            string customerName = GetCustomerName(email);
            DisplayReport(email, customerName);
            SaveReport(email, customerName);
        }

        public string EmailCheck(string email)
        {
            int foundVal = -1;

            while(foundVal == -1)
            {
                StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
                string line = sr.ReadLine();

                while(line != null)
                {
                    string[] temp = line.Split('#');
                    if(temp[4] == email)
                    {
                        foundVal = 10;
                    }
                    line = sr.ReadLine();
                }

                if(foundVal == -1)
                {
                    System.Console.WriteLine("There is no customer on file with that email address, please enter a valid customer email:");
                    email = Console.ReadLine();
                }
                sr.Close();
            }
            return email;
        }

        public string GetCustomerName(string email)
        {
            string customerName= "Null";
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = sr.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');
                if(temp[4] == email)
                {
                    customerName = $"{temp[2]} {temp [3]}";
                }
                line = sr.ReadLine();
            }

            sr.Close();
            return customerName;
        }

        public void DisplayReport(string email, string customerName)
        {
            Console.Clear();
            System.Console.WriteLine($"Previous training sessions for {customerName}:");
            System.Console.WriteLine();
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = sr.ReadLine();
            
            while(line != null)
            {
                string[] temp = line.Split('#');

                if(temp[4] == email)
                {
                    System.Console.WriteLine($"Listing ID: {temp[1]}, Date: {temp[5]}, Trainer: #{temp[6]} {temp[7]} {temp[8]}");
                }
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public void SaveReport(string email, string customerName)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Would you like to save this customers report as a file? Y for yes, N for no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Would you like to save this data to an existing file? Y for yes N for no:");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveToExisting(email, customerName);
                }

                if(input == "N")
                {
                    SaveToNew(email, customerName);
                }
            }else
            {
                MainMenu();
            }
        }

        public void SaveToExisting(string email, string customerName)
        {
            Console.Clear();
            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter nf = File.AppendText(path);
            nf.WriteLine($"Previous training sessions for {customerName}:");
            nf.Close();

            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');

                if(temp[4] == email)
                {
                    StreamWriter report = File.AppendText(path);
                    report.WriteLine($"Listing ID: {temp[1]}, Date: {temp[5]}, Trainer: #{temp[6]} {temp[7]} {temp[8]}");
                    report.Close();
                }
                line = inFile.ReadLine();
            }
            inFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void SaveToNew(string email, string customerName)
        {
            Console.Clear();
            System.Console.WriteLine("What would you like to name the NEW file?");
            string fileName = Console.ReadLine();
            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();
            
            StreamWriter nf = new StreamWriter(fileName);
            nf.WriteLine($"Previous training sessions for {customerName}:");
            nf.Close();
            
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = sr.ReadLine();

            while(line != null)
            {
                string[] temp = line.Split('#');

                if(temp[4] == email)
                {
                    StreamWriter writeReport = File.AppendText(fileName);
                    writeReport.WriteLine($"Listing ID: {temp[1]}, Date: {temp[5]}, Trainer: #{temp[6]} {temp[7]} {temp[8]}");
                    writeReport.Close();
                }
                line = sr.ReadLine();
            }

            sr.Close();
            Console.Clear();
            System.Console.WriteLine("Report saved to new file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void HistoricalCustomerSessions(Booking[] bookings)
        {
            ReadInBookings();
            GetCustomerCount(bookings);
        }

        
        public void ReadInBookings()
        {
            Booking.SetCount(0);
            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();
            while(line != null)
            {       
                string[] temp = line.Split('#');
                string trainingDate = temp[5];
                string[] date = trainingDate.Split("/");
                int[] months = new int[2];
                string month = date[0];
                months[0] = int.Parse(month.ToCharArray()); 
                System.Console.WriteLine($"MONTH: {months[0]}");

                int[] days = new int[2];
                string day = date[1];
                days[0] = int.Parse(day.ToCharArray());
                System.Console.WriteLine($"DAY: {days[0]}");

                string fullDate = $"{months[0]}{days[0]}";
                int dateNum = int.Parse(fullDate);
                temp[5] = fullDate;

                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                line = inFile.ReadLine();
                Booking.IncCount();
            }
            inFile.Close();
        }

        public void GetCustomerCount(Booking[] bookings)
        {
            int[] customers = new int[200];
            customers[0] = int.Parse(bookings[0].GetTrainingDate());

            for(int i = 1; i < Booking.GetCount(); i ++)
            {
                StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
                string line = inFile.ReadLine();
                 
                while(line != null)
                {       
                    string[] temp = line.Split('#');
                    if(i == int.Parse(temp[0]))
                    {
                        for(int j = 1; j < Booking.GetCount(); j ++)
                        {
                            if(int.Parse(bookings[j].GetTrainingDate()) < customers[i])
                            {
                                customers[j] = customers[i];
                                customers[i] = int.Parse(bookings[j].GetTrainingDate());
                            }
                        }
                        


                        // System.Console.WriteLine($"Customer #{temp[0]} {temp[2]} {temp[3]}: Session #{temp[1]}, on {temp[5]}, lead by trainer #{temp[6]} {temp[7]} {temp[8]}");
                    }
                    line = inFile.ReadLine();
                }
                inFile.Close();
            }

            for(int n = 0; n < Booking.GetCount(); n ++)
            {
                System.Console.WriteLine("PRINT");
                System.Console.WriteLine($"{customers[n]}");
            }
        
        }






        public void HistoricalRevenueReport()
        {
            System.Console.WriteLine("HRR");
        }






        public void MenuNav()
        {
            System.Console.WriteLine("1 - Go to Report Menu");
            System.Console.WriteLine("2 - Go to Main Menu");
            string input = Console.ReadLine();
            string valid = "No";

            while(valid == "No")
            {
                if(input == "1")
                {
                    ReportMenu(bookings, listings, trainers);
                    valid = "Yes";
                }else
                {
                    if(input == "2")
                    {
                        MainMenu();
                        valid = "Yes";
                    }else
                    {
                        System.Console.WriteLine("Please enter a valid menu option:");
                        input = Console.ReadLine();
                    }
                }
            }
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

        public string CheckYN(string input)
        {
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
            return input;
        }

    }
}