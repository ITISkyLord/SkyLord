using ITI.SkyLord.DAL.Contexts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.Ef.Test
{
    [TestFixture]
    public class SetUpDBTest
    {
        [Test]
        public void CreateDatabase()
        {
            using (SetUpContext context = new SetUpContext())
            {
                context.Database.Create();
                Assert.That(context.Database.Exists());
            }
        }

        [Test]
        public void Return_false_if_db_already_exist()
        {
            using (SetUpContext context = new SetUpContext())
            {
               bool testCreation = context.Database.CreateIfNotExists();
                Assert.That(testCreation == false);

            }
        }

        [Test]
        public void DeleteDatabase()
        {
            using (SetUpContext context = new SetUpContext())
            {
              context.Database.Delete();
              bool isExists = context.Database.Exists();
              Assert.That(isExists == false);
            }
        }
    }
}
