﻿using mis_221_pa_5_rowecjessica;

Menu();

static void Menu(){

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
    } else 
    {
        if(userInput == "2")
        {
            ManageListingData();
        } else
        {
            if (userInput == "3")
            {
                ManageCustomerBookingData();
            } else
            {
                if (userInput == "4")
                {
                    RunReports();
                } else
                {
                    Console.Clear();
                }
            }
        }
    }
}


static void ManageTrainerData(){
    string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Trainers.txt"; 
    Trainer[] trainers = new Trainer[50];
    TrainerUtility trainerUtility = new TrainerUtility(trainers);

    // trainerUtility.Save(path);
    trainerUtility.TrainerFile(path); 
    trainerUtility.NewTrainer(path);
    // utility.GetAllTrainersFromFile(trainers);
}

static void ManageListingData(){
    string path = @"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\Listings.txt";
    Listing[] listings = new Listing[50];
    ListingUtility listingUtility = new ListingUtility(listings);

    listingUtility.ListingFile(path);
    listingUtility.NewListing(path);
    // listingUtility.GetAllListings(path);
    
}

static void ManageCustomerBookingData(){
    System.Console.WriteLine("manage customer booking data");
}

static void RunReports(){
    System.Console.WriteLine("run reports");
}












