namespace mis_221_pa_5_rowecjessica
{
    public class TrainerUtility
    {
        
        private Trainer[] trainers;


        public TrainerUtility(Trainer[] trainers)
        {
            this.trainers = trainers;
        }

        public void TrainerFile(string path)
        {
            if(!File.Exists(path))
            {
                StreamWriter tf = File.CreateText(path);
            }

        }

        public void NewTrainer(string path){
            System.Console.WriteLine("Are you a new trainer? Enter Y for yes or N for no");
            string response = Console.ReadLine().ToUpper();
            if( response == "Y")
            {
                GetAllTrainers(trainers, path);
            } else 
            {
                System.Console.WriteLine("Are you a trainer wanting to update your information? Y for yes, N for no");
                response = Console.ReadLine().ToUpper();
                if( response == "Y"){
                    EditTrainer(trainers, path);
                } else {
                    if( response == "N"){
                        System.Console.WriteLine("exit application");
                    } else {
                        System.Console.WriteLine("Invalid response");
                    }
                }
            }
        }


        public void GetAllTrainers(Trainer[] trainers, string path)
        {
            System.Console.WriteLine("Would you like to register as a new trainer? Y for yes N for no");
            string response = Console.ReadLine().ToUpper();

            while (response == "Y")
            {
                int trainerID = MakeTrainerID(trainers, path);
                StreamWriter sw = File.AppendText(path);

                trainers[Trainer.GetCount()] = new Trainer();
                
                trainers[Trainer.GetCount()].SetTrainerID(trainerID);
                sw.WriteLine();
                sw.Write($"{trainerID}#");
                System.Console.WriteLine($"Your trainer ID is: {trainerID}");
                
                System.Console.WriteLine("Please enter your first name");
                string firstName = Console.ReadLine();
                trainers[Trainer.GetCount()].SetFirstName(firstName);
                sw.Write($"{firstName}#");

                System.Console.WriteLine("Please enter your last name");
                string lastName = Console.ReadLine();
                trainers[Trainer.GetCount()].SetLastName(lastName);
                sw.Write($"{lastName}#");

                System.Console.WriteLine("Please enter your mailing address");
                string address = Console.ReadLine();
                trainers[Trainer.GetCount()].SetTrainerMailing(address);
                sw.Write($"{address}#");
                
                System.Console.WriteLine("Please enter your Email");
                string email = Console.ReadLine();
                trainers[Trainer.GetCount()].SetTrainerEmail(email);
                sw.Write($"{email}#");

                System.Console.WriteLine("Please enter your hourly rate");
                double rate = double.Parse(Console.ReadLine());
                trainers[Trainer.GetCount()].SetHourlyRate(rate);
                sw.Write($"{rate}#");

                System.Console.WriteLine("Please enter your focus");
                string focus = Console.ReadLine();
                trainers[Trainer.GetCount()].SetFocus(focus);
                sw.Write($"{focus}#");

                System.Console.WriteLine("Please enter your max amount of customers");
                int max = int.Parse(Console.ReadLine());
                trainers[Trainer.GetCount()].SetMaxCustomers(max);
                sw.Write($"{max}#");


                Trainer.IncCount();

                sw.Close();

            System.Console.WriteLine("Would you like to register another trainer? Y for yes N for no");
            response = Console.ReadLine().ToUpper();

            }
        }

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


        public void EditTrainer(Trainer[] trainers, string path)
        {

            System.Console.WriteLine("What is the trainer ID of the trainer who'd information you'd like to change? Enter -1 to exit");
            int searchVal = int.Parse(Console.ReadLine());

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


                if(foundVal != -1)
                {
                    trainers[foundVal].SetTrainerID(searchVal);
                    
                    System.Console.WriteLine("Please enter your first name");
                    string firstName = Console.ReadLine();
                    trainers[foundVal].SetFirstName(firstName);

                    System.Console.WriteLine("Please enter your last name");
                    string lastName = Console.ReadLine();
                    trainers[foundVal].SetLastName(lastName);

                    System.Console.WriteLine("Please enter your mailing address");
                    string address = Console.ReadLine();
                    trainers[foundVal].SetTrainerMailing(address);
                    
                    System.Console.WriteLine("Please enter your Email");
                    string email = Console.ReadLine();
                    trainers[foundVal].SetTrainerEmail(email);

                    System.Console.WriteLine("Please enter your hourly rate");
                    double rate = double.Parse(Console.ReadLine());
                    trainers[foundVal].SetHourlyRate(rate);

                    System.Console.WriteLine("Please enter your focus");
                    string focus = Console.ReadLine();
                    trainers[foundVal].SetFocus(focus);

                    System.Console.WriteLine("Please enter your max amount of customers");
                    int max = int.Parse(Console.ReadLine());
                    trainers[foundVal].SetMaxCustomers(max);



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
                    System.Console.WriteLine("Trainer not found");
                }

                System.Console.WriteLine("If you'd like to edit the information of another trainer, enter their trainer ID. To exit -1");
                searchVal = int.Parse(Console.ReadLine());
            }
        }




            // UpdateTrainer();

        

        // public void Save(string path)
        // {
        //     StreamReader inFile = new StreamReader(path);
        //     for(int i = 0; i < Trainer.GetCount(); i ++)
        //     {
        //         string input = inFile.ReadLine();
        //         trainers[i] = input.Split('#');

        //     }

        //     for(int i = 0; i < Trainer.GetCount(); i ++)
        //     {
        //         System.Console.WriteLine(trainers[i]);
        //     }

        //     inFile.Close();
        // }

        // public int Find(int searchVal){
        //     for(int i = 0; i < Trainer.GetCount(); i ++)
        //     {
        //         if(trainers[i].GetTrainerID() == searchVal)
        //         {
        //             return i;
        //         }
        //     }

        //     return -1;
        // }

        // public void UpdateTrainer(){
        //     System.Console.WriteLine("What's the trainer ID of the trainer youd like to update?");
        //     // will later be search for trainer ID
        //     int searchVal = int.Parse(Console.ReadLine());
        //     int i = Find(searchVal);
        //     int foundIndex = i;

        //     if(foundIndex != -1){
        //         System.Console.WriteLine("Please enter your first name");
        //         string firstName = Console.ReadLine();
        //         trainers[Trainer.GetCount()].SetFirstName(firstName);
        //         // sw.Write($"{firstName}#");

        //         System.Console.WriteLine("Please enter your last name");
        //         string lastName = Console.ReadLine();
        //         trainers[Trainer.GetCount()].SetLastName(lastName);
        //         // sw.Write($"{lastName}#");

        //         System.Console.WriteLine("Please enter your mailing address");
        //         string address = Console.ReadLine();
        //         trainers[Trainer.GetCount()].SetTrainerMailing(address);
        //         // sw.Write($"{address}#");
                
        //         System.Console.WriteLine("Please enter your Email");
        //         string email = Console.ReadLine();
        //         trainers[Trainer.GetCount()].SetTrainerEmail(email);
        //         // sw.Write($"{email}#");

        //         System.Console.WriteLine("Please enter your hourly rate");
        //         double rate = double.Parse(Console.ReadLine());
        //         trainers[Trainer.GetCount()].SetHourlyRate(rate);
        //         // sw.Write($"{rate}#");

        //         System.Console.WriteLine("Please enter your focus");
        //         string focus = Console.ReadLine();
        //         trainers[Trainer.GetCount()].SetFocus(focus);
        //         // sw.Write($"{focus}#");

        //         System.Console.WriteLine("Please enter your max amount of customers");
        //         int max = int.Parse(Console.ReadLine());
        //         trainers[Trainer.GetCount()].SetMaxCustomers(max);
        //         // sw.Write($"{max}#");

        //         Save();
        //     } 
            
        //     else{
        //         System.Console.WriteLine("Trainer not found");
        //     }
        // }


        // public void EditTrainerData(){
        //     System.Console.WriteLine("What is your trainer ID?");
        //     int searchVal = int.Parse(Console.ReadLine());

        //     for(int i = 0; i < Trainer.GetCount(); i ++){
        //         if( trainers[i].GetTrainerID() = searchVal ){
        //             return i;
        //         }
        //     }

        //     System.Console.WriteLine("Here is what your current data looks like:");
        //     System.Console.WriteLine($"{trainers[i]}");
        //     System.Console.WriteLine("Please select what you would like to update: ");
        //     System.Console.WriteLine("1 - Your first name");
        //     System.Console.WriteLine("2 - Your last name");
        //     System.Console.WriteLine("3 - Your mailing address");
        //     System.Console.WriteLine("4 - Your email address");
        //     System.Console.WriteLine("5 - Your hourly rate");
        //     System.Console.WriteLine("6 - Your focus");
        //     System.Console.WriteLine("7 - Your max amount of customers");

        //     int editValue = int.Parse(Console.ReadLine());

        //     if(editValue = 1){
        //         System.Console.WriteLine("What would you like to change your first name to?");
        //         string newFirstName = Console.ReadLine();
        //         trainers[i].SetFirstName(newFirstName);
        //     }

        //     if(editValue = 2){
        //         System.Console.WriteLine("What would you like to change your last name to?");
        //         string newLastName = Console.ReadLine();
        //         trainers[i].SetLastName(newLastName);
        //     }

        //     if(editValue = 3){
        //         System.Console.WriteLine("What would you like to change your mailing address to?");
        //         string newTrainerMailing = Console.ReadLine();
        //         trainers[i].SetTrainerMailing(newTrainerMailing);
        //     }

        //     if(editValue = 4){
        //         System.Console.WriteLine("What would you like to change your email address to?");
        //         string newTrainerEmail= Console.ReadLine();
        //         trainers[i].SetTrainerEmail(newTrainerEmail);
        //     }

        //     if(editValue = 5){
        //         System.Console.WriteLine("What would you like to change your hourly rate to?");
        //         double newHourlyRate = double.Parse(Console.ReadLine());
        //         trainers[i].SetHourlyRate(newHourlyRate);
        //     }

        //     if(editValue = 6){
        //         System.Console.WriteLine("What would you like to change your focus to?");
        //         string newFocus = Console.ReadLine();
        //         trainers[i].SetFocus(newFocus);
        //     }

        //     if(editValue = 7){
        //         System.Console.WriteLine("What would you like to change your max amount of customers to?");
        //         int newMaxCustomers = int.Parse(Console.ReadLine());
        //         trainers[i].SetMaxCustomers(newMaxCustomers);
        //     }
        // }



        // public void GetAllTrainersFromFile(Trainer[] trainers){
        //     StreamReader inFile = new StreamReader("Trainers.txt");
        //     Trainer.SetCount(0);

        //     string line = inFile.ReadLine();

        //     while(line != null){
        //         string[] temp = line.Split('#');
        //         trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4], double.Parse(temp[5]), temp[6], int.Parse(temp[7]));
        //         System.Console.WriteLine(temp[3]);

        //         Trainer.IncCount();
        //         line = inFile.ReadLine();
        //     }

        //     inFile.Close();

        // }

        // public void AddTrainer(){
        //     System.Console.WriteLine("Please enter your trainer ID");
        //     Trainer newTrainer = new Trainer();
        //     newTrainer.SetTrainerID(int.Parse(Console.ReadLine()));

        //     System.Console.WriteLine("Please enter your name");
        //     newTrainer.SetTrainerName(Console.ReadLine());

        //     System.Console.WriteLine("Please enter mailing address");
        //     newTrainer.SetTrainerMailing(Console.ReadLine());

        //     System.Console.WriteLine("Please enter your email");
        //     newTrainer.SetTrainerEmail(Console.ReadLine());

        //     System.Console.WriteLine("Please eneter your hourly rate");
        //     newTrainer.SetHourlyRate(double.Parse(Console.ReadLine()));

        //     System.Console.WriteLine("Please eneter your focus");
        //     newTrainer.SetFocus(Console.ReadLine());

        //     System.Console.WriteLine("Please enter your max amount of cutomers");
        //     newTrainer.SetMaxCustomers(int.Parse(Console.ReadLine()));

        //     trainers[Trainer.GetCount()] = newTrainer;
        //     Trainer.IncCount();

        //     Save(trainerID, trainerName, trainerMailing, trainerEmail, hourlyRate, focus, maxCustomers);
        // }

        // public void Save(int trainerID, string trainerName, string trainerMailing, string trainerEmail, double hourlyRate, string focus, int maxCustomers){
        //     StreamWriter outFile = new StreamWriter("Trainer.txt");
        //     for(int i = 0; i < Trainer.GetCount(); i ++){
        //         outFile.WriteLine($"{trainerID}#{trainerName}#{trainerMailing}#{trainerEmail}#{hourlyRate}#{focus}#{maxCustomers}");
        //     }

        //     outFile.Close();
        // }
    }
}