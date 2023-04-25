namespace mis_221_pa_5_rowecjessica
{
    public class ListingUtility
    {
        private Listing[] listings;
        private Trainer[] trainers;


        public ListingUtility(Listing[] listings, Trainer[] trainers)
        {
            this.listings = listings;
            this.trainers = trainers;
        }


        public void ListingFile(string path)
        {

            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
            }

        }

        public void NewListing(string path, Listing[] listings, Trainer[] trainers){
            System.Console.WriteLine("Press 1 to add a listing, 2 to edit or delete a listing:");
            int response = int.Parse(Console.ReadLine());
            if( response == 1)
            {
                GetAllListings(listings, path, trainers);
            } else 
            {
                System.Console.WriteLine("Press 1 to edit a listing, 2 to delete a listing:");
                response = int.Parse(Console.ReadLine());

                if(response == 1)
                {
                    EditListing(listings, path);
                }
                if(response == 2)
                {
                    DeleteListing(listings);
                }

            }
        }


        

    
        public void GetAllListings(Listing[] listings, string path, Trainer[] trainers)
        {
            System.Console.WriteLine("Would you like to list a listings? Y for yes N for no");
            string response = Console.ReadLine().ToUpper();

            while (response == "Y")
            {
                int listingID = MakeListingID(listings, path);
                StreamWriter sw = File.AppendText(path);
                listings[Listing.GetCount()] = new Listing();

                listings[Listing.GetCount()].SetListingID(listingID);
                sw.Write($"{listingID}#");
                System.Console.WriteLine($"This Listing ID is: {listingID}");
                


                System.Console.WriteLine("Please enter the trainer ID of the trainer running this session: ");
                int trainerID = int.Parse(Console.ReadLine());
                int foundVal = -1;

                //////////////////////// Trainer first and last name //////////////////////
                StreamReader inFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
                Trainer.SetCount(0);
                string line = inFile.ReadLine();

                while(foundVal < 0)
                {
                    while( line != null)
                    {
                        string[] temp = line.Split('#');
                        trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                        Trainer.IncCount();
                        line = inFile.ReadLine();
                    }
                    inFile.Close();

                    for(int i = 0; i < Trainer.GetCount(); i ++)
                    {
                        if(trainerID == trainers[i].GetTrainerID())
                        {
                            foundVal = i;
                        }
                    }
                    
                    if(foundVal < 0)
                    {
                        System.Console.WriteLine("Trainer ID not found. You need to be a registerd trainer to be able to list a training session.");
                        System.Console.WriteLine("To enter a different trainer ID: press 1");
                        System.Console.WriteLine("To return the the Main Menu: press 2");
                        response = Console.ReadLine();

                        if(response == "1")
                        {
                            System.Console.WriteLine("Please enter the trainer ID of the trainer running this session:");
                            trainerID = int.Parse(Console.ReadLine());
                        }

                        if(response == "2")
                        {
                            MainMenu();
                        }
                    }
                }
                
                
                string trainerFirstName = trainers[foundVal].GetFirstName();
                listings[Listing.GetCount()].SetTrainerFirstName(trainerFirstName);
                sw.Write($"{trainerFirstName}#");

                string trainerLastName = trainers[foundVal].GetLastName();
                listings[Listing.GetCount()].SetTrainerLastName(trainerLastName);
                sw.Write($"{trainerLastName}#");
                

                ///////////////////////// Listing date and day //////////////////////////////////
                System.Console.WriteLine("Please enter the session date in MM/DD format: Remeber we are only accepting listings for May - August 2023 at this time. ");
                string date = Console.ReadLine();
                string day = GetDayFromDate(date);
                listings[Listing.GetCount()].SetListingDate(date);
    
                listings[Listing.GetCount()].SetListingDay(day);
                sw.Write($"{day}#");
                sw.Write($"{date}#");
    
                /////////////////////////  Session time  //////////////////////////////////
                System.Console.WriteLine("Please enter the session time");
                string listingTime = Console.ReadLine();
                listings[Listing.GetCount()].SetListingTime(listingTime);
                sw.Write($"{listingTime}#");


                //////////////////////// Recurring //////////////////////
                System.Console.WriteLine("Please enter if this session is recurring: Y for yes, N for no ");
                string userInput = Console.ReadLine().ToUpper();
                string recurring = "false";

                if( userInput == "Y"){
                    recurring = "This session is recurring";
                    sw.Write($"{recurring}#");
                } else {
                    if( userInput == "N"){
                        recurring = "This session is not recurring";
                        sw.Write($"{recurring}#");
                    } else {
                        System.Console.WriteLine("invalid input");
                    }
                } 
                listings[Listing.GetCount()].SetRecurring(recurring);


                ///////////////////////   Listing Cost   ///////////////////////////
                StreamReader costFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
                Trainer.SetCount(0);
                line = costFile.ReadLine();

                while( line != null)
                {
                    string[] temp = line.Split('#');
                    trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                    Trainer.IncCount();
                    line = costFile.ReadLine();
                }
                costFile.Close();

                for(int i = 0; i < Trainer.GetCount(); i ++)
                {
                    if(trainerID == trainers[i].GetTrainerID())
                    {
                        foundVal = i;
                    }
                }

                double listingCost = trainers[foundVal].GetHourlyRate();
                listings[Listing.GetCount()].SetListingCost(listingCost);
                sw.Write($"{listingCost}#");



                /////////////////////////  Max customers  ////////////////////////////////////////////
        
                StreamReader maxFile = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
                Trainer.SetCount(0);
                line = maxFile.ReadLine();

                while( line != null)
                {
                    string[] temp = line.Split('#');
                    trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                    Trainer.IncCount();
                    line = maxFile.ReadLine();
                }
                maxFile.Close();

                for(int i = 0; i < Trainer.GetCount(); i ++)
                {
                    if(trainerID == trainers[i].GetTrainerID())
                    {
                        foundVal = i;
                    }
                }

                int maxCustomers = trainers[foundVal].GetMaxCustomers();
                listings[Listing.GetCount()].SetMaxCustomers(maxCustomers);
                sw.Write($"{maxCustomers}#");




                ////// set spots taken and spots left
                int spotsTaken = 0;
                listings[Listing.GetCount()].SetSpotsTaken(spotsTaken);
                sw.Write($"{spotsTaken}#");

                // calculate spots left
                int spotsLeft = maxCustomers - spotsTaken;
                listings[Listing.GetCount()].SetSpotsLeft(spotsLeft);
                sw.Write($"{spotsLeft}#");




                // set session to open or full
                string availability = "";

                if(spotsLeft > 0){
                    availability = "This session is open for booking!";
                } else
                {
                    availability = "This session is full!";
                }

                listings[Listing.GetCount()].SetAvailability(availability);
                sw.Write($"{availability}#");



                System.Console.WriteLine("Please enter if you offer a first time discount: Y for yes, N for no");
                userInput = Console.ReadLine().ToUpper();
                string discount = "No disocunt offered";

                if( userInput == "Y"){
                    discount = "First time discount offered";
                    sw.Write($"{discount}#");
                } else {
                    if( userInput == "N"){
                        discount = "No discount offered";
                        sw.Write($"{discount}#");
                    } else {
                        System.Console.WriteLine($"{discount}");
                    }
                } 
                sw.WriteLine();
                listings[Listing.GetCount()].SetDiscount(discount);


                Listing.IncCount();

                sw.Close();

            System.Console.WriteLine("Would you like to register another Listings? Y for yes N for no");
            response = Console.ReadLine().ToUpper();

            }

        }

        public int MakeListingID(Listing[] listings, string path)
        {
            ReadInAllListings(listings, path);
            int max = listings[0].GetListingID();

            for(int i = 1; i < Listing.GetCount(); i ++)
            {
                if(listings[i].GetListingID() > max)
                {
                    max = listings[i].GetListingID();
                }
            }

            int listingID = max + 10;
            return listingID;
        }

    

        public int Find(int searchVal){
            for(int i = 0; i < Listing.GetCount(); i ++)
            {
                if(listings[i].GetListingID() == searchVal)
                {
                    return i;
                }
            }

            return -1;
        }



        public void ReadInAllListings(Listing[] listings, string path)
        {
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

            inFile.Close();
        }


        public void EditListing(Listing[] listings, string path)
        {
            System.Console.WriteLine("What is the listing ID of the listing you would like to update?");
            int searchVal = int.Parse(Console.ReadLine());

            while( searchVal != -1)
            {
                int foundVal = -1;
                ReadInAllListings(listings, path);

                for( int i = 0; i < Listing.GetCount(); i ++)
                {
                    if(searchVal == listings[i].GetListingID())
                    {
                        foundVal = i;
                    }
                }

                if( foundVal != -1)
                {
                    listings[foundVal].SetListingID(searchVal);

                    System.Console.WriteLine("What is the first name of the trainer teaching this class?");
                    listings[foundVal].SetTrainerFirstName(Console.ReadLine());

                    System.Console.WriteLine("What is the last name of the trainer teaching this class?");
                    listings[foundVal].SetTrainerLastName(Console.ReadLine());

                    System.Console.WriteLine("What date will the class be held?");
                    string date = Console.ReadLine();
                    listings[foundVal].SetListingDate(date);
                    
                    // get day from date
                    string day = GetDayFromDate(date);
                    listings[foundVal].SetListingDay(day);

                    System.Console.WriteLine("What time will the class be held?");
                    listings[foundVal].SetListingTime(Console.ReadLine());

                    System.Console.WriteLine("Does this class occur every week? Y for yes, N for no");
                    string recurring = "not recurring";
                    if(Console.ReadLine().ToUpper() == "Y")
                    {
                        recurring = "This session is recurring";
                        listings[foundVal].SetRecurring(recurring);
                    } else
                    {
                        if(Console.ReadLine().ToUpper() == "N")
                        {
                            recurring = "This session is not recurring";
                            listings[foundVal].SetRecurring(recurring);
                        } else
                        {
                            System.Console.WriteLine("Invalid");
                        }
                    }

                    System.Console.WriteLine("How much is this class per person?");
                    listings[foundVal].SetListingCost(int.Parse(Console.ReadLine()));

                    System.Console.WriteLine("What is the max amount of customers allowed to attend this class?");
                    int maxCustomers = int.Parse(Console.ReadLine());
                    listings[foundVal].SetMaxCustomers(maxCustomers);

                    System.Console.WriteLine("Please eneter the amount of spots taken:");
                    int spotsTaken = int.Parse(Console.ReadLine());
                    listings[foundVal].SetSpotsTaken(spotsTaken);

                    // calculate spots left
                    int spotsLeft = maxCustomers - spotsTaken;
                    listings[foundVal].SetSpotsLeft(spotsLeft);

                    // set availability 
                    string availability = "";
                        if(spotsLeft > 0){
                            availability = "This session is open for booking!";
                        } else
                        {
                            availability = "This session is full!";
                        }                        
                    listings[foundVal].SetAvailability(availability);


                    System.Console.WriteLine("Do you offer discount to first time customers? Y for yes, N for no");
                    string discount = "No discount";

                    if(Console.ReadLine().ToUpper() == "Y")
                    {
                        discount = "First time discount offered";
                    } else
                    {
                        discount = "No discount offered";
                    }

                    listings[foundVal].SetDiscount(discount);


                    StreamWriter reWrite = new StreamWriter(path);
                    for( int i = 0; i < Listing.GetCount(); i ++)
                    {
                        reWrite.Write($"{listings[i].GetListingID()}#");
                        reWrite.Write($"{listings[i].GetTrainerFirstName()}#");
                        reWrite.Write($"{listings[i].GetTrainerLastName()}#");
                        reWrite.Write($"{listings[i].GetListingDay()}#");
                        reWrite.Write($"{listings[i].GetListingDate()}#");
                        reWrite.Write($"{listings[i].GetListingTime()}#");
                        reWrite.Write($"{listings[i].GetRecurring()}#");
                        reWrite.Write($"{listings[i].GetListingCost()}#");
                        reWrite.Write($"{listings[i].GetMaxCustomers()}#");
                        reWrite.Write($"{listings[i].GetSpotsTaken()}#");
                        reWrite.Write($"{listings[i].GetSpotsLeft()}#");
                        reWrite.Write($"{listings[i].GetAvailability()}#");
                        reWrite.Write($"{listings[i].GetDiscount()}#");
                        reWrite.WriteLine(); 
                    }
                    reWrite.Close();
                }else 
                {
                    System.Console.WriteLine("Listing not found");
                }

                System.Console.WriteLine("If you'd like to edit the information of another listing, enter the listing ID. To exit -1");
                searchVal = int.Parse(Console.ReadLine());
            }
        }

        public void DeleteListing (Listing[] listings)
        {
            string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
            System.Console.WriteLine("What is the listing ID of the listing you would like to delete?");
            int searchVal = int.Parse(Console.ReadLine());

            while(searchVal != -1)
            {
                Listing.SetCount(0);
                StreamReader inFile = new StreamReader(path);
                string line = inFile.ReadLine();

                while( line != null)
                {
                    string[] temp = line.Split('#');
                    if(int.Parse(temp[0]) != searchVal)
                    {
                        listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], temp[6], int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), int.Parse(temp[10]), temp[11], temp[12]);
                        Listing.IncCount();
                    }
                    
                    line = inFile.ReadLine();
                }
                inFile.Close();


                StreamWriter reWrite = new StreamWriter(path);
                for( int i = 0; i < Listing.GetCount(); i ++)
                {
                    reWrite.Write($"{listings[i].GetListingID()}#");
                    reWrite.Write($"{listings[i].GetTrainerFirstName()}#");
                    reWrite.Write($"{listings[i].GetTrainerLastName()}#");
                    reWrite.Write($"{listings[i].GetListingDay()}#");
                    reWrite.Write($"{listings[i].GetListingDate()}#");
                    reWrite.Write($"{listings[i].GetListingTime()}#");
                    reWrite.Write($"{listings[i].GetRecurring()}#");
                    reWrite.Write($"{listings[i].GetListingCost()}#");
                    reWrite.Write($"{listings[i].GetMaxCustomers()}#");
                    reWrite.Write($"{listings[i].GetSpotsTaken()}#");
                    reWrite.Write($"{listings[i].GetSpotsLeft()}#");
                    reWrite.Write($"{listings[i].GetAvailability()}#");
                    reWrite.Write($"{listings[i].GetDiscount()}#");
                    reWrite.WriteLine(); 
                }
                reWrite.Close();

                System.Console.WriteLine("If you want to delete another listing, enter the listing ID. To go back to the listing Menu, enter -1:");
                searchVal = int.Parse(Console.ReadLine());
            }

            NewListing(path, listings, trainers);
        }

        public string GetDayFromDate(string date)
        {
            string day = "";
            bool valid = false;

            while(valid == false)
            {
                if( (date == "05/01") || (date == "05/08") || (date == "05/15") || (date == "05/22") || (date == "05/29") 
                || (date == "06/05") || (date == "06/12") || (date == "06/19") || (date == "06/26") 
                || (date == "07/03") || (date == "07/10") || (date == "07/17") || (date == "07/24") || (date == "07/31")
                || (date == "08/07") || (date == "08/14") || (date == "08/21") || (date == "08/28"))
                {
                    day = "Monday";
                    valid = true;
                }else
                {
                    if( (date == "05/02") || (date == "05/09") || (date == "05/16") || (date == "05/23") || (date == "05/30") 
                    || (date == "06/06") || (date == "06/13") || (date == "06/20") || (date == "06/27") 
                    || (date == "07/04") || (date == "07/11") || (date == "07/18") || (date == "07/25")
                    || (date == "08/01") || (date == "08/08") || (date == "08/15") || (date == "08/22") || (date == "08/29"))
                    {
                        day = "Tuesday";
                        valid = true;
                    }else
                    {
                        if( (date == "05/03") || (date == "05/10") || (date == "05/17") || (date == "05/24") || (date == "05/31") 
                        || (date == "06/07") || (date == "06/14") || (date == "06/21") || (date == "06/28") 
                        || (date == "07/05") || (date == "07/12") || (date == "07/19") || (date == "07/26")
                        || (date == "08/02") || (date == "08/09") || (date == "08/16") || (date == "08/23") || (date == "08/30"))
                        {
                            day = "Wednesday";
                            valid = true;
                        }else
                        {
                            if( (date == "05/04") || (date == "05/11") || (date == "05/18") || (date == "05/25")  
                            || (date == "06/01") || (date == "06/08") || (date == "06/15") || (date == "06/22") || (date == "06/29") 
                            || (date == "07/06") || (date == "07/13") || (date == "07/20") || (date == "07/27")
                            || (date == "08/03") || (date == "08/10") || (date == "08/17") || (date == "08/24") || (date == "08/31"))
                            {
                                day = "Thursday";
                                valid = true;
                            }else
                            {
                                if( (date == "05/05") || (date == "05/12") || (date == "05/19") || (date == "05/26")  
                                || (date == "06/02") || (date == "06/09") || (date == "06/16") || (date == "06/23") || (date == "06/30") 
                                || (date == "07/07") || (date == "07/14") || (date == "07/21") || (date == "07/28")
                                || (date == "08/04") || (date == "08/11") || (date == "08/18") || (date == "08/25"))
                                {
                                    day = "Friday";
                                    valid = true;
                                }else
                                {
                                    if( (date == "05/06") || (date == "05/13") || (date == "05/20") || (date == "05/27")  
                                    || (date == "06/03") || (date == "06/10") || (date == "06/17") || (date == "06/24")  
                                    || (date == "07/01") || (date == "07/08") || (date == "07/15") || (date == "07/22") || (date == "07/29")
                                    || (date == "08/05") || (date == "08/12") || (date == "08/19") || (date == "08/26"))
                                    {
                                        day = "Saturday";
                                        valid = true;
                                    }else
                                    {
                                        if( (date == "05/07") || (date == "05/14") || (date == "05/21") || (date == "05/28")  
                                        || (date == "06/04") || (date == "06/11") || (date == "06/18") || (date == "06/25")  
                                        || (date == "07/02") || (date == "07/09") || (date == "07/16") || (date == "07/23") || (date == "07/30")
                                        || (date == "08/06") || (date == "08/13") || (date == "08/20") || (date == "08/27"))
                                        {
                                            day = "Sunday";
                                            valid = true;
                                        }else
                                        {
                                            System.Console.WriteLine("Please enter a valid date May - August in MM/DD format:");
                                            date = Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return day;
        }

        static void MainMenu()
        {
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
            bookingUtility.ViewAvailableSessions();
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