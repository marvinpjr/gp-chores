using FakeItEasy;
using GpJobs.Api.Models;
using GpJobs.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace GpJobs.Tests
{
    [TestClass]
    public class jobServiceTests
    {
        private static IHostingEnvironment _fakeHostingEnv;
        private static IDataService<JobProfile> _jobService;

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            _fakeHostingEnv = A.Fake<IHostingEnvironment>();
            _fakeHostingEnv.ContentRootPath = "C:\\git\\gp-chores\\GpChores.Api\\GpChores.Api\\";
            _jobService = new DataService<JobProfile>(_fakeHostingEnv);
        }

        [TestMethod]
        public void CanCreateJob()
        {
            // arrange                        
            Thread.Sleep(500);
            var testjob = getSingleTestJob();

            // act
            var result = _jobService.Create(testjob).Result;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanGetJobById()
        {
            // arrange
            Thread.Sleep(500);
            var testjob = getSingleTestJob();
            var newjobId = _jobService.Create(testjob).Result;

            // act
            var returnedjob = _jobService.GetById(newjobId).Result;

            // assert
            Assert.AreEqual(newjobId, returnedjob.Id);
            Assert.AreEqual(testjob.Name, returnedjob.Name);
            Assert.AreEqual(testjob.Description, returnedjob.Description);
        }

        [TestMethod]
        public void CanGetMultipeJobs()
        {
            // arrange
            Thread.Sleep(500);
            var jobs = getTestJobs();
            foreach (var job in jobs)
            {
                Thread.Sleep(250);
                _jobService.Create(job);
            }

            // act
            var returnedjobs = _jobService.GetAll().Result;

            // assert
            Assert.IsTrue(returnedjobs.Count >= jobs.Count);
        }

        [TestMethod]
        public void CanGetMatches()
        {
            // arrange
            Thread.Sleep(500);
            var jobs = getTestJobs();
            foreach (var job in jobs)
            {
                Thread.Sleep(250);
                _jobService.Create(job);
            }

            // act
            var matchingJobs = _jobService.GetMatching(j => j.Name.ToLower().Contains("two")).Result;
            var allJobs = _jobService.GetAll().Result;

            // assert
            Assert.IsTrue(matchingJobs.Count > 0);
            Assert.AreNotEqual(matchingJobs.Count, allJobs.Count);
        }

        [TestMethod]
        public void CanUpdateJob()
        {
            // arrange 
            Thread.Sleep(500);
            var job = getSingleTestJob();
            var jobId = _jobService.Create(job).Result;
            var newName = "A Brand New Name";

            // act
            job.Name = newName;
            _jobService.Update(jobId, job);
            var updatedjob = _jobService.GetById(jobId).Result;

            // assert
            Assert.AreEqual(newName, updatedjob.Name);
        }

        [TestMethod]
        public void CanDeleteJobById()
        {
            // arrange 
            Thread.Sleep(500);
            var job = getSingleTestJob();
            var jobId = _jobService.Create(job).Result;

            // act
            var result = _jobService.Delete(jobId).Result;            
            var shouldBeNull = _jobService.GetById(jobId).Result;

            // assert
            Assert.IsNull(shouldBeNull);
        }

        [TestMethod]
        public void CannotCreateJobWithAlreadyExistingName()
        {
            // arrange 

            // act

            // assert
        }

        [ClassCleanup]
        public static void CleanUpAfterTests()
        {
            var jobs = _jobService.GetAll().Result;
            foreach (var job in jobs)
            {
                _jobService.Delete(job.Id);
            }
        }

        private JobProfile getSingleTestJob()
        {
            return new JobProfile()
            {
                Name = "Test job",
                Description = "Test job Description",
                Value = 9.99m
            };
        }

        private List<JobProfile> getTestJobs()
        {
            return new List<JobProfile>()
            {
                new JobProfile(){
                    Name = "job One",
                    Description = "job One Description",
                    Value = 3.50m
                },
                new JobProfile(){
                    Name = "job Two",
                    Description = "job Two Description",
                    Value = 5.00m
                },
                new JobProfile()
                {
                    Name = "job Three",
                    Description = "job Three Description",
                    Value = 7.65m
                }
            };
        }
    }
}
