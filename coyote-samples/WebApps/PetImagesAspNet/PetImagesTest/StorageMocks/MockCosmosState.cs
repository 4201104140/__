// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using PetImages.Entities;
using PetImages.Exceptions;

using Container = System.Collections.Concurrent.ConcurrentDictionary<string, PetImages.Entities.DbItem>;
using Database = System.Collections.Concurrent.ConcurrentDictionary<
    string, System.Collections.Concurrent.ConcurrentDictionary<string, PetImages.Entities.DbItem>>;

namespace PetImagesTest.StorageMocks
{
    public class MockCosmosState
    {
        private readonly Database Database = new ();

        public void CreateContainer(string containerName)
        {
            EnsureContainerExistsInDatabase(containerName);
            _ = this.Database.TryAdd(containerName, new Container());
        }

        internal void EnsureContainerDoesNotExistInDatabase(string containerName)
        {
            if (this.Database.ContainsKey(containerName))
            {
                throw new DatabaseContainerAlreadyExists();
            }
        }

        internal void EnsureContainerExistsInDatabase(string containerName)
        {
            if (!this.Database.ContainsKey(containerName))
            {
                throw new DatabaseContainerDoesNotExist();
            }
        }
    }
}
