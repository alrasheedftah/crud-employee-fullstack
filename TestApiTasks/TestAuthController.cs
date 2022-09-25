using AutoMapper;
using EmployeeCrudTaskAPi;
using EmployeeCrudTaskAPi.DBContext;
using EmployeeCrudTaskAPi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApiTasks
{
    class TestAuthController<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestFixture<Startup> _fixture;
        public readonly CrudUserDbContext _context;
        public readonly IMapper _mapper;
        public readonly IConfiguration _configuration;
        public TestAuthController(TestFixture<Startup> fixture)
    {
            _fixture = fixture;
            _context = CreateDbContext();
            _mapper = (IMapper)_fixture.Server.Host.Services.GetService(typeof(IMapper));
            _configuration = (IConfiguration)_fixture.Server.Host.Services.GetService(typeof(IConfiguration));
        }
        public void Dispose()
    {
            _context.Dispose();
    }


        public async void Login_should_be_Successful(string userName, string password)
        {
            var fakeUserManager = new Mock<FakeUserManager>();

            var userToCheck = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            fakeUserManager.Setup(x => x.Users).Returns(_context.Users);

            fakeUserManager.Setup(x => x.CheckPasswordAsync(userToCheck, "12345678")).ReturnsAsync(true);



            // TODO If Have Mor Tiome To Complete
            //var mediator = new Mock<IMediator>();
            //LoginUserCommandHandler loginHandler = new LoginUserCommandHandler(_context, mediator.Object, _mapper, _configuration, fakeUserManager.Object);


            //ACT
            //var result = await loginHandler.Handle(new UserForLoginDto { Username = userName, Password = password }, new System.Threading.CancellationToken());
            //ASSERT
            //Assert.NotNull(result.Data.Token);

        }


        private CrudUserDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<CrudUserDbContext>()
            .UseInMemoryDatabase(databaseName: "UserDbContextTest")
            .Options;
            var dbContext = new CrudUserDbContext(options); dbContext.Users.AddRange(GetFakeData().AsQueryable());
            dbContext.SaveChanges();
            return dbContext;
        }
        private List<ApplicationUser> GetFakeData()
        {
            var users = new List<ApplicationUser>
      { new ApplicationUser { Id = "", UserName = "userTest", PasswordHash = "12345678", Email = "ana@g.com" }
      };
            return users;
        }
    }

}
