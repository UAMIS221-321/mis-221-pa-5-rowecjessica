namespace mis_221_pa_5_rowecjessica
{
    public class TrainerUtility
    {
        private Trainer[] trainers;

        public TrainerUtility(Trainer[] trainers)
        {
            this.trainers = trainers;
        }


        public void GetAllTrainers(Trainer[] trainers)
        {
            StreamWriter newFile = new StreamWriter("Trainers.txt");

            Trainer.GetCount();
            System.Console.WriteLine("Please enter your trainer ID, -1 to stop.");
            int ID = int.Parse(Console.ReadLine());

            while (ID != -1)
            {
                trainers[Trainer.GetCount()] = new Trainer();
                trainers[Trainer.GetCount()].SetTrainerID(ID);
                newFile.WriteLine($"{ID}#");

                System.Console.WriteLine("Please enter your name");
                string name = Console.ReadLine();
                trainers[Trainer.GetCount()].SetTrainerName(name);
                newFile.Write($"{name}#");

                System.Console.WriteLine("Please enter your mailing address");
                string address = Console.ReadLine();
                trainers[Trainer.GetCount()].SetTrainerMailing(address);
                newFile.Write($"{address}#");
                
                System.Console.WriteLine("Please enter your Email");
                string email = Console.ReadLine();
                trainers[Trainer.GetCount()].SetTrainerEmail(email);
                newFile.Write($"{email}#");

                System.Console.WriteLine("Please enter your hourly rate");
                double rate = double.Parse(Console.ReadLine());
                trainers[Trainer.GetCount()].SetHourlyRate(rate);
                newFile.Write($"{rate}#");

                System.Console.WriteLine("Please enter your focus");
                string focus = Console.ReadLine();
                trainers[Trainer.GetCount()].SetFocus(focus);
                newFile.Write($"{focus}#");

                System.Console.WriteLine("Please enter your max amount of customers");
                int max = int.Parse(Console.ReadLine());
                trainers[Trainer.GetCount()].SetMaxCustomers(max);
                newFile.Write($"{max}#");


                Trainer.IncCount();

                System.Console.WriteLine("Please enter your trainer ID, -1 to stop.");
                ID = int.Parse(Console.ReadLine());

            }

            newFile.Close();
        }

        public void GetAllTrainersFromFile(Trainer[] trainers){
            StreamReader inFile = new StreamReader("Trainers.txt");
            Trainer.SetCount(0);
            string line = inFile.ReadLine();

            while(line != null){
                string[] temp = line.Split('#');
                trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3], double.Parse(temp[4]), temp[5], int.Parse(temp[6]));
                System.Console.WriteLine(temp[3]);

                Trainer.IncCount();
                line = inFile.ReadLine();
            }
            inFile.Close();

        }

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