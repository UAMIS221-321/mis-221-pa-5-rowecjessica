using mis_221_pa_5_rowecjessica;

Menu();

static void Menu()
{
    System.Console.WriteLine("Welcome to TLAC!");
    System.Console.WriteLine("Please select what you would like to do:");
    System.Console.WriteLine("1 - Manage Trainer Data");
    System.Console.WriteLine("2 - Managae Listing Data");
    System.Console.WriteLine("3 - Manage Customer booking data");
    System.Console.WriteLine("4 - Run Reports");
    System.Console.WriteLine("5 - Exit the application");

    string userInput = Console.ReadLine();

    if (userInput == "1")
    {
        ManageTrainerData();
    }

    if(userInput == "2")
    {
        ManageListingData();
    } 
    
    if (userInput == "3")
    {
        ManageCustomerBookingData();
    }
            
    if (userInput == "4")
    {
        RunReports();            
    }
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













