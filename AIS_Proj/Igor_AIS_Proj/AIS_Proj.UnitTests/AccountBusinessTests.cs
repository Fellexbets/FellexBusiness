using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Business.Interfaces;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
using Igor_AIS_Proj.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Proj.UnitTests
{
    public class AccountBusinessTests
    {
        //private Account _account;
        private CreateAccountRequest _createAccountRequest;
        //private CreateAccountResponse _createAccountResponse;
        //private Movim _movims;
        private TransferRequest _transferRequest;
        //private AccountMovims _accountMovims;
        private Account _account1;
        private Account _account2;
        private Account _account10;
        private readonly IAccountBusiness _accountBusiness;
        private readonly Mock<IAccountBusiness> _mockAccountBusiness;
        private readonly Mock<IAccountPersistence> _accountPersistence;
        private readonly Mock<IMovementPersistence> _movementPersistence;
        private readonly Mock<ITransferPersistence> _transferPersistence;
        private Mock<ILogger<AccountBusiness>> _logger;

        public AccountBusinessTests()
        {
            _accountPersistence = new Mock<IAccountPersistence>();
            _movementPersistence = new Mock<IMovementPersistence>();
            _transferPersistence = new Mock<ITransferPersistence>();
            _logger = new Mock<ILogger<AccountBusiness>>();

            _accountBusiness = new AccountBusiness(_accountPersistence.Object, _movementPersistence.Object, _transferPersistence.Object, _logger.Object);

        }

        [SetUp]
        public void Setup()
        {
            _createAccountRequest = new CreateAccountRequest {  Balance = 1000, Currency = "EUR" };
            //_createAccountResponse = new CreateAccountResponse { AccountId = 1, Balance = 100, Currency = "EUR", UserId = 1 };
            //_movims = new Movim();

            _transferRequest = new TransferRequest { Amount = 100, FromAccountId = 1, ToAccountId = 2 };
            //_accountMovims = new AccountMovims { Account = _createAccountResponse, Movims = (ICollection<Movim>)_movims };

            _account1 = new Account { AccountId = 1, Balance = 1000, Currency = "USD", UserId = 1 };
            _account2 = new Account { AccountId = 2, Balance = 1000, Currency = "USD", UserId = 2 };
            _account10 = new Account { AccountId = 10, Balance = 100, Currency = "USD", UserId = 2 };

            _accountPersistence.Setup(r => r.GetById(1)).Returns(_account1);
            _accountPersistence.Setup(r => r.GetById(2)).Returns(_account2);
            _accountPersistence.Setup(r => r.GetById(10)).Returns(_account10);

            

            _accountPersistence.Setup(r => r.Create(_createAccountRequest, 1)).Returns(CreateOk());
        }
        private  async Task<CreateAccountResponse> CreateOk()
        {
            var userId = 1;
            Account response = EntityMapper.MapRequestToAccountModel(_createAccountRequest, userId);
            return EntityMapper.MapAccountModelToContract(response);
        }


        [Test]
        public async Task Create_ResultOk()
        {
            // Arrange
            int userId = 1;
            // Act
            var result = await _accountBusiness.Create(_createAccountRequest, userId);
            // Assert
            Assert.That(result.Item1, Is.True);
        }

        [Test]
        public async Task Create_ResultNotOk()
        {
            // Arrange
            _accountPersistence.Setup(r => r.Create(_createAccountRequest, 3)).Throws(new Exception());

            //var logger = new Mock<ILogger<AccountBusiness>>();
            // Act
            var result = await _accountBusiness.Create(_createAccountRequest, 3);
            // Assert
            Assert.That(() => result.Item1, Is.False);

        }

        [Test]
        public async Task GetAllAccountsUser_UserOwnsAccount_ResultOk()
        {
            var userId = 1;
            Task<List<Account>> account = Task.FromResult(new List<Account>());
            _accountPersistence.Setup(r => r.GetAllAccountsUser(userId)).Returns(account);
            // Arrange
            
            // Act
            var result = await _accountBusiness.GetAllAccountsUser(userId);
            // Assert

            Assert.That(result.Item1, Is.True);
            Assert.That(result.Item2, Is.TypeOf<List<Account>>());
        }
        [Test]
        public async Task GetAllAccountsUser_AccountNotFound_ResultNull()
        {

            // Arrange
            var userId = 99;
            Task<List<Account>> account = Task.FromResult(new List<Account>());
            _accountPersistence.Setup(r => r.GetAllAccountsUser(userId)).Throws(new Exception());
            // Act
            var result = await _accountBusiness.GetAllAccountsUser(userId);
            // Assert

            Assert.That(result.Item1, Is.False);
        }

        [Test]
        public async Task GetAccount_ResultOk()
        {
            // Arrange
            
            _accountPersistence.Setup(r => r.GetById(_account1.AccountId)).Returns(_account1);
            // Act
            var result =  _accountBusiness.GetById(_account1.AccountId);

            // Assert
            Assert.That(result.Item1, Is.True);
        }
        [Test]
        public async Task GetAccount_AccountNotFound_ResultNotOk()
        {
            // Arrange
            var accountId = 1000;
            _accountPersistence.Setup(r => r.GetById(accountId)).Returns(() => null);
            // Act
            var result = await _accountBusiness.GetAccount(accountId);
            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.AreEqual(result, (false, result.Item2, "Account Not Found."));
        }
        [Test]
        public async Task GetAccount_ResultNotOk()
        {
            // Arrange
            var userId = 1;
            _accountPersistence.Setup(r => r.GetById(userId)).Throws(new Exception());

            // Act
            var result = await _accountBusiness.GetAccount(userId);
            // Assert
            Assert.That(result.Item1, Is.False);
        }

        [Test]
        public async Task TransferFunds_AmountNegative_ResultNotOk()
        {
            // Arrange
            _transferRequest.Amount = -1;

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.AreEqual(result, (false, "Transfer amount must be positive"));
        }
        [Test]
        public async Task TransferFunds_WrongCurrency_ResultNotOk()
        {
            // Arrange
            _account2.Currency = "EUR";

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.AreEqual((false, "You can only transfer in the same currency"), result);
        }
        [Test]
        public async Task TransferFunds_InsufficientFunds_ResultNotOk()
        {
            // Arrange
            _account1.Balance = 0;

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);

            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.AreEqual((false, "Insufficient funds"), result);
        }
        [Test]
        public async Task TransferFunds_SameAccount_ResultNotOk()
        {
            // Arrange

            _transferRequest.ToAccountId = 1;
            var expected = (false, "You can´t transfer to the same account!");

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);
            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result, Is.EqualTo(expected));

        }
        [Test]
        public async Task TransferFunds_DestinationAccountNotValid_ResultNotOk()
        {
            // Arrange
            _account2.AccountId = 0;
            var expected = (false, "Destination Account not valid!");

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);
            // Assert
            Assert.That(result.Item1, Is.False);
            Assert.That(result, Is.EqualTo(expected));

        }
        [Test]
        public async Task TransferFunds_ResultOk()
        {
            // Arrange
            
            var expected = (true, "Transaction Complete!");

            // Act
            var result = await _accountBusiness.TransferFunds(_transferRequest);

            // Assert
            Assert.That(result.Item1, Is.True);
            Assert.That(result, Is.EqualTo(expected));

        }

    }
}
