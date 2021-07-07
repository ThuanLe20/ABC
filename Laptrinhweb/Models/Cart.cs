using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laptrinhweb.Models
{
    public partial class Cart
    {
        dbABCDataContext data = new dbABCDataContext();

        public int iidpro { get; set; }

        public string snamepro { get; set; }
        public string simage { get; set; }
        public double dprice { get; set; }
        public int icount { get; set; }
        public double dintoprice
        {
            get { return icount * dprice; }
        }

        public Cart(int idpro)
        {
            iidpro = idpro;
            Product pro = data.Products.Single(n => n.idpro == iidpro);
            snamepro = pro.namepro;
            simage = pro.image;
            dprice = double.Parse(pro.price.ToString());
            icount = 1;
        }
    }
    
}