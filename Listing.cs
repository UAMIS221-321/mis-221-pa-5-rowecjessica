namespace mis_221_pa_5_rowecjessica
{
    public class Listing
    {
        private int listingID;
        private string trainerFirstName;
        private string trainerLastName;
        private string day;
        private string sessionDate;
        private string sessionTime;
        private bool recurring;
        private double sessionCost;
        private int maxCustomers;
        private int spotsTaken;
        private bool availability;
        private int spotsLeft;
        private bool discounts;
        static private int count;


        public Listing(int listingsID, string trainerFirstName, string trainerLastName, string day, string sessionDate, string sessionTime, bool recurring, double sessionCost, int maxCustomers, int spotsTaken, bool availability, int spotsLeft, bool discounts)
        {
            this.listingID = listingID;
            this.trainerFirstName = trainerFirstName;
            this.trainerLastName = trainerLastName;
            this.day = day;
            this.sessionDate = sessionDate;
            this.sessionTime = sessionTime;
            this.recurring = recurring;
            this.sessionCost = sessionCost;
            this.maxCustomers = maxCustomers;
            this.spotsTaken = spotsTaken;
            this.availability = availability;
            this.spotsLeft = spotsLeft;
            this.discounts = discounts;
        }

        public Listing(){
            listingID = 0;
            trainerLastName = "Rowe";
            trainerFirstName = "Jess";
            day = "Monday";
            sessionDate = "06/26/2003";
            sessionTime = "12:30";
            recurring = true;
            sessionCost = 10.0;
            maxCustomers = 9;
            spotsTaken = 0;
            availability = true;
            spotsLeft = 8;
            discounts = true;
            count = 0;
        }

        public void SetListingID(int listingID)
        {
            this.listingID = listingID;
        }

        public int GetListingID()
        {
            return listingID;
        }

        public void SetTrainerLastName(string trainerLastName)
        {
            this.trainerLastName = trainerLastName;
        }

        public string GetTrainerLastNameID()
        {
            return trainerLastName;
        }

        public void SetTrainerFirstName(string trainerFirstName)
        {
            this.trainerFirstName = trainerFirstName;
        }

        public string GetTrainerFirstName()
        {
            return trainerFirstName;
        }

        public void SetListingDay(string Day)
        {
            this.day = day;
        }

        public string GetListingDay()
        {
            return day;
        }

        public void SetListingDate(string sessionDate)
        {
            this.sessionDate = sessionDate;
        }

        public string GetListingDate()
        {
            return sessionDate;
        }

        public void SetSessionTime(string sessionTime)
        {
            this.sessionTime = sessionTime;
        }

        public string GetSessionTime()
        {
            return sessionTime;
        }

        public void SetRecurring(bool recurring)
        {
            this.recurring = recurring;
        }

        public bool GetRecurring()
        {
            return recurring;
        }

        public void SetSessionCost(int sessionCost)
        {
            this.sessionCost = sessionCost;
        }

        public double GetSessionCost()
        {
            return sessionCost;
        }

        public void SetMaxCustomers(int maxCustomers)
        {
            this.maxCustomers = maxCustomers;
        }

        public int GetMaxCustomers()
        {
            return maxCustomers;
        }

        public void SetSpotsTaken(int spotsTaken)
        {
            this.spotsTaken = spotsTaken;
        }

        public int GetSpotsTaken()
        {
            return spotsTaken;
        }

        public void SetAvailability(bool availability)
        {
            this.availability = availability;
            if(spotsLeft > 0){
                availability = true;
            } else 
            {
                availability = false;
            }
        }

        public bool GetAvailability()
        {
            return availability;
        }

        public void SetSpotsLeft(int maxCusomters, int spotsTaken)
        {
            // this.spotsLeft = spotsLeft;
            spotsLeft = maxCusomters - spotsTaken;
        }

        public int GetSpotsLeft()
        {
            return spotsLeft;
        }

        public void SetDiscounts(bool discounts)
        {
            this.discounts = discounts;
        }

        public bool GetDiscounts()
        {
            return discounts;
        }

        static public void IncCount(){
            Listing.count ++;
        }

        static public void SetCount(int count){
            Listing.count = count;
        }
        static public int GetCount(){
            return Listing.count;
        }

    }
}