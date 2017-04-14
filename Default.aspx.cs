using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class _Default : System.Web.UI.Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
        ProductList products = new ProductList();

        var path = HttpContext.Current.Server.MapPath(@"~/load/Products.xml");

        XmlSerializer serializer = new XmlSerializer(typeof(ProductList));
        using (TextReader file = new StreamReader(path))
        {
            products = ((ProductList) serializer.Deserialize(file)); 
        }

        fillForms(products);

    }

    private void fillForms(ProductList products)
    {

        foreach(var product in products.Item)
        {
            TableRow parentRow = new TableRow();
            TableRow nullRow = new TableRow();
            TableRow tRow1 = new TableRow();
            TableRow tRow2 = new TableRow();
            TableRow tRow3 = new TableRow();
            TableCell tdName = new TableCell();
            TableCell tdDesc = new TableCell();
            TableCell tdImg = new TableCell();
            TableCell tdPurc = new TableCell();

            nullRow.Height = 5;
            nullRow.Cells.Add(new TableCell());

            tdName.Text = product.name;
            tdDesc.Text = product.description;
            tdImg.Text = string.Format("<img src='" + product.imageURL + "' />");
            tdPurc.Text = product.purchaseButton;

            tRow1.Cells.Add(tdName);
            tRow2.Cells.Add(tdImg);
            tRow2.Cells.Add(tdDesc);
            tRow3.Cells.Add(tdPurc);

            ProductTable.Rows.Add(tRow1);
            ProductTable.Rows.Add(tRow2);
            ProductTable.Rows.Add(tRow3);
            ProductTable.Rows.Add(nullRow);

        }

    }

    [XmlRoot("ProductList")]
    public class ProductList
    {
        public ProductList() { Item = new List<ProductItem>(); }
        [XmlElement("ProductItem")]
        public List<ProductItem> Item { get; set; }
    }

    public class ProductItem
    {
        [XmlElement("id")]
        public string id { get; set; }
        [XmlElement("name")]
        public string name { get; set; }
        [XmlElement("description")]
        public string description { get; set; }
        [XmlElement("imageURL")]
        public string imageURL { get; set; }
        [XmlElement("purchaseButton")]
        public string purchaseButton { get; set; }

    }
}