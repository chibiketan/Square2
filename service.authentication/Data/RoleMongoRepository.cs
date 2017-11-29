﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ketan.Square2.Service.Authentication.Data.Interface;
using Ketan.Square2.Service.Authentication.Model;
using Ketan.Square2.Service.Authentication.Model.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Ketan.Square2.Service.Authentication.Data
{
    public class RoleMongoRepository : BaseMongoRepository<Role>, IRoleRepository
    {
        public RoleMongoRepository(DatabaseConfiguration config)
            : base(config, "Role")
        {
            
        }

        public async Task CreateRoleAsync(Role role)
        {
            if (null == role)
            {
                throw new ArgumentNullException(nameof(role));
            }

            // On test si un objet existe déjà avec cet id et on lève une exception si c'est le cas
            var existingRole = await m_collection.AsQueryable().FirstOrDefaultAsync(r => r._id == role._id);

            if (null != existingRole)
            {
                throw new ObjectExistsException($"A Role with id \"{role._id}\" already exists");
            }

            // On test si un objet existe déjà avec ce nom et on lève une exception si c'est le cas
            existingRole = await m_collection.AsQueryable().FirstOrDefaultAsync(r => r.Name == role.Name);

            if (null != existingRole)
            {
                throw new ObjectExistsException($"A Role with name \"{role.Name}\" already exists");
            }

            StripDates(role);
            await m_collection.InsertOneAsync(role);
        }

        public Task<List<Role>> GetAllRoleAsync()
        {
            return m_collection.AsQueryable().ToListAsync();
        }

        public Task<Role> GetRoleByNameAsync(string name)
        {
            if (null == name)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if ("" == name)
            {
                throw new ArgumentException(nameof(name));
            }

            var query = from role in m_collection.AsQueryable()
                        where role.Name == name
                        select role;

            return query.FirstOrDefaultAsync();
        }

        protected override void OnCollectionCreated(IMongoCollection<Role> collection)
        {
            // Nothing to do
        }

        private void StripDates(Role role)
        {
            role.CreationDate = role.CreationDate.StripTick();
            role.ModificationDate = role.ModificationDate.StripTick();
        }
    }
}