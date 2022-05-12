using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTrackR.API.Entities;

namespace DevTrackR.API.Persistence
{

    // Contexto de dados - para armazenar os objetos que for salvando em memória.
    // Para simular como se fosse um banco de dados em memória.
    public class DbMemContext
    {
        public DbMemContext()
        {
            Packages = new List<Package>();
        }

        public List<Package> Packages { get; set; }



    }
}