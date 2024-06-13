using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gerenciador_de_tarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciador_de_tarefas.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}