namespace mis_221_pa_5_rowecjessica
{
    public class TrainerUtility
    {
        private Trainer[] trainers;

        public TrainerUtility(Trainer[] trainers)
        {
            this.trainers = trainers;
        }
       
        // Create trainer file if the text file does not already exist
        public void TrainerFile(string path)
        {
            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
            }

        }

        // Navigate to add, edit, or delete trainer information
        public void NewTrainer(string path)
        {
            System.Console.WriteLine("1 - Add a new trainer");
            System.Console.WriteLine("2 - Edit a current trainer's inforamtion");
            System.Console.WriteLine("3 - Delete a trainer from the system");
            System.Console.WriteLine("4 - Return to main menu");
            System.Console.WriteLine("5 - Exit application");
            string line = Console.ReadLine();

            int userInput = TrainerErrorHandle(line);

            if(userInput == 1)
            {
                GetAllTrainers(trainers, path);
            } 

            if(userInput == 2)
            {
                EditTrainer(trainers, path);
            }

            if(userInput == 3)
            {
                DeleteTrainer(trainers, path);
            }

            if(userInput == 4)
            {
                MainMenu(); 
            }

            if(userInput == 5)
            {
                Console.Clear();
            }
        }

        static int TrainerErrorHandle(string line)
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



        // Make new trainer by prompting user for information
        // Writing to the Trainers.txt file as the user enters information
        public void GetAllTrainers(Trainer[] trainers, string path)
        {
            System.Console.WriteLine("Would you like to register as a new trainer? Press Y for yes, any other key to return to trainer menu");
            string response = Console.ReadLine().ToUpper();

            while (response == "Y")
            {
                int trainerID = MakeTrainerID(trainers, path);
                StreamWriter sw = File.AppendText(path);

                sw.Write($"{trainerID}#");
                System.Console.WriteLine($"Your trainer ID is: {trainerID}");
                
                System.Console.WriteLine("Please enter your first name");
                string firstName = Console.ReadLine();
                sw.Write($"{firstName}#");

                System.Console.WriteLine("Please enter your last name");
                string lastName = Console.ReadLine();
                sw.Write($"{lastName}#");

                System.Console.WriteLine("Please enter your mailing address");
                string address = Console.ReadLine();
                sw.Write($"{address}#");
                
                System.Console.WriteLine("Please enter your Email");
                string email = Console.ReadLine();
                sw.Write($"{email}#");

                System.Console.WriteLine("Please enter your hourly rate per customer");
                string line = Console.ReadLine();
                double result = 0.0;
                bool parseSuccessful = double.TryParse(line, out result);

                while (result <= 0 ){
                    System.Console.WriteLine("Please enter a number:");
                    line = Console.ReadLine();
                    parseSuccessful = double.TryParse(line, out result);
                }
                double rate = result;
                sw.Write($"{rate}#");

                System.Console.WriteLine("Please enter your focus");
                string focus = Console.ReadLine();
                sw.Write($"{focus}#");

                System.Console.WriteLine("Please enter your max amount of customers");
                line = Console.ReadLine();
                int var = 0;
                parseSuccessful = int.TryParse(line, out var);

                while (var <= 0 ){
                    System.Console.WriteLine("Please enter a number:");
                    line = Console.ReadLine();
                    parseSuccessful = int.TryParse(line, out var);
                }

                int max = var;
                sw.Write($"{max}#");
                sw.WriteLine();

                sw.Close();
                System.Console.WriteLine("Would you like to register another trainer? Y for yes N for no");
                response = Console.ReadLine().ToUpper();
            }
            // takes user back to trainer menu
            NewTrainer(path);
        }


        // Reads through exiting trainers and determines the last trainer ID made
        // Makes a trainer ID for the new trainer that is one more than the current max ID
        public int MakeTrainerID(Trainer[] trainers, string path)
        {
            ReadInAllTrainers(trainers, path);
            int max = trainers[0].GetTrainerID();

            for(int i = 1; i < Trainer.GetCount(); i ++)
            {
                if(trainers[i].GetTrainerID() > max)
                {
                    max = trainers[i].GetTrainerID();
                }
            }
            
            int trainerID = max + 1;
            return trainerID;

        }


        // Reads in all current trainers on file and puts them into the trainers array
        public void ReadInAllTrainers(Trainer[] trainers, string path)
        {
            StreamReader inFile = new StreamReader(path);
            Trainer.SetCount(0);
            string line = inFile.ReadLine();

            while( line != null)
            {
                string[] temp = line.Split('#');
                trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                Trainer.IncCount();
                line = inFile.ReadLine();
            }

            inFile.Close();
        }


        // Searches for the trainer with the trainer ID given by the user
        // User re enters the information for that trainer
        // Program reads in existing trainers, identify the one to be changed, changes it
        // Program reprints the trainers with the updated trainer
        public void EditTrainer(Trainer[] trainers, string path)
        {
            System.Console.WriteLine("What is the trainer ID of the trainer who's information you'd like to change?");
            string line = Console.ReadLine();
            int searchVal = EditTrainerErrorHandle(line);
            
            while(searchVal != -1)
            {
                int foundVal = -1;
                ReadInAllTrainers(trainers, path);
            
                for(int i = 0; i < Trainer.GetCount(); i ++)
                {
                    if(searchVal == trainers[i].GetTrainerID())
                    {
                        foundVal = i;
                    }
                }

                // user re enters the information for the trainer with the trainer ID they entered
                if(foundVal != -1)
                {
                    trainers[foundVal].SetTrainerID(searchVal);
                    
                    System.Console.WriteLine("Please enter the trainer's first name");
                    string firstName = Console.ReadLine();
                    trainers[foundVal].SetFirstName(firstName);

                    System.Console.WriteLine("Please enter the trainer's last name");
                    string lastName = Console.ReadLine();
                    trainers[foundVal].SetLastName(lastName);

                    System.Console.WriteLine("Please the trainer's mailing address");
                    string address = Console.ReadLine();
                    trainers[foundVal].SetTrainerMailing(address);
                    
                    System.Console.WriteLine("Please the trainer's Email");
                    string email = Console.ReadLine();
                    trainers[foundVal].SetTrainerEmail(email);

                    System.Console.WriteLine("Please enter the trainer's hourly rate per customer");
                    line = Console.ReadLine();
                    double result = 0.0;
                    bool parseSuccessful = double.TryParse(line, out result);

                    while (result <= 0 ){
                        System.Console.WriteLine("Please enter a number:");
                        line = Console.ReadLine();
                        parseSuccessful = double.TryParse(line, out result);
                    }
                    double rate = result;
                    trainers[foundVal].SetHourlyRate(rate);

                    System.Console.WriteLine("Please enter the trainer's focus");
                    string focus = Console.ReadLine();
                    trainers[foundVal].SetFocus(focus);

                    System.Console.WriteLine("Please enter the trainer's max amount of customers");
                    line = Console.ReadLine();
                    int var = 0;
                    parseSuccessful = int.TryParse(line, out var);

                    while (var <= 0 ){
                        System.Console.WriteLine("Please enter a number:");
                        line = Console.ReadLine();
                        parseSuccessful = int.TryParse(line, out var);
                    }

                    int max = var;
                    trainers[foundVal].SetMaxCustomers(max);

                    // rewrites all trainers from the file including the updated one
                    StreamWriter reWrite = new StreamWriter(path);
                    for(int i = 0; i < Trainer.GetCount(); i ++)
                    {
                        reWrite.Write($"{trainers[i].GetTrainerID()}#");
                        reWrite.Write($"{trainers[i].GetFirstName()}#");
                        reWrite.Write($"{trainers[i].GetLastName()}#");
                        reWrite.Write($"{trainers[i].GetTrainerMailing()}#");
                        reWrite.Write($"{trainers[i].GetTrainerEmail()}#");
                        reWrite.Write($"{trainers[i].GetHourlyRate()}#");
                        reWrite.Write($"{trainers[i].GetFocus()}#");
                        reWrite.Write($"{trainers[i].GetMaxCustomers()}#");
                        reWrite.WriteLine(); 
                    }
                    reWrite.Close();
                } 
                else 
                {
                    System.Console.WriteLine($"There is no trainer on file with the ID number {searchVal}.");
                }

                System.Console.WriteLine("If you'd like to edit the information of another trainer, enter their trainer ID. To exit -1");
                line = Console.ReadLine();
                if( line == "-1")
                {
                    Console.Clear();
                }
                searchVal = EditTrainerErrorHandle(line);
            }
        }

        public int EditTrainerErrorHandle(string line)
        {
            int result = 0;

            if( line == "-1")
            {
                NewTrainer(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
            }

            bool parseSuccessful = int.TryParse(line, out result);

            while (result <= 0 ){
                System.Console.WriteLine("Please enter a number:");
                line = Console.ReadLine();
                if( line == "-1")
                {
                    NewTrainer(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
                }
                parseSuccessful = int.TryParse(line, out result);
            }
            int searchVal = result;

            return searchVal;
        }


        // Delete trainer from trainer file
        public void DeleteTrainer(Trainer[] trainers, string path)
        {
            System.Console.WriteLine("What is the trainer ID of the trainer you would like to delete?");
            string line = Console.ReadLine();
            int searchVal = DeleteTrainerErrorHandle(line);
            
            // Read in current trainers and find the trainer ID given
            while(searchVal >= 0)
            {
                Trainer.SetCount(0);
                int foundVal = -1;
                StreamReader inFile = new StreamReader(path);
                line = inFile.ReadLine();

                while( line != null)
                {
                    string[] temp = line.Split('#');
                    if(int.Parse(temp[0]) == searchVal)
                    {
                        foundVal = 1;
                    }

                    if(int.Parse(temp[0]) != searchVal)
                    {
                        trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
                        Trainer.IncCount();
                    }

                    line = inFile.ReadLine();
                }
                inFile.Close();

                // if that trainer ID exists, rewrite trainers after deleting the found trainer
                // if that trainer ID doesn't exist, prompt for new trainer ID
                if(foundVal >= 0)
                {
                    StreamWriter reWrite = new StreamWriter(path);
                    for(int i = 0; i < Trainer.GetCount(); i ++)
                    {
                        reWrite.Write($"{trainers[i].GetTrainerID()}#");
                        reWrite.Write($"{trainers[i].GetFirstName()}#");
                        reWrite.Write($"{trainers[i].GetLastName()}#");
                        reWrite.Write($"{trainers[i].GetTrainerMailing()}#");
                        reWrite.Write($"{trainers[i].GetTrainerEmail()}#");
                        reWrite.Write($"{trainers[i].GetHourlyRate()}#");
                        reWrite.Write($"{trainers[i].GetFocus()}#");
                        reWrite.Write($"{trainers[i].GetMaxCustomers()}#");
                        reWrite.WriteLine(); 
                    }
                    reWrite.Close();
                }else
                {
                    System.Console.WriteLine($"There is no trainer on file with the ID number {searchVal}.");
                }
                System.Console.WriteLine("If you would like to delete another trainer, enter the trainer ID. To return to the menu enter -1:");
                line = Console.ReadLine();
                searchVal = DeleteTrainerErrorHandle(line);
            }
            NewTrainer(path);
        }


        static  int DeleteTrainerErrorHandle(string line)
        {
            int result = 0;
            if( line == "-1")
            {
                NewTrainer(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
            }
            bool parseSuccessful = int.TryParse(line, out result);

            while (result <= 0 ){
                System.Console.WriteLine("Please enter a number:");
                line = Console.ReadLine();
                if( line == "-1")
                {
                    NewTrainer(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt");
                }
                parseSuccessful = int.TryParse(line, out result);
            }
            int searchVal = result;

            return searchVal;
        }

        // Copy of menu from main
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
            ListingUtility listingUtility = new ListingUtility(listings);

            listingUtility.ListingFile(path);
            listingUtility.NewListing(path);
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
            ReportUtility reportUtility = new ReportUtility();

            reportUtility.ReportMenu();
        }

    }
}
