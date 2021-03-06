using System;
using System.Collections.Generic;
using Xunit;
using Bangazon;
using Bangazon.Managers;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.Win32.SafeHandles;
/*
Original Authors: Ryan McPherson and Kevin Haggerty
Updated By: Greg Turner
Purpose: Testing CustomerManager methods
*/

namespace Bangazon.Managers.Tests
{
    public class PaymentManagerShould : IDisposable
    {

        private PaymentType paymentType;
        private DatabaseConnection _db;
        private  PaymentTypeManager _ptm;
        /*  Method to clear the PaymentType table from the Test database. This will be 
            called by Dispose method. */
        public void Dispose()
        {
            // Clear any data PaymentType table in the test database
            _db.Update($"DELETE FROM PaymentType;");
            // Reset the Id sequence so new entries begin with 1
            _db.Update($"DELETE FROM sqlite_sequence WHERE name='PaymentType';");
        }

        public PaymentManagerShould()
        {

            string prodPath = System.Environment.GetEnvironmentVariable("BANGAZON_TEST_DB");
            // Create connection to test database and capture in DatabaseConnection variable db
            DatabaseConnection db = new DatabaseConnection(prodPath);
            // Set the private DatabaseConnection variable of _db to equal the DatabaseConnection of db
            _db = db;
            // Ensure there is a database with tables at the end of the test database connection
            DatabaseStartup databaseStartup = new DatabaseStartup(_db);

            _ptm = new PaymentTypeManager(_db);

        }

        [Fact]
        public void AddPaymentType()
        {
            PaymentType paymentType = new PaymentType();
                paymentType.Name = "Visa";
                paymentType.AccountNumber = 987944895;
            var result = _ptm.AddPaymentType(paymentType);

            Assert.True(result != 0);

            // Assert.Equal(_paymentType.Id, 1);
        }
    }
}