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




        // make lsitingsings file if it doesnt exist 
        public void ListingFile(string path)
        {

            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
                tf.Close();
            }

        }


        // lsiting menu
        public void NewListing(string path, Listing[] listings, Trainer[] trainers){
            Console.Clear();
            System.Console.WriteLine("Select what you would like to do:");
            System.Console.WriteLine("1 - Add a listing");
            System.Console.WriteLine("2 - Edit a listing");
            System.Console.WriteLine("3 - Delete a listing");
            System.Console.WriteLine("4 - Go back to Main Menu");

            string input = Console.ReadLine();
            string valid = "No";

            while(valid == "No")
            {
                if( (input == "1") || (input == "2") || (input == "3") || (input == "4"))
                {
                    valid = "Yes";
                }else
                {
                    valid = "No";
                    Console.WriteLine("Please enter a valid menu option:");
                    input = Console.ReadLine();
                }
            }

            if(input == "1")
            {
                GetAllListings(listings, path, trainers);
            }

            if(input == "2")
            {
                EditListing(listings, path, trainers);
            }

            if(input == "3")
            {
                DeleteListing(listings);
            }

            if(input == "4")
            {
                MainMenu();
            }

        }


    
        // read in existing listings and add a new one
        public void GetAllListings(Listing[] listings, string path, Trainer[] trainers)
        {
            Console.Clear();
            System.Console.WriteLine("Would you like to add new a listings? Y for yes N for no");
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

            if(input == "N")
            {
                NewListing(path, listings, trainers);
            }

            while (input == "Y")
            {
                int listingID = MakeListingID(listings, path);
                listings[Listing.GetCount()] = new Listing();

                listings[Listing.GetCount()].SetListingID(listingID);
                System.Console.WriteLine($"This Listing ID is: {listingID}");

                
                /////////// Find trainer first and last name by trainerID  //////////////////////
                System.Console.WriteLine("Please enter the trainer ID of the trainer running this session: ");
                int ID = int.Parse(Console.ReadLine());
                int trainerID = CheckTrainerID(ID);
                int foundVal = FindTrainersName(trainerID);

                string trainerFirstName = trainers[foundVal].GetFirstName();
                listings[Listing.GetCount()].SetTrainerFirstName(trainerFirstName);

                string trainerLastName = trainers[foundVal].GetLastName();
                listings[Listing.GetCount()].SetTrainerLastName(trainerLastName);

                ///////////////////////// Listing date and day /////////////////////
                System.Console.WriteLine("Please enter the session date in MM/DD format: Remeber we are only accepting listings for May - August 2023 at this time. ");
                string date = Console.ReadLine();
                string day = GetDayFromDate(date);
                listings[Listing.GetCount()].SetListingDate(date);
                listings[Listing.GetCount()].SetListingDay(day);
    
                /////////////////////////  Session time  ///////////////////
                System.Console.WriteLine("Please enter the session time");
                string listingTime = Console.ReadLine();
                listings[Listing.GetCount()].SetListingTime(listingTime);



                //////////////////////// Recurring //////////////////////
                
                string recurring = ListingReccuring();
                if(recurring == "This session is recurring")
                    {
                        IfRecurring(recurring, day, listingID, trainerFirstName, trainerLastName, listingTime, foundVal, trainerID);
                    }else
                    {
                    listings[Listing.GetCount()].SetRecurring(recurring);

                    ///////////////////////   Listing Cost   ///////////////////////////
                    
                    foundVal = FindListingCost(trainerID);
                    double listingCost = trainers[foundVal].GetHourlyRate();
                    listings[Listing.GetCount()].SetListingCost(listingCost);

                    /////////////////////////  Max customers  ////////////////////////////////////////////

                    foundVal = FindMaxCustomers(trainerID);
                    int maxCustomers = trainers[foundVal].GetMaxCustomers();
                    listings[Listing.GetCount()].SetMaxCustomers(maxCustomers);


                    ////// set spots taken and spots left
                    int spotsTaken = 0;
                    listings[Listing.GetCount()].SetSpotsTaken(spotsTaken);


                    // calculate spots left
                    int spotsLeft = maxCustomers - spotsTaken;
                    listings[Listing.GetCount()].SetSpotsLeft(spotsLeft);


                    // set session to open or full
                    string availability = FindAvailability(spotsLeft);
                    listings[Listing.GetCount()].SetAvailability(availability);


                    //////////////////// discount 
                    
                    string discount = FindDiscount();
                    listings[Listing.GetCount()].SetDiscount(discount);
                    
                    //////////////////// write it out
                    StreamWriter sw = File.AppendText(path);
                    sw.Write($"{listingID}#");
                    sw.Write($"{trainerFirstName}#");
                    sw.Write($"{trainerLastName}#");
                    sw.Write($"{day}#");
                    sw.Write($"{date}#");
                    sw.Write($"{listingTime}#");
                    sw.Write($"{recurring}#");
                    sw.Write($"{listingCost}#");
                    sw.Write($"{maxCustomers}#");
                    sw.Write($"{spotsTaken}#");
                    sw.Write($"{spotsLeft}#");
                    sw.Write($"{availability}#");
                    sw.Write($"{discount}#");

                    
                    sw.WriteLine();
                    Listing.IncCount();
                    sw.Close();
                }

            System.Console.WriteLine("Would you like to register another Listings? Y for yes N for no");
            input = Console.ReadLine().ToUpper();

            }
        }


          // read in listings to find the most recent listing ID, add 10 and make the ID for the new listing 
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


        // find the location of the selected listing
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


        // read in listings text and make the listing array
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



        // check trainers file to make sure the trainerID entered exists
        public int CheckTrainerID(int ID)
        {
            int foundVal = -1;
            int trainerID = 0;

            StreamReader inFile = new StreamReader(@"./Trainers.txt");
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
                    if(ID == trainers[i].GetTrainerID())
                    {
                        foundVal = i;
                    }
                }
                
                if(foundVal < 0)
                {
                    System.Console.WriteLine("Trainer ID not found. You need to be a registerd trainer to be able to list a training session.");
                    System.Console.WriteLine("To enter a different trainer ID: press 1");
                    System.Console.WriteLine("To return the the Main Menu: press 2");
                    string response = Console.ReadLine();

                    if(response == "1")
                    {
                        System.Console.WriteLine("Please enter the trainer ID of the trainer running this session:");
                        ID = int.Parse(Console.ReadLine());
                    }

                    if(response == "2")
                    {
                        MainMenu();
                    }
                }

                trainerID = ID;
                }
            return trainerID;
        }


        // using trainerID, reade in trainers file and get the trainers name from that ID
        public int FindTrainersName(int trainerID)
        {
                int foundVal = -1;

                //////////////////////// Trainer first and last name //////////////////////
                StreamReader inFile = new StreamReader(@"./Trainers.txt");
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
                    
                }
            return foundVal;
        }

        // ask if the trainer wants to make the session recurring
        public string ListingReccuring()
        {
            System.Console.WriteLine("Please enter if this session is recurring: Y for yes, N for no ");
            string userInput = Console.ReadLine().ToUpper();
            string recurring = "false";

            if( userInput == "Y"){
                recurring = "This session is recurring";
            } else {
                if( userInput == "N"){
                    recurring = "This session is not recurring";
                } else {
                    System.Console.WriteLine("invalid input");
                }
            } 
            return recurring;
        }



        // if trainer chooses to make the session recurring, it will automatically book that session for that time every weekday of the day chosen
        public void IfRecurring(string recurring, string day, int listingID, string trainerFirstName, string trainerLastName, string listingTime, int foundVal, int trainerID)
        {
            if(recurring == "This session is recurring")
            {
                ///////////////////////   Listing Cost   ///////////////////////////
                    
                    foundVal = FindListingCost(trainerID);
                    double listingCost = trainers[foundVal].GetHourlyRate();
                    listings[Listing.GetCount()].SetListingCost(listingCost);

                    /////////////////////////  Max customers  ////////////////////////////////////////////

                    foundVal = FindMaxCustomers(trainerID);
                    int maxCustomers = trainers[foundVal].GetMaxCustomers();
                    listings[Listing.GetCount()].SetMaxCustomers(maxCustomers);


                    ////// set spots taken and spots left
                    int spotsTaken = 0;
                    listings[Listing.GetCount()].SetSpotsTaken(spotsTaken);


                    // calculate spots left
                    int spotsLeft = maxCustomers - spotsTaken;
                    listings[Listing.GetCount()].SetSpotsLeft(spotsLeft);


                    // set session to open or full
                    string availability = FindAvailability(spotsLeft);
                    listings[Listing.GetCount()].SetAvailability(availability);

                    // discount 
                    string discount = FindDiscount();
                    listings[foundVal].SetDiscount(discount);

                GetDateFromDay(day, listingID, trainerFirstName, trainerLastName, listingTime, foundVal, trainerID, listingCost, maxCustomers, spotsTaken, spotsLeft, availability, discount);
            }
        }


        // using trainerID, read in trainers text and find how much the trainer charges for their sessions
        public int FindListingCost(int trainerID)
        {
            StreamReader costFile = new StreamReader(@"./Trainers.txt");
            Trainer.SetCount(0);
            int foundVal = -1;

            string line = costFile.ReadLine();

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
            return foundVal;
        }


      // using trainerID, read in trainers text and find the max amoutn of customers the trainer offers for each session
        public int FindMaxCustomers(int trainerID)
        {
            StreamReader maxFile = new StreamReader(@"./Trainers.txt");
            Trainer.SetCount(0);
            int foundVal = -1;
            string line = maxFile.ReadLine();

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

            return foundVal;
        }


        
        // set if class is open or closed by looking at spots left
        public string FindAvailability(int spotsLeft)
        {
            string availability = "";
            if(spotsLeft > 0){
                availability = "This session is open for booking!";
            } else
            {
                availability = "This session is full!";
            }
            return availability;
        }


        // ask trainer if they would like to offer a discount for the listing
        public string FindDiscount()
        {
            System.Console.WriteLine("Please enter if you would like to offer a %5 discount on your classes: Y for yes, N for no");
            string userInput = Console.ReadLine().ToUpper();
            string discount = "No disocunt offered";

            if( userInput == "Y"){
                discount = "discount offered";
            } else {
                if( userInput == "N"){
                    discount = "No discount offered";
                } else {
                    System.Console.WriteLine($"{discount}");
                }
            }
            return discount; 
        }



        // read in current listings and chose one to edit
        public void EditListing(Listing[] listings, string path, Trainer[] trainers)
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
                    // Listing.SetCount(0);

                    listings[foundVal].SetListingID(searchVal);

                    /////////// Find trainer first and last name by trainerID  //////////////////////
                    System.Console.WriteLine("Please enter the trainer ID of the trainer running this session: ");
                    int ID = int.Parse(Console.ReadLine());
                    int trainerID = CheckTrainerID(ID);
                    int foundValTrainer = FindTrainersName(trainerID);

                    string trainerFirstName = trainers[foundValTrainer].GetFirstName();
                    listings[foundVal].SetTrainerFirstName(trainerFirstName);

                    string trainerLastName = trainers[foundValTrainer].GetLastName();
                    listings[foundVal].SetTrainerLastName(trainerLastName);


                    ///////////////////////// Listing date and day /////////////////////
                    System.Console.WriteLine("Please enter the session date in MM/DD format: Remeber we are only accepting listings for May - August 2023 at this time. ");
                    string date = Console.ReadLine();
                    string day = GetDayFromDate(date);
                    listings[foundVal].SetListingDate(date);
        
                    listings[foundVal].SetListingDay(day);
        
                    /////////////////////////  Session time  ///////////////////
                    System.Console.WriteLine("Please enter the session time");
                    string listingTime = Console.ReadLine();
                    listings[foundVal].SetListingTime(listingTime);

                    //////////////////////// Recurring //////////////////////
                    
                    string recurring = ListingReccuring();
                    listings[foundVal].SetRecurring(recurring);

                    ///////////////////////   Listing Cost   ///////////////////////////
                    
                    foundVal = FindListingCost(trainerID);
                    double listingCost = trainers[foundValTrainer].GetHourlyRate();
                    listings[foundVal].SetListingCost(listingCost);

                    /////////////////////////  Max customers  ////////////////////////////////////////////

                    foundVal = FindMaxCustomers(trainerID);
                    int maxCustomers = trainers[foundValTrainer].GetMaxCustomers();
                    listings[foundVal].SetMaxCustomers(maxCustomers);


                    ////// set spots taken and spots left
                    int spotsTaken = 0;
                    listings[foundVal].SetSpotsTaken(spotsTaken);


                    // calculate spots left
                    int spotsLeft = maxCustomers - spotsTaken;
                    listings[foundVal].SetSpotsLeft(spotsLeft);


                    // set session to open or full
                    string availability = FindAvailability(spotsLeft);
                    listings[foundVal].SetAvailability(availability);


                    //////////////////// discount 
                    
                    string discount = FindDiscount();
                    listings[foundVal].SetDiscount(discount);
                    
                    //////////////////// done 



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



        // choose which session the trainer would like to delete
        public void DeleteListing (Listing[] listings)
        {
            string path = @"./Listings.txt";
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


        // from date entered by user, find which day of the week that day is
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


        // right out the listing for the recurring listings by the day of the week they recur
        public void GetDateFromDay(string day, int listingID, string trainerFirstName, string trainerLastName, string listingTime, int foundVal, int trainerID, double listingCost, int maxCustomers, int spotsTaken, int spotsLeft, string availability, string discount)
        {
            StreamWriter sw = File.AppendText(@"./Listings.txt");
            if(day == "Monday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/01#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/08#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/15#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/22#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#05/29#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/05#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/12#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/19#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#06/26#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/03#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/10#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/17#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/24#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#07/31#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/07#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/14#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/21#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 170}#{trainerFirstName}#{trainerLastName}#{day}#08/28#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Tuesday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/02#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/09#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/16#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/23#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#05/30#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/06#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/13#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/20#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#06/27#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/04#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/11#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/18#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/25#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/01#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/08#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/15#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/22#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 170}#{trainerFirstName}#{trainerLastName}#{day}#08/29#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Wednesday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/03#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/10#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/17#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/24#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#05/31#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/07#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/14#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/21#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#06/28#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/05#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/12#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/19#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/26#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/02#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/09#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/16#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/23#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 170}#{trainerFirstName}#{trainerLastName}#{day}#08/30#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Thursday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/04#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/11#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/18#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/25#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#06/01#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/08#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/15#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/22#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#06/29#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/06#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/13#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/20#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/27#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/03#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/10#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/17#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/24#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 170}#{trainerFirstName}#{trainerLastName}#{day}#08/31#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Friday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/05#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/12#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/19#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/26#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#06/02#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/09#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/16#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/23#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#06/30#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/07#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/14#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/21#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/28#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/04#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/11#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/18#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/25#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Saturday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/06#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/13#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/20#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/27#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#06/03#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/10#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/17#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/24#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#07/01#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/08#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/15#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/22#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/29#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/05#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/12#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/19#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/26#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }

            if(day == "Sunday")
            {
                sw.WriteLine($"{listingID}#{trainerFirstName}#{trainerLastName}#{day}#05/07#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 10}#{trainerFirstName}#{trainerLastName}#{day}#05/14#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 20}#{trainerFirstName}#{trainerLastName}#{day}#05/21#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 30 }#{trainerFirstName}#{trainerLastName}#{day}#05/28#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 40}#{trainerFirstName}#{trainerLastName}#{day}#06/04#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 50}#{trainerFirstName}#{trainerLastName}#{day}#06/11#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 60}#{trainerFirstName}#{trainerLastName}#{day}#06/18#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 70}#{trainerFirstName}#{trainerLastName}#{day}#06/25#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 80}#{trainerFirstName}#{trainerLastName}#{day}#07/02#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 90}#{trainerFirstName}#{trainerLastName}#{day}#07/09#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 100}#{trainerFirstName}#{trainerLastName}#{day}#07/16#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 110}#{trainerFirstName}#{trainerLastName}#{day}#07/23#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 120}#{trainerFirstName}#{trainerLastName}#{day}#07/30#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 130}#{trainerFirstName}#{trainerLastName}#{day}#08/06#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 140}#{trainerFirstName}#{trainerLastName}#{day}#08/13#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 150}#{trainerFirstName}#{trainerLastName}#{day}#08/20#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
                sw.WriteLine($"{listingID + 160}#{trainerFirstName}#{trainerLastName}#{day}#08/27#{listingTime}#This session is recurring#{listingCost}#{maxCustomers}#{spotsTaken}#{spotsLeft}#{availability}#{discount}#");
            }
            sw.Close();
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
            string path = @"./Trainers.txt"; 
            Trainer[] trainers = new Trainer[200];
            TrainerUtility trainerUtility = new TrainerUtility(trainers);

            trainerUtility.TrainerFile(path); 
            trainerUtility.NewTrainer(path);
        }

        static void ManageListingData()
        {
            string path = @"./Listings.txt";
            Listing[] listings = new Listing[200];
            Trainer[] trainers = new Trainer[200];
            ListingUtility listingUtility = new ListingUtility(listings, trainers);

            listingUtility.ListingFile(path);
            listingUtility.NewListing(path, listings, trainers);
        }

        static void ManageCustomerBookingData()
        {
            string path = @"./transactions.txt";
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