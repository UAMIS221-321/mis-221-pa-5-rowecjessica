namespace mis_221_pa_5_rowecjessica
{
    public class Customer
    {
        private int customerID;
        private int sessionID;
        private string customerFirstName;
        private string customerLastName;
        private string customerEmail;
        private string trainingDate;
        private int trainerID;
        private string trainerFirstName;
        private string trainerLastName;
        private string status;
        static private int count;

    public Customer(int customerID, int sessionID, string customerFirstName, string customerLastName, string customerEmail, string trainingDate, int trainerID, string trainerFirstName, string trainerLastName, string status)
    {
        this.customerID = customerID;
        this.sessionID = sessionID;
        this.customerFirstName = customerFirstName;
        this.customerLastName = customerLastName;
        this.customerEmail = customerEmail;
        this.trainingDate = trainingDate;
        this.trainerID = trainerID;
        this.trainerFirstName = trainerFirstName;
        this.trainerLastName = trainerLastName;
        this.status = status;
    }

    public Customer()
    {
        customerID = 1010;
        sessionID = 10;
        customerFirstName = "Jess";
        customerLastName = "Rowe";
        customerEmail = "rowecjessica";
        trainingDate = "06/26";
        trainerID = 1;
        trainerFirstName = "Jacob";
        trainerLastName = "Tinnell";
        status = "open";
        count = 0;
    }
    }
}