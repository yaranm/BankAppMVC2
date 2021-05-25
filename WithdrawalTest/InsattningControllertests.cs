using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAppMVC2.Controllers;
using BankAppMVC2.Services;
using Moq;
using BankAppMVC2.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using Newtonsoft.Json;
using BankAppMVC2.ViewModels;

namespace WithdrawalTest
{

    public class BaseTest2
    {
        protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();

    }



    [TestClass]
    public class InsattningControllertests: BaseTests
    {
        private InsattningController sut;
        private Mock <ITransactionRepository> ITransactionRepositoryMock;  
        private Mock <IAccountRepository> IAccountRepositoryMock;

        
        public InsattningControllertests()
        {
            var fakeAccountLista = new List<Account>(); 

            fakeAccountLista.Add(new Account
            {
                AccountId = 1,

                Balance = 100
            });
            


            var fakeAccountQ = fakeAccountLista.AsQueryable<Account>();

            var fakeTransLista = new List<Transaction>
            {
                new Transaction
                {
                     AccountId = 1,

                        Balance = 100
    }
            };

            var fakeTransQ = fakeTransLista.AsQueryable<Transaction>();

        
            ITransactionRepositoryMock = new Mock<ITransactionRepository>();
            IAccountRepositoryMock = new Mock<IAccountRepository>();

            ITransactionRepositoryMock.Setup(r => r.GetAllTransaction()).Returns(fakeTransQ);
            IAccountRepositoryMock.Setup(r => r.GetAllAccount()).Returns(fakeAccountQ);


           

            sut = new InsattningController(ITransactionRepositoryMock.Object, IAccountRepositoryMock.Object);
        }

       

        [TestMethod]
        public void testar_sa_att_det_inte_gar_att_satt_in_negativt_belopp()
        {
            var viewModel = fixture.Create<InsattningViewModel>();
            viewModel.AccountId = 1;
            viewModel.Amount = (-200);



            
            sut.New(viewModel);
            ITransactionRepositoryMock.Verify(e => e.Save(), Times.Never);
           
        }
        [TestMethod]
        public void testar_sa_att_det_gar_att_satt_in()
        {
            var viewModel = fixture.Create<InsattningViewModel>();
            viewModel.AccountId = 1;
            viewModel.Amount = 1000;




            sut.New(viewModel);
            ITransactionRepositoryMock.Verify(e => e.Save(), Times.Once);

        }

    }
}
