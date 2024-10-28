﻿using Datas.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)   { }
        public DbSet<Item> Items { get; set; }
    }
}