using Domain.Interfaces;
using Entities.Entities;
using Infrastucture.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {
        public RepositoryMessage()
        {
            
        }
    }
}
