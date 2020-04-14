using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ShopDbContext ShopDbContext;

        public ShopDbContext Init()
        {
            return ShopDbContext ?? (ShopDbContext = new ShopDbContext());
        }

        protected override void DisposeCore()
        {
            if(ShopDbContext != null)
            {
                ShopDbContext.Dispose();
            }
        }
    }
}
