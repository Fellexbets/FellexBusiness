using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Business.Interfaces;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
using Igor_AIS_Proj.Persistence;
using Igor_AIS_Proj.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AIS_Proj.UnitTests
{

    public class UserBusinessTests

    {
        private readonly IUserBusiness _userBusiness;
        private readonly Mock<IUserPersistence> _userPersistence;
        private readonly Mock<ISessionPersistence> _sessionPersistence;
        private readonly Mock<IJwtServices> _jwtServices;

        private CreateUserRequest _request;

        public UserBusinessTests()
        {
            var userPersistenceMock = new Mock<IUserPersistence>();
            var sessionPersistenceMock = new Mock<ISessionPersistence>();
            var jwtServicesMock = new Mock<IJwtServices>();

            _userPersistence = new Mock<IUserPersistence>();
            _sessionPersistence = new Mock<ISessionPersistence>();
            _jwtServices = new Mock<IJwtServices>();

            _userBusiness = new UserBusiness(_userPersistence.Object, _sessionPersistence.Object, _jwtServices.Object);

            Setup();

        }
        [SetUp]
        public void Setup()
        {
            _request = new CreateUserRequest
            { Email = "unittest@email.com", FullName = "Unit Test", Username = "Unittest", UserPassword = "unittest" };

           

            _userPersistence.Setup(r => r.Create(_request)).Returns(CreateOk(_request));

            //_userBusinessMock.Setup(r => r.Authenticate(_loginUserRequest)).Returns(LoginOk);
        }
        private async Task<CreateUserResponse> CreateOk(CreateUserRequest request)
        {
            var response = EntityMapper.MapRequestToUser(request);
            return (EntityMapper.MapUserToResponse(response));
        }
        [Test]
        public async Task Create_AnnonimousUser_CreatesUser()
        {
            // Arrange

            // Act
            var result = await _userBusiness.Create(_request);

            // Assert
            Assert.That(result.Email, Is.EqualTo("unittest@email.com"));
            Assert.That(result, Is.TypeOf<CreateUserResponse>());
        }





    }
}