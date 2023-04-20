namespace mis_221_pa_5_rowecjessica
{
    public class Booking
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

    public Booking(int customerID, int sessionID, string customerFirstName, string customerLastName, string customerEmail, string trainingDate, int trainerID, string trainerFirstName, string trainerLastName, string status)
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

    public Booking()
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

    public void SetCustomerID(int customerID)
    {
        this.customerID = customerID;
    }

    public int GetCustomerID()
    {
        return customerID;
    }

    public void SetSessionID(int sessionID)
    {
        this.sessionID = sessionID;
    }

    public int GetSessionID()
    {
        return sessionID;
    }

    public void SetCustomerFirstName(string customerFirstName)
    {
        this.customerFirstName = customerFirstName;
    }

    public string GetCustomerFirstName()
    {
        return customerFirstName;
    }

    public void SetCustomerLastName(string customerLastName)
    {
        this.customerLastName = customerLastName;
    }

    public string GetCustomerLastName()
    {
        return customerLastName;
    }

    public void SetCustomerEmail(string customerEmail)
    {
        this.customerEmail = customerEmail;
    }

    public string GetCustomerEmail()
    {
        return customerEmail;
    }    

    public void SetTrainingDate(string trainingDate)
    {
        this.trainingDate = trainingDate;
    }

    public string GetTrainingDate()
    {
        return trainingDate;
    }

    public void SetBookingTrainerID(int trainerID)
    {
        this.trainerID = trainerID;
    }

    public int GetBookingTrainerID()
    {
        return trainerID;
    }

    public void SetBookingTrainerFirstName(string trainerFirstName)
    {
        this.trainerFirstName = trainerFirstName;
    }

    public string GetBookingTrainerFirstName()
    {
        return trainerFirstName;
    }

    public void SetBookingTrainerLastName(string trainerLastName)
    {
        this.trainerLastName = trainerLastName;
    }

    public string GetBookingTrainerLastName()
    {
        return trainerLastName;
    }
    
    public void SetStatus(string status)
    {
        this.status = status;
    }

    public string GetStatus()
    {
        return status;
    }

    static public void SetCount(int count)
    {
        Booking.count = count;
    }

    static public int GetCount()
    {
        return Booking.count;
    }

    static public void IncCount()
    {
        Booking.count ++;
    }
    }
}