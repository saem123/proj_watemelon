
using UnityEngine;
using Saem;
namespace Saem
{
    public class RxImageMaterial : RxImage<Material>
    {

        protected override void publishedValue(Material value)
        {
            applyComponent.material = value;
        }

        public void setMaterial(Material value)
        {
            publishedValue(value);
        }
    }
}