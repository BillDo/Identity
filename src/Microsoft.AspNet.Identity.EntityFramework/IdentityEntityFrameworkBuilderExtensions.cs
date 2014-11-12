// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System;

namespace Microsoft.Framework.DependencyInjection
{
    public static class IdentityEntityFrameworkBuilderExtensions
    {
        public static IdentityBuilder<IdentityUser, IdentityRole> AddEntityFramework(this IdentityBuilder<IdentityUser, IdentityRole> builder)
        {
            return builder.AddEntityFramework<IdentityDbContext, IdentityUser, IdentityRole>();
        }

        public static IdentityBuilder<IdentityUser, IdentityRole> AddEntityFramework<TContext>(this IdentityBuilder<IdentityUser, IdentityRole> builder)
            where TContext : DbContext
        {
            return builder.AddEntityFramework<TContext, IdentityUser, IdentityRole>();
        }

        public static IdentityBuilder<TUser, IdentityRole> AddEntityFramework<TContext, TUser>(this IdentityBuilder<TUser, IdentityRole> builder)
            where TUser : IdentityUser, new()
            where TContext : DbContext
        {
            return builder.AddEntityFramework<TContext, TUser, IdentityRole>();
        }

        public static IdentityBuilder<TUser, TRole> AddEntityFramework<TContext, TUser, TRole>(this IdentityBuilder<TUser, TRole> builder)
            where TUser : IdentityUser, new()
            where TRole : IdentityRole, new()
            where TContext : DbContext
        {
            builder.Services.Add(IdentityEntityFrameworkServices.GetDefaultServices<TContext, TUser, TRole>());
            return builder;
        }

        public static IdentityBuilder<TUser, TRole> AddEntityFramework<TContext, TUser, TRole, TKey>(this IdentityBuilder<TUser, TRole> builder)
            where TUser : IdentityUser<TKey>, new()
            where TRole : IdentityRole<TKey>, new()
            where TContext : DbContext
            where TKey : IEquatable<TKey>
        {
            builder.Services.Add(IdentityEntityFrameworkServices.GetDefaultServices<TContext, TUser, TRole, TKey>());
            return builder;
        }
    }
}