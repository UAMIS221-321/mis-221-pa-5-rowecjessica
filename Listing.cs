namespace mis_221_pa_5_rowecjessica
{
    public class Listing
    {
        private int listingID;
        private string trainerFirstName;
        private string trainerLastName;
        private string day;
        private string date;
        private string listingTime;
        private string recurring;
        private int listingCost;
        private int maxCustomers;
        private int spotsTaken;
        private int spotsLeft;
        private string availability;
        private string discount;
        static private int count;


        public Listing(int listingID, string trainerFirstName, string trainerLastName, string day, string date, string listingTime, string recurring, int listingCost, int maxCustomers, int spotsTaken, int spotsLeft, string availability, string discount)
        {
            this.listingID = listingID;
            this.trainerFirstName = trainerFirstName;
            this.trainerLastName = trainerLastName;
            this.day = day;
            this.date = date;
            this.listingTime = listingTime;
            this.recurring = recurring;
            this.listingCost = listingCost;
            this.maxCustomers = maxCustomers;
            this.spotsTaken = spotsTaken;
            this.spotsLeft = spotsLeft;
            this.availability = availability;
            this.discount = discount;
        }

        public Listing(){
            listingID = 0;
            trainerLastName = "Rowe";
            trainerFirstName = "Jess";
            day = "Monday";
            date = "06/26/2003";
            listingTime = "12:30";
            recurring = "Recurring";
            listingCost = 10;
            maxCustomers = 9;
            spotsTaken = 0;
            spotsLeft = 8;
            availability = "Available";
            discount = "discount offered";
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

        public string GetTrainerLastName()
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

        public void SetListingDay(string day)
        {
            this.day = day;
        }

        public string GetListingDay()
        {
            return day;
        }

        public void SetListingDate(string date)
        {
            this.date = date;
        }

        public string GetListingDate()
        {
            return date;
        }

        public void SetListingTime(string listingTime)
        {
            this.listingTime = listingTime;
        }

        public string GetListingTime()
        {
            return listingTime;
        }

        public void SetRecurring(string recurring)
        {
            this.recurring = recurring;
        }

        public string GetRecurring()
        {
            return recurring;
        }

        public void SetListingCost(int listingCost)
        {
            this.listingCost = listingCost;
        }

        public int GetListingCost()
        {
            return listingCost;
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

        public void SetAvailability(string availability)
        {
            this.availability = availability;
            // if(spotsLeft > 0){
            //     availability = "This session is open for booking";
            // } else 
            // {
            //     availability = "This session is closed";
            // }
        }

        public string GetAvailability()
        {
            return availability;
        }

        public void SetSpotsLeft(int spotsLeft)
        {
            // this.spotsLeft = spotsLeft;
            // spotsLeft = maxCusomters - spotsTaken;
            this.availability = availability;
        }

        public int GetSpotsLeft()
        {
            return spotsLeft;
        }

        public void SetDiscount(string discount)
        {
            this.discount = discount;
        }

        public string GetDiscount()
        {
            return discount;
        }

        static public void IncCount()
        {
            Listing.count ++;
        }

        static public void SetCount(int count)
        {
            Listing.count = count;
        }
        static public int GetCount()
        {
            return Listing.count;
        }

    }
}