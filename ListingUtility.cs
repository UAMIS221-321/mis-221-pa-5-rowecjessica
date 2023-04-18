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
                GetAllListings(path);
            } else 
            {
                EditListing(listings, path);
            }
        }

    
        public void GetAllListings(string path)
        {
            System.Console.WriteLine("Would you like to list a listings? Y for yes N for no");
            string response = Console.ReadLine().ToUpper();

            while (response == "Y")
            {
                int listingID = MakeListingID(listings, path);
                StreamWriter sw = File.AppendText(path);

                listings[Listing.GetCount()] = new Listing();
                
                listings[Listing.GetCount()].SetListingID(listingID);
                sw.WriteLine();
                sw.Write($"{listingID}#");
                System.Console.WriteLine($"Your Listing ID is: {listingID}");
                
                System.Console.WriteLine("Please enter the trainer's first name");
                string trainerFirstName = Console.ReadLine();
                listings[Listing.GetCount()].SetTrainerFirstName(trainerFirstName);
                sw.Write($"{trainerFirstName}#");

                System.Console.WriteLine("Please enter the trainer's last name");
                string trainerLastName = Console.ReadLine();
                listings[Listing.GetCount()].SetTrainerLastName(trainerLastName);
                sw.Write($"{trainerLastName}#");

                System.Console.WriteLine("Please enter the day of the session");
                string day = Console.ReadLine();
                listings[Listing.GetCount()].SetListingDay(day);
                sw.Write($"{day}#");
                
                System.Console.WriteLine("Please enter the session date");
                string date = Console.ReadLine();
                listings[Listing.GetCount()].SetListingDate(date);
                sw.Write($"{date}#");

                System.Console.WriteLine("Please enter the session time");
                string sessionTime = Console.ReadLine();
                listings[Listing.GetCount()].SetSessionTime(sessionTime);
                sw.Write($"{sessionTime}#");

                System.Console.WriteLine("Please enter if this session is recurring: Y for yes, N for no ");
                string userInput = Console.ReadLine().ToUpper();
                bool recurring = false;

                if( userInput == "Y"){
                    recurring = true;
                    sw.Write($"Recurring#");
                } else {
                    if( userInput == "N"){
                        recurring = false;
                        sw.Write($"Not recurring#");
                    } else {
                        System.Console.WriteLine("invalid input");
                    }
                } 
            
                listings[Listing.GetCount()].SetRecurring(recurring);

                System.Console.WriteLine("Please enter the session cost");
                int sessionCost = int.Parse(Console.ReadLine());
                listings[Listing.GetCount()].SetSessionCost(sessionCost);
                sw.Write($"{sessionCost}#");

                System.Console.WriteLine("Please enter the max amount of customers");
                int maxCustomers = int.Parse(Console.ReadLine());
                listings[Listing.GetCount()].SetMaxCustomers(maxCustomers);
                sw.Write($"{maxCustomers}#");

                System.Console.WriteLine("Please enter if this session is stil open to booking: Y for yes, N for no");
                userInput = Console.ReadLine().ToUpper();
                bool availability = false;

                if( userInput == "Y"){
                    availability = true;
                    sw.Write($"This session is open for booking#");
                } else {
                    if( userInput == "N"){
                        availability = false;
                        sw.Write($"This session is closed for booking#");
                    } else {
                        System.Console.WriteLine("invalid input");
                    }
                } 
                listings[Listing.GetCount()].SetAvailability(availability);


                System.Console.WriteLine("Please eneter the amoutn of spots taken:");
                int spotsTaken = int.Parse(Console.ReadLine());
                listings[Listing.GetCount()].SetSpotsTaken(spotsTaken);

                listings[Listing.GetCount()].SetSpotsLeft(maxCustomers, spotsTaken);
                int spotsLeft = listings[Listing.GetCount()].GetSpotsLeft();
                sw.Write($"{spotsLeft}#");

                System.Console.WriteLine("Please enter if you offer a first time discount: Y for yes, N for no");
                userInput = Console.ReadLine().ToUpper();
                bool discount = false;

                if( userInput == "Y"){
                    discount = true;
                    sw.Write($"This trainer offers a discount for first timers#");
                } else {
                    if( userInput == "N"){
                        discount = false;
                        sw.Write($"This trainer does not offer discounts for first timers#");
                    } else {
                        System.Console.WriteLine("invalid input");
                    }
                } 
                
                listings[Listing.GetCount()].SetDiscounts(discount);


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
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], temp[5], bool.Parse(temp[6]), int.Parse(temp[7]), int.Parse(temp[8]), int.Parse(temp[9]), bool.Parse(temp[10]), int.Parse(temp[11]), bool.Parse(temp[12]));
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

                    System.Console.WriteLine("What day will the class be held?");
                    listings[foundVal].SetListingDay(Console.ReadLine());

                    System.Console.WriteLine("What time will the class be held?");
                    listings[foundVal].SetSessionTime(Console.ReadLine());

                    System.Console.WriteLine("Does this class occur every week? Y for yes, N for no");
                    if(Console.ReadLine().ToUpper() == "Y")
                    {
                        listings[foundVal].SetRecurring(true);
                    } else
                    {
                        if(Console.ReadLine().ToUpper() == "N")
                        {
                            listings[foundVal].SetRecurring(false);
                        } else
                        {
                            System.Console.WriteLine("Invalid");
                        }
                    }

                    System.Console.WriteLine("How much is this class per person?");
                    listings[foundVal].SetSessionCost(int.Parse(Console.ReadLine()));

                    System.Console.WriteLine("What is the max amount of customers allowed to attend this class?");
                    listings[foundVal].SetMaxCustomers(int.Parse(Console.ReadLine()));

                    System.Console.WriteLine("Do you offer discounts to first time customers? Y for yes, N for no");
                    if(Console.ReadLine().ToUpper() == "Y")
                    {
                        listings[foundVal].SetDiscounts(true);
                    } else
                    {
                        if(Console.ReadLine().ToUpper() == "N")
                        {
                            listings[foundVal].SetDiscounts(false);
                        } else 
                        {
                            System.Console.WriteLine("Invalid input");
                        }
                    }


                }
            }
        }







        // public void UpdateListings(){
        //     System.Console.WriteLine("What's the Listings ID of the Listings you'd like to update?");
        //     // will later be search for Listings ID
        //     int searchVal = int.Parse(Console.ReadLine());
        //     int i = Find(searchVal);
        //     int foundIndex = i;

        //     if(foundIndex != -1){
        //         System.Console.WriteLine("Please enter your first name");
        //         string firstName = Console.ReadLine();
        //         listings[Listing.GetCount()].SetTrainerFirstName(firstName);
        //         // sw.Write($"{firstName}#");

        //         System.Console.WriteLine("Please enter your last name");
        //         string lastName = Console.ReadLine();
        //         listings[Listing.GetCount()].SetTrainerLastName(lastName);
        //         // sw.Write($"{lastName}#");

        //         System.Console.WriteLine("Please enter your max amount of customers");
        //         int max = int.Parse(Console.ReadLine());
        //         listings[Listing.GetCount()].SetMaxCustomers(max);
        //         // sw.Write($"{max}#");

        //         Save();
        //     } 
            
        //     else{
        //         System.Console.WriteLine("Listings not found");
        //     }
        // }

    }
}