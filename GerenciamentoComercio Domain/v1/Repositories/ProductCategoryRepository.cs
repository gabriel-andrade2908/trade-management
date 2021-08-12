﻿using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository 
    {
        public ProductCategoryRepository(GerenciamentoComercioContext context) : base(context)
        {
        }
    }
}