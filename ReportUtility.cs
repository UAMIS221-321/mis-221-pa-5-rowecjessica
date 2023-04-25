namespace mis_221_pa_5_rowecjessica
{
    public class ReportUtility
    {
        public void ReportMenu(Booking[] bookings)
        {
            System.Console.WriteLine("What report function would you like to do?");
            System.Console.WriteLine("1 - Individual Customer Sessions");
            System.Console.WriteLine("2 - Historical Customer Sessions");
            System.Console.WriteLine("3 - Historical Revenue Report");

            string userInput = Console.ReadLine();

            if(userInput == "1")
            {
                IndividualCustomerSesssions();
            }

            if(userInput == "2")
            {
                HistoricalCustomerSessions(bookings);
            }

            if(userInput == "3")
            {
                HistoricalRevenueReport();
            }
        }



        public void IndividualCustomerSesssions()
        {
            System.Console.WriteLine("What is the email address of the customer you would like to see a report for?");
            string email = Console.ReadLine();
            string customerName = GetCustomerName(email);
            DisplayReport(email, customerName);
            SaveReport(email, customerName);
        }

        public string GetCustomerName(string email)
        {
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = sr.ReadLine();
            string customerName= "Null";

            while(line != null)
            {
                string[] temp = line.Split('#');
                if(temp[4] == email)
                {
                    customerName = $"{temp[2]} {temp [3]}";
                }
                line = sr.ReadLine();
            }

            sr.Close();
            return customerName;
        }

        public void DisplayReport(string email, string customerName)
        {
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            string line = sr.ReadLine();
            System.Console.WriteLine($"Previous training sessions for {customerName}:");

            while(line != null)
            {
                string[] temp = line.Split('#');

                if(temp[4] == email)
                {
                    System.Console.WriteLine($"Listing ID: {temp[1]}, Date: {temp[5]}, Trainer: #{temp[6]} {temp[7]} {temp[8]}");
                }

                line = sr.ReadLine();
            }

            sr.Close();
        }

        public void SaveReport(string email, string customerName)
        {
            System.Console.WriteLine("Would you like to save this customers report as a file? Y for yes, N for no");
            string input = Console.ReadLine().ToUpper();

            if(input == "Y")
            {
                ///////////////////
                // ADD IN OPTION TO ADD TO EXISTING FILE
                /////////////////////////////////

                System.Console.WriteLine("What would you like to name this file?");
                string fileName = Console.ReadLine();
                StreamWriter newFile = File.CreateText(fileName);
                newFile.Close();
                
                StreamWriter nf = new StreamWriter(fileName);
                nf.WriteLine($"Previous training sessions for {customerName}:");
                nf.Close();
                
                StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
                string line = sr.ReadLine();

                while(line != null)
                {
                    string[] temp = line.Split('#');

                    if(temp[4] == email)
                    {
                        StreamWriter writeReport = File.AppendText(fileName);
                        writeReport.WriteLine($"Listing ID: {temp[1]}, Date: {temp[5]}, Trainer: #{temp[6]} {temp[7]} {temp[8]}");
                        writeReport.Close();
                    }
                    line = sr.ReadLine();
                }

                sr.Close();
            }
        }

        public void HistoricalCustomerSessions(Booking[] bookings)
        {
            
            int max = 0; 
            int customerID = 0;
            int currentID = 0;
            StreamReader sr = new StreamReader(@"C:\Users\rowec\OneDrive\MIS221\PAs\mis-221-pa-5-rowecjessica\transactions.txt");
            Booking.SetCount(0);
            string line = sr.ReadLine();

            while( line != null)
            {
                string[] temp = line.Split('#');
                bookings[Booking.GetCount()] = new Booking(int.Parse(temp[0]), int.Parse(temp[1]), temp[2], temp[3], temp[4], temp[5], int.Parse(temp[6]), temp[7], temp[8], temp[9]);
                Booking.IncCount();
                line = sr.ReadLine();
            }
            sr.Close();

            for(int i = 0; i < Booking.GetCount(); i ++)
            {
                currentID = bookings[i].GetCustomerID();
                if(currentID > max)
                {
                    max = currentID;
                }
                Booking.IncCount();
            }

            for(int n = 0; n < max; n ++)
            {
                for(int j = 0; j < Booking.GetCount(); j ++)
                {
                    if(bookings[j].GetCustomerID() == j)
                    {
                        System.Console.WriteLine($"Listing ID: {bookings[j].GetSessionID()}, Date: {bookings[j].GetTrainingDate()}, Trainer: #{bookings[j].GetBookingTrainerID()} {bookings[j].GetBookingTrainerFirstName()} {bookings[j].GetBookingTrainerLastName()}");
                    }
                }
                Booking.IncCount();
            }
        }

        public void HistoricalRevenueReport()
        {
            System.Console.WriteLine("HRR");
        }
    }
}