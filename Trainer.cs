
namespace mis_221_pa_5_rowecjessica
{
    public class Trainer
    {
        private int trainerID;
        private string firstName;
        private string lastName;
        private string trainerMailing;
        private string trainerEmail;
        private double hourlyRate;
        private string focus;
        private int maxCustomers;
        static private int count;

        public Trainer(int trainerID, string firstName, string lastName, string trainerMailing, string trainerEmail, double hourlyRate, string focus, int maxCustomers)
        {
            
            this.trainerID = trainerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.trainerMailing = trainerMailing;
            this.trainerEmail = trainerEmail;
            this.hourlyRate = hourlyRate;
            this.focus = focus;
            this.maxCustomers = maxCustomers;
        }
        public Trainer()
        {
            trainerID = 0;
            firstName = "Jess";
            lastName = "Rowe";
            trainerMailing = "";
            trainerEmail = "";
            hourlyRate = 0.0;
            focus = "";
            maxCustomers = 0;
            count = 0;
        }

        public void SetTrainerID(int trainerID)
        {
            this.trainerID = trainerID;
        }

        public int GetTrainerID()
        {
            return trainerID;
        }

        public void SetFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public string GetFirstName()
        {
            return firstName;
        }

        public void SetLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public string GetLastName()
        {
            return lastName;
        }

        public void SetTrainerMailing(string trainerMailing)
        {
            this.trainerMailing = trainerMailing;
        }

        public string GetTrainerMailing()
        {
            return trainerMailing;
        }

        public void SetTrainerEmail(string trainerEmail)
        {
            this.trainerEmail = trainerEmail;
        }

        public string GetTrainerEmail()
        {
            return trainerEmail;
        }

        public void SetHourlyRate(double hourlyRate)
        {
            this.hourlyRate = hourlyRate;
        }

        public double GetHourlyRate()
        {
            return hourlyRate;
        }

        public void SetFocus(string focus)
        {
            this.focus = focus;
        }

        public string GetFocus()
        {
            return focus;
        }

        public void SetMaxCustomers(int maxCustomers)
        {
            this.maxCustomers = maxCustomers;
        }

        public int GetMaxCustomers()
        {
            return maxCustomers;
        }

        static public void IncCount(){
            Trainer.count ++;
        }

        static public void SetCount(int count){
            Trainer.count = count;
        }
        static public int GetCount(){
            return Trainer.count;
        }

    }
}