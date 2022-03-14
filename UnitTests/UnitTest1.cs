using AutoMapper;
using FluentAssertions;
using HorseRacingBackend.DAL.Entities;
using HorseRacingBackend.DAL.Repositories;
using HorseRacingBackend.Profiles;
using HorseRacingBackend.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Tests
    {
        public decimal GetTip(decimal bill, decimal tipPercentage)
        {
            var tip = bill * tipPercentage;
            return bill + tip;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var result = GetTip(10, 0.2M);

            result.Should().BeInRange(0, 15);
        }

        [Test]
        public void Test2()
        {
            var result = GetTip(10, 0.2M);

            Assert.AreEqual(12, result);
        }

        [Test]
        public void HorseServiceTest()
        {
            ///aaa
            ///arrange
            int id = 999;
            var mockGenericRepository = new Mock<IGenericRepository<Horse>>();
            mockGenericRepository.Setup(r => r.GetByIDAsync(id)).Returns(Task.FromResult<Horse>(null));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MainProfile>());
            var mapper = new Mapper(config);
            //act
            var horseService = new HorseService(mockGenericRepository.Object, mapper);
            //assert
            Assert.ThrowsAsync<ArgumentException>(() => horseService.GetByIdAsync(id)); //Should throw exception
            horseService.GetByIdAsync(id).Should();
        }

        //[Test]
        //public void HorseServiceTest2()
        //{
        //    var mockGenericRepository = new MockGenericRepository();
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<MainProfile>());
        //    var mapper = new Mapper(config);

        //    int id = 999;
        //    var horseService = new HorseService(mockGenericRepository, mapper);
        //    var horse = horseService.GetByIdAsync(id);
        //    Assert.AreNotEqual(null, horse);
        //}
    }
}