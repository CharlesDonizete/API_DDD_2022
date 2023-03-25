﻿using Domain.Interfaces;
using Entities.Entities;
using Infrastucture.Configuration;
using Infrastucture.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {

        private readonly DbContextOptions<ContextBase> _ObtionsBuilder;

        public RepositoryMessage()
        {
            _ObtionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> ListarMessage(Expression<Func<Message, bool>> exMessage)
        {
            using (var banco = new ContextBase(_ObtionsBuilder))
            {
                return await banco.Message.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}
