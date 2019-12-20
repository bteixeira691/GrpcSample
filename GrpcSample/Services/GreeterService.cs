using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcSample.Repository;
using Microsoft.Extensions.Logging;

namespace GrpcSample
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ITodoRepository _todoRepository;

        public GreeterService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

       
        public override Task<HelloReply> SayHelloAgain(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello again " + request.Name });
        }
    }
}
