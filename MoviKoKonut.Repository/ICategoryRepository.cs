namespace MoviKokonut.Repository
{
    using System;
    using System.Collections.Generic;
    using MoviKokonut.Domain.Catalog;
    
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();

        ICollection<Subcategory> GetSubcategories(int categoryId);
    }
}