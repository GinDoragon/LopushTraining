using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LopushonokWPF
{
    partial class Product
    {
        public string ValidImage
        { 
            get
            {

                if (String.IsNullOrEmpty(Image) || String.IsNullOrWhiteSpace(Image)) return @"\products\picture.png";
                else return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Image;
            }
        }
        public string ValidMaterials
        {
            get
            {
                string materials = "";
                List<ProductMaterial> materialList = ProductMaterial.ToList();
                if (materialList != null && materialList.Count > 0)
                {
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        materials += materialList[i];
                        if (materialList.Last() == materialList[i]) materials += ".";
                        else materials += ",\n\t ";
                    }
                    return materials;
                }
                else materials = "Отсутствуют данные.";
                return materials;
            }
        }
    }
}
