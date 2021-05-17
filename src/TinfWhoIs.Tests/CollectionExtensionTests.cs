using System;
using System.Collections.Generic;
using System.Linq;
using TinfWhoIs.Core.Extensions;
using Xunit;
using static Xunit.Assert;

namespace TinfWhoIs.Tests
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void CollectionExtension_AddOrUpdate_ShouldAdd()
        {
            // Arrange
            CatEntityFake cat = new() { Name = "Martha" };

            var list = new List<CatEntityFake> { cat };

            // Act
            list.AddOrUpdate<CatEntityFake, Guid>(cat);

            // Assert
            Single(list);
            Collection(list, x => Equal(cat.Name, x.Name));
        }

        [Fact]
        public void CollectionExtension_AddOrUpdate_ShouldAddSecond()
        {
            // Arrange
            CatEntityFake cat = new() { Name = "Martha" };
            CatEntityFake cat2 = new() { Name = "Garfield" };

            var list = new List<CatEntityFake> { cat };

            // Act
            list.AddOrUpdate<CatEntityFake, Guid>(cat2);

            // Assert
            Equal(2, list.Count);
            Collection(list,
                x => Equal(cat.Name, x.Name),
                x => Equal(cat2.Name, x.Name));
        }

        [Fact]
        public void CollectionExtension_AddOrUpdate_ShouldUpdate()
        {
            // Arrange
            CatEntityFake cat = new() { Name = "Martha" };
            CatEntityFake cat2 = new() { Name = "Garfield" };

            var list = new List<CatEntityFake> { cat, cat2 };

            var updatedCat = list.Last();
            updatedCat.Name = "Tom";

            // Act
            list.AddOrUpdate<CatEntityFake, Guid>(updatedCat);

            // Assert
            Equal(2, list.Count);
            Collection(list,
                x => Equal(cat.Name, x.Name),
                x => Equal(updatedCat.Name, x.Name));
        }
    }
}
