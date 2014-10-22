// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Identity.Test;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Advanced;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Logging;

namespace Microsoft.AspNet.Identity.EntityFramework.InMemory.Test
{
    public static class TestIdentityFactory
    {
        public static InMemoryContext CreateContext()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework().AddInMemoryStore().UseLoggerFactory<LoggerFactory>();
            var serviceProvider = services.BuildServiceProvider();

            var db = new InMemoryContext(serviceProvider);
            db.Database.EnsureCreated();

            return db;
        }

        public static UserManager<IdentityUser> CreateManager(InMemoryContext context)
        {
            return MockHelpers.CreateManager<IdentityUser>(new InMemoryUserStore<IdentityUser>(context));
        }

        public static UserManager<IdentityUser> CreateManager()
        {
            return CreateManager(CreateContext());
        }

        public static RoleManager<IdentityRole> CreateRoleManager(InMemoryContext context)
        {
            var services = new ServiceCollection();
            services.AddDefaultIdentity<IdentityUser, IdentityRole>().AddRoleStore(new RoleStore<IdentityRole>(context));
            return services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
        }

        public static RoleManager<IdentityRole> CreateRoleManager()
        {
            return CreateRoleManager(CreateContext());
        }
    }
}
