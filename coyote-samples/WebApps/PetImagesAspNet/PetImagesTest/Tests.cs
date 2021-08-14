// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Coyote;
using Microsoft.Coyote.SystematicTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetImages;
using PetImages.Contracts;
using PetImagesTest.Clients;
using PetImagesTest.MessagingMocks;
using PetImagesTest.StorageMocks;

namespace PetImagesTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task TestFirstScenario()
        {
            // Initialize the mock in-memory DB and account manager.
            var cosmosState = new MockCosmosState();
            var database = new MockCosmosDatabase(cosmosState);
            var accountContainer = await database.CreateContainerAsync(Constants.AccountContainerName);
            var petImagesClient = new TestPetImagesClient(accountContainer);

            // Create an account request payload
            var account = new Account()
            {
                Name = "MyAccount"
            };

            // Call CreateAccount twice without awaiting, which makes both methods run
            // asynchronously with each other.
            var task1 = petImagesClient.CreateAccountAsync(account);
            var task2 = petImagesClient.CreateAccountAsync(account);

            // Then wait both requests to complete.
            await Task.WhenAll(task1, task2);

            var statusCode1 = task1.Result.StatusCode;
            var statusCode2 = task2.Result.StatusCode;

            // Finally, assert that only one of the two requests succeeded and the other
            // failed. Note that we do not know which one of the two succeeded as the
            // requests ran concurrently (this is why we use an exclusive OR).
            Assert.IsTrue(
                (statusCode1 == HttpStatusCode.OK && statusCode2 == HttpStatusCode.Conflict) ||
                (statusCode1 == HttpStatusCode.Conflict && statusCode2 == HttpStatusCode.OK));
        }
    }
}
