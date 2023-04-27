using mis_221_pa_5_rowecjessica;


Menu();

static void Menu()
{
    Console.Clear();
    System.Console.WriteLine("Welcome to TLAC!");
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
    Listing[] listings = new Listing[500];
    Trainer[] trainers = new Trainer[200];
    ListingUtility listingUtility = new ListingUtility(listings, trainers);

    listingUtility.ListingFile(path);
    listingUtility.NewListing(path, listings, trainers);
}


static void ManageCustomerBookingData()
{
    string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt";
    Booking[] bookings = new Booking[200];
    Listing[] listings = new Listing[500];
    Trainer[] trainers = new Trainer[200];
    BookingUtility bookingUtility = new BookingUtility(bookings, listings, trainers);

    bookingUtility.BookingFile(path);
    bookingUtility.ViewAvailableSessions(listings);
}


static void RunReports()
{
    Booking[] bookings = new Booking[200];
    Listing[] listings = new Listing[500];
    Trainer[] trainers = new Trainer[200];
    Customer[] customers = new Customer[200];
    ReportUtility reportUtility = new ReportUtility(bookings, listings, trainers, customers);

    reportUtility.ReportMenu(bookings, listings, trainers);
}













