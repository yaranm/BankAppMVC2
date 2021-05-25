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

    public class BaseTests
    {
        protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();

    }



    [TestClass]
    public class OverforingControllertests: BaseTests
    {
        private OverforingController sut;
        private Mock <ITransactionRepository> ITransactionRepositoryMock;  
        private Mock <IAccountRepository> IAccountRepositoryMock;

        
        public OverforingControllertests()
        {
            var fakeAccountLista = new List<Account>(); 

            fakeAccountLista.Add(new Account
            {
                AccountId = 1,
                 Balance = 100
            });
            fakeAccountLista.Add(new Account
            {
                AccountId = 2,
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
            




            sut = new OverforingController(ITransactionRepositoryMock.Object, IAccountRepositoryMock.Object);
        }

        [TestMethod]
        public void testar_sa_att_det_inte_gar_att_overfora_mer_an_vad_som_finns()
        {
            var viewModel = fixture.Create<OverforingViewModel>();
            viewModel.AccountId = 1;
            viewModel.Amount = 200;
            viewModel.AccountIdMot = 2;


            sut.NewOverforing(viewModel);
            ITransactionRepositoryMock.Verify(e => e.Save(), Times.Never);
            
        }
        
        [TestMethod]
        public void testar_sa_att_det_inte_gar_att_overfora_negativt_belopp()
        {
            var viewModel = fixture.Create<OverforingViewModel>();
            viewModel.AccountId = 1;
            viewModel.Amount = (-200);
            viewModel.AccountIdMot = 2;




            sut.NewOverforing(viewModel);
            ITransactionRepositoryMock.Verify(e => e.Save(), Times.Never);
           
        }
        
        [TestMethod]
        public void En_korrekt_overforing()
        {
            var viewModel = fixture.Create<OverforingViewModel>();
            viewModel.AccountId = 1;
            viewModel.Amount = 50;
            viewModel.AccountIdMot = 2;




            sut.NewOverforing(viewModel);

            ITransactionRepositoryMock.Verify(e => e.Save(), Times.Once);

        }


    }
}
