namespace mis_221_pa_5_rowecjessica
{
    public class ListingUtility
    {
        private Listing[] listings;


        public ListingUtility(Listing[] listings)
        {
            this.listings = listings;
        }

        public void ListingFile(string path)
        {

            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
            }

        }

        public void NewListing(string path){
            System.Console.WriteLine("Are you a new Listings? Enter Y for yes or N for no");
            string response = Console.ReadLine().ToUpper();
            if( response == "Y")
            {
                GetAllListings(listings, path);
            } else 
            {
                EditListing(listings, path);
            }
        }

    
        public void GetAllListings(Listing[] listings, string path)
        {
            System.Console.WriteLine("Would you like to list a listings? Y for yes N for no");
            string response = Console.ReadLine().ToUpper();

            while (response == "Y")
            {
                int listingID = MakeListingID(listings, path);
                StreamWriter sw = File.AppendText(path);
                listings[Listing.GetCount()] = new Listing();

                listings[Listing.GetCount()].SetListingID(listingID);
                sw.WriteLine($"{listingID}#");
                System.Console.WriteLine($"This Listing ID is: {listingID}");
                
                System.Console.WriteLine("Please enter the trainer's first name");
                string trainerFirstName = Console.ReadLine();
                listings[Listing.GetCount()].SetTrainerFirstName(trainerFirstName);
                sw.Write($"{trainerFirstName}#");

                System.Console.WriteLine("Please enter the trainer's last name");
                string trainerLastName = Console.ReadLine();
                listings[Listing.GetCount()].SetTrainerLastName(trainerLastName);
                sw.Write($"{trainerLastName}#");
                
                System.Console.WriteLine("Please enter the session date");
                string date = Console.ReadLine();
                listings[Listing.GetCount()].SetListingDate(date);
    
                // get day from date - do not prompt user
                string day = GetDayFromDate(date);
                listings[Listing.GetCount()].SetListingDay(day);
                sw.Write($"{day}#");
                sw.Write($"{date}#");
    

                System.Console.WriteLine("Please enter the session time");
                string listingTime = Console.ReadLine();
                listings[Listing.GetCount()].SetListingTime(listingTime);
                sw.Write($"{listingTime}#");

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

                System.Console.WriteLine("Please enter the listing cost");
                int listingCost = int.Parse(Console.ReadLine());
                listings[Listing.GetCount()].SetListingCost(listingCost);
                sw.Write($"{listingCost}#");

                System.Console.WriteLine("Please enter the max amount of customers");
                int maxCustomers = int.Parse(Console.ReadLine());
                listings[Listing.GetCount()].SetMaxCustomers(maxCustomers);
                sw.Write($"{maxCustomers}#");

                System.Console.WriteLine("Please eneter the amount of spots taken:");
                int spotsTaken = int.Parse(Console.ReadLine());
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

        public string GetDayFromDate(string date)
        {
            string day = "";

            switch (date)
            {
                case "05/01":
                    day = "Monday";
                    break;
                case "05/02":
                    day = "Tuesday";
                    break;
            }

            return day;
        }

    }
}