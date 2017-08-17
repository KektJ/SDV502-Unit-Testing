using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastwayCourier;


namespace SDV502_Unit_Testing
{
    [TestClass]

    //class containing Test methods which return as a success if a location is paired with it's expected zone. 
    public class CheckingDestinationZone_Should
    {
        private string Zone;
        private string ExpectedColour;
        [DataTestMethod]

        //Act Locations | Pink zone

        [DataRow("Motueka")]
        [DataRow("Mapua")]
        [DataRow("Maitai")]
        [DataRow("Nelson")]
        [DataRow("Atawhai")]
        [DataRow("Hope")]
        [DataRow("Brightwater")]
        [DataRow("Nelson")]
        [DataRow("Blenheim")]

        public void ReturnPink_When_PinkZoneLocationEntered(string Destination)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            Zone = fc.GetDestinationZone(Destination);

            ExpectedColour = "Pink";

            // Assert
            Assert.AreEqual(Zone, ExpectedColour);


        }

        [DataTestMethod]

        //Act Locations | Blue Zone

        [DataRow("Havelock")]
        [DataRow("Takaka")]
        [DataRow("Waihopai Valley")]
        [DataRow("Ward")]
        [DataRow("Seddon")]
        [DataRow("Riwaka")]
        public void ReturnBlue_WhenBlueZoneLocationEntered(string Destination)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            Zone = fc.GetDestinationZone(Destination);

            ExpectedColour = "Blue";

            // Assert
            Assert.AreEqual(Zone, ExpectedColour);


        }

        [DataTestMethod]

        //Act Locations | Orange Zone

        [DataRow("Reefton")]
        [DataRow("Hanmer Springs")]
        [DataRow("Kaikoura")]

        public void ReturnOrange_WhenOrangeZoneLocationEntered(string Destination)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            Zone = fc.GetDestinationZone(Destination);

            ExpectedColour = "Orange";

            // Assert
            Assert.AreEqual(Zone, ExpectedColour);


        }

        //Act Locations | Lime zone

        [DataTestMethod]
        [DataRow("Murchison")]
        [DataRow("Nelson Lakes National Park")]

        public void ReturnLime_WhenLimeZoneLocationEntered(string Destination, string Colour)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            Zone = fc.GetDestinationZone(Destination);

            ExpectedColour = "Lime";

            // Assert
            Assert.AreEqual(Zone, ExpectedColour);


        }
    }

    //Class containing a test method which returns success if a keynotfoundexception occurs when an non-existant location is entered
    [TestClass]
    public class EnteringNon_ExistantLocation_Should
    {
        public string Zone;
        public string ExpectedColour;
        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException),
        "Location does not exist")]
        public void Throw_KeyNotFoundException()
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            Zone = fc.GetDestinationZone("Masldhslfhsdlifjapisdj[gsj[wt[");

            ExpectedColour = "Lime";

            // Assert
            Assert.AreEqual(Zone, ExpectedColour);
        }



    }


    //class containing test methods which pass when an ArgumentOutOfRange exception is thrown upon entering a weight considered out of range (1-25) by the business case  
    [TestClass]
    public class EnteringOutOfRangeWeight_Should
    {
        [DataTestMethod]
        [DataRow(-2.0, "Pink" )]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException),
    "Input out of expected value range")]
        public void Throw_ArgumentOutOfRangeException_WhenWeightBelow1(double Weight, string Zone)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            decimal price = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            
            // Assert


        }

        [DataTestMethod]
        [DataRow(26, "Pink")]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException),
"Input out of expected value range")]
        public void Throw_ArgumentOutOfRangeException_WhenWeightAbove25(double Weight, string Zone)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();


            // Act
            decimal price = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;

            // Assert


        }
    }

    //class containing partitioning testing methods for expected parcel prices vs actual prices. Returns success if result matches expected result.
    [TestClass]
    public class PartitionTesting_ParcelPriceFromNelson_Should

        //PARTITION TESTING BEGINS HERE 

    {
        private string Zone;
        //Lime Zone
        //valid within valid partition
        [DataRow(13.30,  8.70)]
        //Testing Lower Partion (Move me)
        [DataRow(-1,  8.70)]
        //testing Upper partition
        [DataRow(25.1, 21.10)]

        [DataTestMethod]
        public void ReturnCorrectPrice_LimeZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Lime";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected); 


            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //Blue zone
        //valid within valid partition
        [DataRow(13.30, 6.95)]
        //Testing Lower Partion 
        [DataRow(-1, 6.95)]
        //testing Upper partition
        [DataRow(25.1, 6.95)]

        [DataTestMethod]
        public void ReturnCorrectPrice_BlueZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Blue";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //Pink Zone
        //valid within valid partition

        [DataRow(13.20, 4.15)]
        //Testing Lower Partion 
        [DataRow(-1, 4.15)]
        //testing Upper partition
        [DataRow(25.1, 4.15)]

        [DataTestMethod]
        public void ReturnCorrectPrice_PinkZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Pink";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //Orange Zone
        //valid within valid partition
        [DataRow(13.30, 12.95)]
        //Testing Lower Partion 
        [DataRow(-1, 12.95)]
        //testing Upper partition
        [DataRow(25.1, 31.55)]

        [DataTestMethod]
        public void ReturnCorrectPrice_OrangeZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Orange";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //PARTITION TESTING ENDS HERE 

        //BOUNDARY TESTING STARTS HERE




    }

    //Test class containing methods for boundary testing expected quote price vs actual price result for each zone. 


    [TestClass]
    public class BoundaryTesting_ParcelPriceFromNelson_Should
    {
        private string Zone;
        //Orange Zone
        //Upper Upper
        [DataRow(25.1, 31.55)]
        //Upper Lower
        [DataRow(25, 25.35)]
        //Lower upper
        [DataRow(0.1, 12.95)]
        //Lower Lower
        [DataRow(0.0, 12.95)]

        [DataTestMethod]
        public void ReturnCorrectPrice_OrangeZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Orange";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //Blue Zone
        //Upper Upper
        [DataRow(25.1, 6.95)]
        //Upper Lower
        [DataRow(25, 6.95)]
        //Lower upper
        [DataRow(0.1, 6.95)]
        //Lower Lower
        [DataRow(0, 6.95)]

        [DataTestMethod]

        public void ReturnCorrectPrice_BlueZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Blue";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }


        //Pink Zone
        //Upper Upper
        [DataRow(25.1, 4.15)]
        //Upper Lower
        [DataRow(25, 4.15)]
        //Lower upper
        [DataRow(0.1, 4.15)]
        //Lower Lower
        [DataRow(0, 4.15)]

        [DataTestMethod]

        public void ReturnCorrectPrice_PinkZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Blue";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

        //Lime Zone
        //Upper Upper
        [DataRow(25.1, 14.90)]
        //Upper Lower
        [DataRow(25, 14.90)]
        //Lower upper
        [DataRow(0.1, 8.70)]
        //Lower Lower
        [DataRow(0, 8.70)]

        [DataTestMethod]

        public void ReturnCorrectPrice_LimeZone(double Weight, double Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Lime";
            var Quote = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).Price;
            decimal ExpectedQuote = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedQuote, Quote);


        }

    }

        //class containing test methods which calculates the excess tickets needed for packages of a particular weight and tests them vs their expected values. 
        [TestClass]
        public class CalculatingAdditionalTickets_Should
        {
            private string Zone;
            [DataRow(25, 1)]
            [DataRow(16.5, 1)]
            [DataRow(5.5, 0)]
            [DataRow(0.1, 0)]

        [DataTestMethod]

            public void ReturnCorrectExcessTickets_LimeZone(double Weight, int Expected)
            {
                // Arrange
                var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
                Zone = "Lime";
                var Tickets = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).ExcessTickets;
                decimal ExpectedTickets = Convert.ToDecimal(Expected);

                // Assert
                Assert.AreEqual(ExpectedTickets, Tickets);


            }
        [DataRow(15, 0)]
        [DataRow(17.5, 1)]
        [DataRow(21, 2)]

        [DataTestMethod]

        public void ReturnCorrectExcessTickets_OrangeZone(double Weight, int Expected)
        {
            // Arrange
            var fc = new FastwayCourier.ParcelQuoteFromNelson();

            // Act
            Zone = "Orange";
            var Tickets = fc.CalculateQuote(Convert.ToDecimal(Weight), Zone).ExcessTickets;
            decimal ExpectedTickets = Convert.ToDecimal(Expected);

            // Assert
            Assert.AreEqual(ExpectedTickets, Tickets);


        }
    }


    }

/*
////////////////    ("`-''-/").___..--''"`-._ 
////////////////   `6_ 6  )   `-.  (     ).`-.__.`) 
////////////////   (_Y_.)'  ._   )  `._ `. ``-..-`  
////////////////  _..`--'_..-_/  /--'_.' ,'  
////////////////(il),-''  (li),'  ((!.-'           
*/

