using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Metanit;

namespace CRUDWithEFServiceApp.Services
{
    public class UserApiService : UserService.UserServiceBase
    {
        ApplicationContext db;
        public UserApiService(ApplicationContext db)
        {
            this.db = db;
        }

        // sending the list of users 
        public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
        {
            var listReply = new ListReply();    // defining the list 

            // convert each User object into a UserReply object 
            var userList = db.Users.Select(item => new UserReply { Id = item.Id, Name = item.Name, Age = item.Age }).ToList();
            listReply.Users.AddRange(userList);
            return Task.FromResult(listReply);
        }

        // sending one user by id 
        public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = await db.Users.FindAsync(request.Id);

            // if the user is not found, we throw an exception 
            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                               "User not found"));
            }

            UserReply userReply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
            return await Task.FromResult(userReply);
        }

        // adding a user 
        public override async Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            // creating a User object from the data  
            // and adding it to the users list 
            var user = new User { Name = request.Name, Age = request.Age };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            var reply = new UserReply()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };

            return await Task.FromResult(reply);
        }

        // user update 
        public override async Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var user = await db.Users.FindAsync(request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                  "User not found"));
            }

            // updating data 
            user.Name = request.Name;
            user.Age = request.Age;
            await db.SaveChangesAsync();

            var reply = new UserReply()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };

            return await Task.FromResult(reply);
        }

        // user deletion 
        public override async Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var user = await db.Users.FindAsync(request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                  "User not found"));
            }

            // deleting a user from the database 
            db.Users.Remove(user);
            await db.SaveChangesAsync();

            var reply = new UserReply()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };

            return await Task.FromResult(reply);
        }
    }
}
