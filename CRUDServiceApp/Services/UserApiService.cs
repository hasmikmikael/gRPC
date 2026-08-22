using Crud;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CRUDServiceApp.Services
{
    public class UserApiService : UserService.UserServiceBase
    {
        static int id = 0;  // counter for id generation 

        // hypothetical database 
        static List<User> users = new() { new User(++id, "Tom", 38), 
            new User(++id, "Bob", 42) };

        // sending the list of users 
        public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
        {
            var listReply = new ListReply();    // defining the list 

            // converting each object from the users list  
            // into a UserReply object 
            var userList = users.Select(item => new UserReply { Id = item.Id, 
                Name = item.Name, Age = item.Age }).ToList();
            listReply.Users.AddRange(userList);
            return Task.FromResult(listReply);
        }

        // sending one user by id 
        public override Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = users.Find(u => u.Id == request.Id);

            // if the user is not found, we throw an exception 
            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            UserReply userReply = new UserReply()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };

            return Task.FromResult(userReply);
        }

        // adding a user 
        public override Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            // creating a User object from the data and adding it  
            // to the users list 
            var user = new User(++id, request.Name, request.Age);

            users.Add(user);
            var reply = new UserReply()
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age
            };

            return Task.FromResult(reply);
        }

        // user update 
        public override Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            var user = users.Find(u => u.Id == request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            // updating data 
            user.Name = request.Name;
            user.Age = request.Age;

            var reply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
            return Task.FromResult(reply);
        }

        // user deletion 
        public override Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            var user = users.Find(u => u.Id == request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            users.Remove(user);
            var reply = new UserReply() { Id = user.Id, Name = user.Name, Age = user.Age };
            return Task.FromResult(reply);
        }
    }

    // user model — User class 
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}
