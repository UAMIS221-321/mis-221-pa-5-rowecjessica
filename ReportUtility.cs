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


        // ask user what report they'd like to do
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

         
         // nav to the selected report option
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
                    HistoricalRevenueReport(bookings, listings, trainers);
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



        // prompt user for the customer email they'd like to see previous sessions for
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


        // check that the customer email entered exists
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


        // using the customer email, read in transactions text and find the customer name that goes with the customerID
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


        // display the customer report for the individual customer
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


        // ask user if they'd like to save the customers report
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
                ReportMenu(bookings, listings, trainers);
            }
        }


        // prompt user to enter the path of the existing file they want to save the report to 
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


        // ask user what they'd like to name the new file to save the report to
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


        // historival customer sessions
        public void HistoricalCustomerSessions(Booking[] bookings)
        {
            ReadInBookings(bookings);
        }

        
        // read in transactions file and make bookings array
        public void ReadInBookings(Booking[] bookings)
        {
            string customerDates = "";
            string date = "";

            Booking.SetCount(0);
            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();
            
            while(line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                
                date = bookings[Booking.GetCount()].GetTrainingDate();
                string[] dates = date.Split('/'); 
                customerDates = $"{dates[0]}{dates[1]}";
                bookings[Booking.GetCount()].SetTrainingDate(customerDates);

                line = inFile.ReadLine();
                Booking.IncCount();
            }
            inFile.Close();

            
            int max = 0;

            for(int i = 0; i < Booking.GetCount(); i ++) 
            {
                int customerCount = 0;
                for(int s = 501; s < 832; s ++)
                {
                    for(int j = 0; j < Booking.GetCount(); j ++) 
                    {
                        if((bookings[j].GetCustomerID() == i) && (int.Parse(bookings[j].GetTrainingDate()) == s))
                        {
                            string dateFormat = bookings[j].GetTrainingDate();
                            dateFormat.ToCharArray();
                            string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";
                            customerCount ++;
                            System.Console.WriteLine($"Customer: #{bookings[j].GetCustomerID()} {bookings[j].GetCustomerFirstName()} {bookings[j].GetCustomerLastName()}, Session: #{bookings[j].GetSessionID()} on {finalDate}, Trainer: #{bookings[j].GetBookingTrainerID()} {bookings[j].GetBookingTrainerFirstName()} {bookings[j].GetBookingTrainerLastName()} ");
                            max = j;
                            
                        }
                    }
                }
                if(max != 0)
                {
                    System.Console.WriteLine($"Total bookings for {bookings[max].GetCustomerFirstName()} {bookings[max].GetCustomerLastName()}: {customerCount}");
                }
                    max = 0;
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Do you want to save this report to a file? Y - for yes, N - for no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                System.Console.WriteLine("Do you want to save this to an existing file? Y - for yes, N - for no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveHistoricalToExisting();
                }

                if(input == "N")
                {
                    SaveHistoricalToNew();
                }
            }else
            {
                ReportMenu(bookings, listings, trainers);
            }
        }


        // ask user what they'd like to name the new file to save the historical information to 
        public void SaveHistoricalToNew()
        {
            Console.Clear();
            System.Console.WriteLine("What would you like to name the NEW file?");
            string fileName = Console.ReadLine();
            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();
            
            string customerDates = "";
            string date = "";


            Booking.SetCount(0);
            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();
            
            while(line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                
                date = bookings[Booking.GetCount()].GetTrainingDate();
                string[] dates = date.Split('/'); 
                customerDates = $"{dates[0]}{dates[1]}";
                bookings[Booking.GetCount()].SetTrainingDate(customerDates);

                line = inFile.ReadLine();
                Booking.IncCount();
            }
            inFile.Close();

            
            for(int i = 0; i < Booking.GetCount(); i ++) 
            {
                for(int s = 501; s < 832; s ++)
                {
                    for(int j = 0; j < Booking.GetCount(); j ++) 
                    {
                        if((bookings[j].GetCustomerID() == i) && (int.Parse(bookings[j].GetTrainingDate()) == s))
                        {
                            string dateFormat = bookings[j].GetTrainingDate();
                            dateFormat.ToCharArray();
                            string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";
                            StreamWriter report = File.AppendText(fileName);
                            report.WriteLine($"Customer: #{bookings[j].GetCustomerID()} {bookings[j].GetCustomerFirstName()} {bookings[j].GetCustomerLastName()}, Session: #{bookings[j].GetSessionID()} on {finalDate}, Trainer: #{bookings[j].GetBookingTrainerID()} {bookings[j].GetBookingTrainerFirstName()} {bookings[j].GetBookingTrainerLastName()} ");
                            report.Close();
                        }
                    }
                }
            }

           
            Console.Clear();
            System.Console.WriteLine("Report saved to new file!");
            System.Console.WriteLine();
            MenuNav();
        }



        // ask user to enter the path of the existing file they'd like to save the report to 
        public void SaveHistoricalToExisting()
        {
            Console.Clear();
            string customerDates = "";
            string date = "";

            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            Booking.SetCount(0);
            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();
            
            while(line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                
                date = bookings[Booking.GetCount()].GetTrainingDate();
                string[] dates = date.Split('/'); 
                customerDates = $"{dates[0]}{dates[1]}";
                bookings[Booking.GetCount()].SetTrainingDate(customerDates);

                line = inFile.ReadLine();
                Booking.IncCount();
            }
            inFile.Close();

            
            for(int i = 0; i < Booking.GetCount(); i ++) 
            {
                for(int s = 501; s < 832; s ++)
                {
                    for(int j = 0; j < Booking.GetCount(); j ++) 
                    {
                        if((bookings[j].GetCustomerID() == i) && (int.Parse(bookings[j].GetTrainingDate()) == s))
                        {
                            string dateFormat = bookings[j].GetTrainingDate();
                            dateFormat.ToCharArray();
                            string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";
                            StreamWriter report = File.AppendText(path);
                            report.WriteLine($"Customer: #{bookings[j].GetCustomerID()} {bookings[j].GetCustomerFirstName()} {bookings[j].GetCustomerLastName()}, Session: #{bookings[j].GetSessionID()} on {finalDate}, Trainer: #{bookings[j].GetBookingTrainerID()} {bookings[j].GetBookingTrainerFirstName()} {bookings[j].GetBookingTrainerLastName()} ");
                            report.Close();
                        }
                    }
                }
            }

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }




        // ask user how they would like to view historical revenue report: by month, trainer, or all
        public void HistoricalRevenueReport(Booking[] bookings, Listing[] listings, Trainer[] trainers)
        {
            string customerDates = "";
            string date = "";

            Booking.SetCount(0);
            StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = inFile.ReadLine();
            
            while(line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                
                date = bookings[Booking.GetCount()].GetTrainingDate();
                string[] dates = date.Split('/'); 
                customerDates = $"{dates[0]}{dates[1]}";
                bookings[Booking.GetCount()].SetTrainingDate(customerDates);

                line = inFile.ReadLine();
                Booking.IncCount();
            }
            inFile.Close();



            StreamReader readIn = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt");
            Listing.SetCount(0);
            line = readIn.ReadLine();

            while( line != null)
            {
                string[] temp = line.Split('#');
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                Listing.IncCount();
                line = readIn.ReadLine();
            }

            readIn.Close();
            
            Console.Clear();
            System.Console.WriteLine("How would you like to view reports:");
            System.Console.WriteLine("1 - View by month");
            System.Console.WriteLine("2 - View by trainer");
            System.Console.WriteLine("3 - View all");
            System.Console.WriteLine("4 - Return to Report Menu");
            string userInput = Console.ReadLine();
            string valid = "No";

            while(valid == "No")
            {
                if(userInput == "1")
                {
                    ViewByMonth();
                    valid = "Yes";
                }

                if(userInput == "2")
                {
                    string lastName = ViewByTrainer();
                    SaveTrainer(lastName);
                    valid = "yes";
                }

                if(userInput == "3")
                {
                    double mayRev = ViewMayReport();
                    double juneRev = ViewJuneReport();
                    double julyRev = ViewJulyReport();
                    double augRev = ViewAugustReport();

                    double totalRev = mayRev + juneRev + julyRev + augRev;
                    System.Console.WriteLine();
                    System.Console.WriteLine();
                    System.Console.WriteLine($"TOTAL REVENUE FOR SUMMER 2023: ${totalRev}");

                    SaveAllReports();
                    valid = "yes";
                }

                if(userInput == "4")
                {
                    ReportMenu(bookings, listings, trainers);
                    valid = "yes";
                }

                if(valid == "No")
                {
                    System.Console.WriteLine("Please enter a valid menu option:");
                    userInput = Console.ReadLine();
                }
            }

        }


        // ask user which month they want to view reports for 
        public void ViewByMonth()
        {
            Console.Clear();
            System.Console.WriteLine("Please select which month you would like to view:");
            System.Console.WriteLine("1 - May");
            System.Console.WriteLine("2 - June");
            System.Console.WriteLine("3 - July");
            System.Console.WriteLine("4 - August");
            string input = Console.ReadLine();
            string valid = "No";

            while(valid == "No")
            {
                if(input == "1")
                {
                    ViewMayReport();
                    SaveMayReport();
                    valid = "Yes";
                }

                if(input == "2")
                {
                    ViewJuneReport();
                    SaveJuneReport();
                    valid = "Yes";
                }

                if(input == "3")
                {
                    ViewJulyReport();
                    SaveJulyReport();
                    valid = "Yes";
                }

                if(input == "4")
                {
                    ViewAugustReport();
                    SaveAugustReport();
                    valid = "Yes";
                }

                if(valid == "No")
                {
                    System.Console.WriteLine("Please enter a valid menu option");
                    input = Console.ReadLine();
                }
            }
        }


          public double ViewMayReport()
        {
            double discount = 0.0;
            double mayRev = 0.0;

            System.Console.WriteLine("MAY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((500 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 600))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    System.Console.WriteLine();
                    System.Console.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    System.Console.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    mayRev = mayRev + bookingRev;
                    System.Console.WriteLine($"The revenue from this session was: ${bookingRev}");
                    System.Console.WriteLine();
                }
            }
            System.Console.WriteLine($"The total revenue for May 2023 was: ${mayRev}");
            return mayRev;
        }

        public double ViewJuneReport()
        {
            double discount = 0.0;
            double juneRev = 0.0;

            System.Console.WriteLine("JUNE 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((600 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 700))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    System.Console.WriteLine();
                    System.Console.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    System.Console.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    juneRev = juneRev + bookingRev;
                    System.Console.WriteLine($"The revenue from this session was: ${bookingRev}");
                    System.Console.WriteLine();
                }
            }
            System.Console.WriteLine($"The total revenue for June 2023 was: ${juneRev}");
            return juneRev;
        }

        public double ViewJulyReport()
        {
            double discount = 0.0;
            double julyRev = 0.0;

            System.Console.WriteLine("JULY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((700 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 800))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    System.Console.WriteLine();
                    System.Console.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    System.Console.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    julyRev = julyRev + bookingRev;
                    System.Console.WriteLine($"The revenue from this session was: ${bookingRev}");
                    System.Console.WriteLine();
                }
            }
            System.Console.WriteLine($"The total revenue for July 2023 was: ${julyRev}");
            return julyRev;
        }

        public double ViewAugustReport()
        {
            double discount = 0.0;
            double augRev = 0.0;

            System.Console.WriteLine("August 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((800 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 900))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    System.Console.WriteLine();
                    System.Console.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    System.Console.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    augRev = augRev + bookingRev;
                    System.Console.WriteLine($"The revenue from this session was: ${bookingRev}");
                    System.Console.WriteLine();
                }
            }
            System.Console.WriteLine($"The total revenue for August 2023 was: ${augRev}");
            return augRev;
        }


                // ask user if they want to save the May report
        public void SaveMayReport()
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Do you want to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Do you want to save this report to an EXISTING file? Y - yes, N - no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveMayReportToExisting();
                }

                if(input == "N")
                {
                    SaveMayReportToNew();
                }

            }else
            {
                ReportMenu(bookings,listings, trainers);
            }
        }

        public void SaveMayReportToExisting()
        {
            Console.Clear();
            double discount = 0.0;
            double mayRev = 0.0;

            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);


            
            outFile.WriteLine("MAY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((500 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 600))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    mayRev = mayRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for May 2023 was: ${mayRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void SaveMayReportToNew()
        {
            Console.Clear();
            double discount = 0.0;
            double mayRev = 0.0;

            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string fileName = Console.ReadLine();

            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();

            StreamWriter outFile = File.AppendText(fileName);


            
            outFile.WriteLine("MAY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((500 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 600))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    mayRev = mayRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for May 2023 was: ${mayRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


        // ask user if they want to save the june report
        public void SaveJuneReport()
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Do you want to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Do you want to save this report to an EXISTING file? Y - yes, N - no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveJuneReportToExisting();
                }

                if(input == "N")
                {
                    SaveJuneReportToNew();
                }

            }else
            {
                ReportMenu(bookings,listings, trainers);
            }
        }

        public void SaveJuneReportToExisting()
        {
            Console.Clear();
            double discount = 0.0;
            double juneRev = 0.0;

            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);


            
            outFile.WriteLine("JUNE 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((600 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 700))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    juneRev = juneRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for June 2023 was: ${juneRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void SaveJuneReportToNew()
        {
            Console.Clear();
            double discount = 0.0;
            double juneRev = 0.0;

            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string fileName = Console.ReadLine();

            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();

            StreamWriter outFile = File.AppendText(fileName);


            
            outFile.WriteLine("JUNE 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((600 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 700))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    juneRev = juneRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for June 2023 was: ${juneRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


                // ask user if they want to save the july report
        public void SaveJulyReport()
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Do you want to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Do you want to save this report to an EXISTING file? Y - yes, N - no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveJulyReportToExisting();
                }

                if(input == "N")
                {
                    SaveJulyReportToNew();
                }

            }else
            {
                ReportMenu(bookings,listings, trainers);
            }
        }

        public void SaveJulyReportToExisting()
        {
            Console.Clear();
            double discount = 0.0;
            double julyRev = 0.0;

            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);


            
            outFile.WriteLine("JULY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((700 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 800))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    julyRev = julyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for July 2023 was: ${julyRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void SaveJulyReportToNew()
        {
            Console.Clear();
            double discount = 0.0;
            double julyRev = 0.0;

            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string fileName = Console.ReadLine();

            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();

            StreamWriter outFile = File.AppendText(fileName);


            
            outFile.WriteLine("JULY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((700 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 800))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    julyRev = julyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for July 2023 was: ${julyRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


 // ask user if they want to save the august report
        public void SaveAugustReport()
        {
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Do you want to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Do you want to save this report to an EXISTING file? Y - yes, N - no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveAugustReportToExisting();
                }

                if(input == "N")
                {
                    SaveAugustReportToNew();
                }

            }else
            {
                ReportMenu(bookings,listings, trainers);
            }
        }

        public void SaveAugustReportToExisting()
        {
            Console.Clear();
            double discount = 0.0;
            double augRev = 0.0;

            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);


            
            outFile.WriteLine("AUGUST 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((800 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 900))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    augRev = augRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for August 2023 was: ${augRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }

        public void SaveAugustReportToNew()
        {
            Console.Clear();
            double discount = 0.0;
            double augRev = 0.0;

            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string fileName = Console.ReadLine();

            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();

            StreamWriter outFile = File.AppendText(fileName);


            
            outFile.WriteLine("AUGUST 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((800 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 900))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    augRev = augRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for August 2023 was: ${augRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }





        // view reports by trainer
        public string ViewByTrainer()
        {
            Console.Clear();
            System.Console.WriteLine("Please enter the trainer last name for the trainer who's reports you'd like to see (capitalize first letter):");
            string lastName = Console.ReadLine();
            double discount = 0.0;
            double monthlyRev = 0.0;
            int checkVal = -1;

            while(checkVal == -1)
            {
                for(int i = 0; i < Booking.GetCount(); i ++)
                {
                    if(bookings[i].GetBookingTrainerLastName() == lastName)
                    {
                        checkVal = 1;
                    }
                }

                if(checkVal == -1)
                {
                    System.Console.WriteLine("Trainer last name not found, please enter a valid trainer last name (capitalize first letter):");
                    lastName = Console.ReadLine();
                }
            }
            

            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if(bookings[i].GetBookingTrainerLastName() == lastName)
                {

                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    System.Console.WriteLine();
                    System.Console.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    System.Console.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    monthlyRev = monthlyRev + bookingRev;
                    System.Console.WriteLine($"The revenue from this session was: ${bookingRev}");
                    System.Console.WriteLine();
                }

            }
            System.Console.WriteLine($"The total revenue for {lastName} was: ${monthlyRev}");
            return lastName;
        }



        // ask user if they'd like to save the trainer report
        public void SaveTrainer(string lastName)
        {
                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine();
            System.Console.WriteLine("Would you like to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Would you like to save this report to an EXISTING file?");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveTrainerReportToExisting(lastName);
                }

                if(input == "N")
                {
                    SaveTrainerReportToNew(lastName);
                }
            }else
            {
                ReportMenu(bookings, listings, trainers);
            }
        }


        // ask user what they'd like to name the new file to save the trainer report to 
        public void SaveTrainerReportToNew(string lastName)
        {
            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string fileName = Console.ReadLine();

            StreamWriter newFile = File.CreateText(fileName);
            newFile.Close();

            StreamWriter outFile = File.AppendText(fileName);
            
            double discount = 0.0;
            double monthlyRev = 0.0;
        
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if(bookings[i].GetBookingTrainerLastName() == lastName)
                {

                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    monthlyRev = monthlyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }

            }
            outFile.WriteLine($"The total revenue for {lastName} was: ${monthlyRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


        // ask user for the path of the existing file they'd like to save the report to 
        public void SaveTrainerReportToExisting(string lastName)
        {
            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);
            
            double discount = 0.0;
            double monthlyRev = 0.0;
            

            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if(bookings[i].GetBookingTrainerLastName() == lastName)
                {

                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    monthlyRev = monthlyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }

            }
            outFile.WriteLine($"The total revenue for {lastName} was: ${monthlyRev}");
            outFile.Close();

            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


        // ask user if they'd like to save all reports
        public void SaveAllReports()
        {
            
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Would you like to save this report? Y - yes, N - no");
            string input = Console.ReadLine().ToUpper();
            input = CheckYN(input);

            if(input == "Y")
            {
                Console.Clear();
                System.Console.WriteLine("Would you like to save this report to an EXISTING file? Y - yes, N - no");
                input = Console.ReadLine().ToUpper();
                input = CheckYN(input);

                if(input == "Y")
                {
                    SaveAllReportsToExisting();
                }

                if(input == "N")
                {
                    SaveAllReportsToNew();
                }
            }else
            {
                ReportMenu(bookings, listings, trainers);
            }
        }


        
        // ask user for the path to the existing fil ethey want to save the report to 
        public void SaveAllReportsToExisting()
        {
            Console.Clear();


            System.Console.WriteLine("Please enter the path to the existing file you would like to add this data to:");
            string path = Console.ReadLine();

            StreamWriter outFile = File.AppendText(path);


            double discount = 0.0;
            double mayRev = 0.0;

            outFile.WriteLine("MAY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((500 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 600))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    mayRev = mayRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for May 2023 was: ${mayRev}");

            discount = 0.0;
            double juneRev = 0.0;

        
            outFile.WriteLine("JUNE 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((600 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 700))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    juneRev = juneRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for June 2023 was: ${juneRev}");



             discount = 0.0;
            double julyRev = 0.0;

            
            outFile.WriteLine("JULY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((700 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 800))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    julyRev = julyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for July 2023 was: ${julyRev}");


            discount = 0.0;
            double augRev = 0.0;

            outFile.WriteLine("AUGUST 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((800 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 900))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    augRev = augRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for August 2023 was: ${augRev}");

            outFile.Close();
            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
        }


        // ask user for the name of the new file they want to save all reports to 
        public void SaveAllReportsToNew()
        {
            Console.Clear();

            System.Console.WriteLine("Enter what would you like to call the NEW file this report will be saved to:");
            string path = Console.ReadLine();

            StreamWriter newFile = File.CreateText(path);
            newFile.Close();

            StreamWriter outFile = File.AppendText(path);


            double discount = 0.0;
            double mayRev = 0.0;

            outFile.WriteLine("MAY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((500 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 600))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    mayRev = mayRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for May 2023 was: ${mayRev}");

            discount = 0.0;
            double juneRev = 0.0;

        
            outFile.WriteLine("JUNE 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((600 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 700))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    juneRev = juneRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for June 2023 was: ${juneRev}");



             discount = 0.0;
            double julyRev = 0.0;

            
            outFile.WriteLine("JULY 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((700 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 800))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    julyRev = julyRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for July 2023 was: ${julyRev}");


            discount = 0.0;
            double augRev = 0.0;

            outFile.WriteLine("AUGUST 2023:");
            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                if((800 < int.Parse(bookings[i].GetTrainingDate())) && (int.Parse(bookings[i].GetTrainingDate()) < 900))
                {
                    string dateFormat = bookings[i].GetTrainingDate();
                    dateFormat.ToCharArray();
                    string finalDate = $"{dateFormat[0]}{dateFormat[1]}/{dateFormat[2]}{dateFormat[3]}";

                    outFile.WriteLine();
                    outFile.WriteLine($"Session #{bookings[i].GetSessionID()} was run by #{bookings[i].GetBookingTrainerID()} {bookings[i].GetBookingTrainerFirstName()} {bookings[i].GetBookingTrainerLastName()} on {finalDate}.");
                    
                    int foundVal = -1;
                    for(int j = 0; j < Listing.GetCount(); j ++)
                    {
                        if(listings[j].GetListingID() == bookings[i].GetSessionID())
                        {
                            foundVal = j;
                        }
                    }
                    
                    outFile.WriteLine($"This session had {listings[foundVal].GetSpotsTaken()} out of {listings[foundVal].GetMaxCustomers()} spots booked at ${listings[foundVal].GetListingCost()} a spot and {listings[foundVal].GetDiscount()}.");
                    
                    if(listings[i].GetDiscount() == "discount offered")
                    {
                        discount = 0.95;
                    }
                    else
                    {
                        discount = 1.0;
                    }
                    double bookingRev = listings[foundVal].GetSpotsTaken() * listings[foundVal].GetListingCost() * discount;
                    augRev = augRev + bookingRev;
                    outFile.WriteLine($"The revenue from this session was: ${bookingRev}");
                    outFile.WriteLine();
                }
            }
            outFile.WriteLine($"The total revenue for August 2023 was: ${augRev}");

            outFile.Close();
            Console.Clear();
            System.Console.WriteLine("Report saved to file!");
            System.Console.WriteLine();
            MenuNav();
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